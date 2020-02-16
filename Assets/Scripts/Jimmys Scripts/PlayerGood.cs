using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerGood : MonoBehaviour
{

    public float ForwardBackMoveSpeed = 5f;
    public float LeftRightMoveSpeed = 5f;
    public float LeftRightLookSpeed = 2f;

    public Vector3 move;
    public Vector3 velocity;
    public float gravity = -9.8f;
    public float sens = 5f;
    CharacterController controller;
    public float jumpHeight = 5f;

    public GameObject CameraObject;

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

    Animator an;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        an = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {   
        Movement();
        Gravity();
        Look();
        Attack();
       
    }
    void Look(){
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if(mouseX > 0){
            an.SetBool("RightStrafe",true);
            an.SetBool("LeftStrafe",false);
        }
        else //if (mouseX)
        {
            an.SetBool("RightStrafe",false);
            an.SetBool("LeftStrafe",true);
        }
        if (mouseX < 0.05 && mouseX > -0.05){
            an.SetBool("RightStrafe",false);
            an.SetBool("LeftStrafe",false);
        }

        //transform.Rotate(0, mouseX * sens, 0);
        Vector3 thing = transform.right * mouseX;
        controller.Move(thing * LeftRightMoveSpeed * Time.deltaTime);
        
        CameraObject.transform.Rotate(mouseY * sens,0,0);
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
        
        //Animations x != 0 ||
        if(z != 0){
            an.SetBool("Running",true);
        }
        else
        {
            an.SetBool("Running",false);
        }
        if(z < 0)
            x = -x;
        transform.Rotate(0, x * LeftRightLookSpeed, 0);

//transform.right * x + 
        move = transform.forward * z;
        
        
        controller.Move(move * ForwardBackMoveSpeed * Time.deltaTime);
    }

    void Attack(){
        if (Input.GetAxis("Fire1") > 0){
            an.SetBool("Attack 0", true);
            StartCoroutine("asd");
            //an.SetBool("Attack 0", false);
            //an.ResetTrigger("Attack"); 
        }
    }

    IEnumerator asd(){
        yield return new WaitForSeconds(1f);
        an.SetBool("Attack 0", false);
    }

}
