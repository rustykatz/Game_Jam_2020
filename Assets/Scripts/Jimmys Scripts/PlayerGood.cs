using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerGood : MonoBehaviour
{

    public float speed = 10f;
    public Vector3 move;
    public Vector3 velocity;
    public float gravity = -9.8f;
    public float sens = 5f;
    CharacterController controller;
    public float jumpHeight = 5f;

    // Player Health Management
    public float health;
    public float maxHealth; 
    private float hperc;
    public TextMeshProUGUI hpText;

    //
    public TextMeshProUGUI textBox2;
    public TextMeshProUGUI textBox3;


    // Player Gun barrel Positions
    public Transform bulletSpawn_RT;
    public Transform bulletSpawn_LT;

    // Projectile Management 
    public int bulletSpeed = 100; 
    public GameObject bulletPrefab;
    public float spreadFactor = 100f; 

    // Weapon Management 
    [SerializeField] private bool weapon_1;
    [SerializeField] private bool weapon_2;


    
    private int impWeapons = 2;
    public float damage;
    public int weaponLevel;

    // Respawn Location
    public GameObject respawn; 
    Vector3 respawnOffset;

    // test message
    public string tm1;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

        // Starting Stats
        health = 10;
        maxHealth = health;
        weaponLevel = 1;
        damage = 1; 

        weapon_1= true;
        weapon_2 = false;

    }

    // Update is called once per frame
    void Update()
    {   
        Movement();
        Gravity();
        Look();
        UIupdate();
        TestSendMsg(tm1);
        WeaponFire();
       
    }
    void UIupdate(){
        hpText.GetComponent<TextMeshProUGUI>().SetText("HP: " + health.ToString());
        textBox2.GetComponent<TextMeshProUGUI>().SetText("WP Lvl: " + weaponLevel.ToString());
        textBox3.GetComponent<TextMeshProUGUI>().SetText("DMG: " + damage.ToString());

    }
    void Look(){
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX * sens, 0);
        Camera.main.gameObject.transform.Rotate(-mouseY * sens,0,0);
    }

    void Gravity(){
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
    }

    void Movement(){
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
    }

    void WeaponFire(){
        if(Input.GetMouseButtonDown(0) && weapon_1)
        {
            ShootWeapon();
        }
        
    }
    // void ShootRightWeapon(){
    //     // Create the Bullet from the Bullet Prefab
    //     var bulletTop = (GameObject)Instantiate(
    //         // Bullet prefab object
    //         bulletPrefab,
    //         // Bullet spawn position
    //         bulletSpawn_RT.position,
    //         // Bullet spawn rotation 
    //         bulletSpawn_RT.rotation);
    //     // Add velocity to the bullet so it moves 
    //     bulletTop.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
    //     // Destroy the bullet after 2 seconds 
    //     Destroy(bulletTop, 3.0f);


    // }
    void ShootWeapon(){
        
        var rX = Random.Range(-spreadFactor, spreadFactor);
        var rY = Random.Range(-spreadFactor, spreadFactor);
        var rZ = Random.Range(-spreadFactor, spreadFactor);
        // rO is offset factor used to randomize second shot so both shots don't hit same place. Saves comp time of calc 3 new rand vars
        var rO = Random.Range(-spreadFactor/2, spreadFactor/2);

        var bulletTop = (GameObject)Instantiate(bulletPrefab, bulletSpawn_LT.position, bulletSpawn_LT.rotation);
        // bulletTop.GetComponent<Transform>().Rotate(randomNumberX, randomNumberY, randomNumberZ);
        bulletTop.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f + new Vector3 (rX, rY, rZ));
        Destroy(bulletTop, 3.0f);
       
    }

    public void TakeDamage(float damage){
        if(health > 0.01){
            health -= damage;
            hperc = health/ maxHealth + 0.05f;
            if(health<=0){
                OnDeath();
                gameObject.transform.position = respawn.transform.position + respawnOffset;
                health = 10;
                print("Health reset: " + health.ToString());
                maxHealth = health;

            }
        }
    }

    void OnDeath(){
        print("You Died!");
    }

    public void WeaponUpgrade(int _upgrade){
        weaponLevel += _upgrade; 
        print("PLAYER: Weapon Level -> " + weaponLevel.ToString());
    }

    public void Heal(float _heal){
        health += _heal; 
        if( health > maxHealth ){
            health = maxHealth;
        }
    }

    public void BoostDamage(float _boost){
        damage += _boost;
        print("PLAYER: Weapon Damage -> " + damage.ToString());
    }

    void TestSendMsg(string message){
        if(Input.GetKeyDown(KeyCode.M)){
            Debug.Log("Sending Message to script -> Game Handler");
            GameObject gh = GameObject.FindGameObjectWithTag("GameHandler");
            gh.GetComponent<GameHandler>().GetMsg(message);
        }
        
    }
}
