using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    // RB and Camera 
    Rigidbody rb; 
    Transform cameraTransform;

    // Mouse Controller 
    private float mouseX;
    private float mouseY;
    private float yawH = 100f;
    private float pitchV = 100f;
    public float speed = 5.0f;
    public float jumpForce = 5f;
    
    // Distance of player to ground
    [SerializeField] private float groundDistance =0.4f;
    [SerializeField] private bool isGrounded;
    public Transform groundCheck;
    public LayerMask groundMask;

    // Player Health Management
    public float health;
    public float maxHealth; 
    private float hperc;
    public TextMeshProUGUI hpText;

    // Player Gun barrel Positions
    public Transform bulletSpawn_RT;
    public Transform bulletSpawn_RB;
    public Transform bulletSpawn_LT;
    public Transform bulletSpawn_LB;

    // Projectile Management 
    public int bulletSpeed = 20; 
    public GameObject bulletPrefab;
    public float spreadFactor = 100f; 

    // Weapon Management 
    [SerializeField] private bool weapon_1;
    [SerializeField] private bool weapon_2;
    [SerializeField] private bool weapon_3;
    [SerializeField] private bool weapon_4;
    private int impWeapons = 4;
    public float damage;
    public int weaponLevel;

    // Respawn Location
    public GameObject respawn; 
    Vector3 respawnOffset;


    void Start()
    {   
        
        // RB and Camera Setup
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = Camera.main.transform;

        // Starting Stats
        health = 10;
        maxHealth = health;
        weaponLevel = 1;
        damage = 1; 

    }

    void Update()
    {
        CheckGrounded();
        look();
        WeaponFire();
        WeaponSwapper();
        Jump();
        hpText.GetComponent<TextMeshProUGUI>().SetText("HP: " + health.ToString());
    }

    void FixedUpdate(){
        PlayerMovement();
    }

    void PlayerMovement()
    {
        Vector3 movement = Vector3.zero; 
        if (Input.GetKey(KeyCode.W)){ movement += transform.forward;}
        if (Input.GetKey(KeyCode.A)){ movement += transform.TransformDirection (Vector3.left);}
        if (Input.GetKey(KeyCode.S)){ movement -= transform.forward;}
        if (Input.GetKey(KeyCode.D)){ movement += transform.TransformDirection (Vector3.right);}

        movement = Vector3.Normalize (movement);
        movement = movement * speed;
        rb.MovePosition (transform.position + movement * Time.fixedDeltaTime);       
    }

    void CheckGrounded(){
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

    }

    void Jump(){
         if (Input.GetKeyDown(KeyCode.Space) && isGrounded == true)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void WeaponFire(){
        if(Input.GetMouseButtonDown(1) && weapon_1)
        {
            ShootRightWeapon();
        }
        if(Input.GetMouseButtonDown(0) && weapon_2)
        {
            ShootLeftWeapon();
        }
    }

    void WeaponSwapper(){
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            if(weapon_1 == false){
                weapon_1 =true;
            }
            else{
                weapon_1 = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            if(weapon_2 == false){
                weapon_2 =true;
            }
            else{
                weapon_2 = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            if(weapon_3 == false){
                weapon_3 =true;
            }
            else{
                weapon_3 = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            if(weapon_4 == false){
                weapon_4 =true;
            }
            else{
                weapon_4 = false;
            }
        }
    }

    void look()
    {
        mouseX += Input.GetAxis("Mouse X") * yawH * Time.deltaTime;
        mouseY += Input.GetAxis("Mouse Y") * pitchV *Time.deltaTime;
    
        mouseY = Mathf.Clamp(mouseY, -30f, 80f);
        transform.eulerAngles = new Vector3(-mouseY, mouseX,0f);
    }   

    // Collision for items 
    // void OnCollisionEnter(Collision col){
    //     if(col.gameObject.tag == "Floor" ){
    //         isGrounded = true;
    //     }
    // }
    // void OnCollisionExit(Collision col){
    //     if(col.gameObject.tag == "Floor" ){
    //         isGrounded = false; 
    //     }
    // }

    void ShootRightWeapon(){
        // Create the Bullet from the Bullet Prefab
        var bulletTop = (GameObject)Instantiate(
            // Bullet prefab object
            bulletPrefab,
            // Bullet spawn position
            bulletSpawn_RT.position,
            // Bullet spawn rotation 
            bulletSpawn_RT.rotation);
        // Add velocity to the bullet so it moves 
        bulletTop.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
        // Destroy the bullet after 2 seconds 
        Destroy(bulletTop, 3.0f);

        // Bottom right barrel
        if(weapon_3 == true){
            var bulletBot = (GameObject)Instantiate(bulletPrefab, bulletSpawn_RB.position, bulletSpawn_RB.rotation);
            bulletBot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f);
            Destroy(bulletBot, 3.0f);
        }
    }
    void ShootLeftWeapon(){
        
        var rX = Random.Range(-spreadFactor, spreadFactor);
        var rY = Random.Range(-spreadFactor, spreadFactor);
        var rZ = Random.Range(-spreadFactor, spreadFactor);
        // rO is offset factor used to randomize second shot so both shots don't hit same place. Saves comp time of calc 3 new rand vars
        var rO = Random.Range(-spreadFactor/2, spreadFactor/2);

        var bulletTop = (GameObject)Instantiate(bulletPrefab, bulletSpawn_LT.position, bulletSpawn_LT.rotation);
        // bulletTop.GetComponent<Transform>().Rotate(randomNumberX, randomNumberY, randomNumberZ);
        bulletTop.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f + new Vector3 (rX, rY, rZ));
        Destroy(bulletTop, 3.0f);

        if(weapon_4 == true){
            var bulletBot = (GameObject)Instantiate(bulletPrefab, bulletSpawn_LB.position, bulletSpawn_LB.rotation);
            bulletBot.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed * 10f + new Vector3(rX +rO, rZ + rO, rY +rO));
            Destroy(bulletBot, 3.0f);
        }
       
    }

 
    //  var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
    //  bullet.transform.Rotate(
    //  bullet.rigidbody.AddForce(bullet.transform.forward * 10000);






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
    
    public void TakeDamage(float damage){
        if(health > 0.01){
            health -= damage;
            hperc = health/ maxHealth + 0.05f;
        }
        else
        {
            gameObject.transform.position = respawn.transform.position + respawnOffset;
            health = 10;
            print("Health reset: " + health.ToString());
            maxHealth = health;
            // Destroy(gameObject);
        }  
    }

}
