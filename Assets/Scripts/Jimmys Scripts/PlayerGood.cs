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



    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {   
        Movement();
        Gravity();
        Look();
        
       
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


}
