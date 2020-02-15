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

    // Projectile Management 
    public int bulletSpeed = 20; 
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

    }

    // Update is called once per frame
    void Update()
    {   
        Movement();
        Gravity();
        Look();
        UIupdate();
        TestSendMsg(tm1);
       
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

    void TestSendMsg(string message){
        if(Input.GetKeyDown(KeyCode.M)){
            Debug.Log("Sending Message to script -> Game Handler");
            GameObject gh = GameObject.FindGameObjectWithTag("GameHandler");
            gh.GetComponent<GameHandler>().GetMsg(message);
        }
        
    }
}
