using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGood : MonoBehaviour
{

    public float speed = 10f;
    public Vector3 move;
    public Vector3 velocity;
    public float gravity = -9.8f;
    public float sens = 5f;
    CharacterController controller;
    public float jumpHeight = 5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //-----------CAMERA-------------
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX * sens, 0);
        Camera.main.gameObject.transform.Rotate(-mouseY * sens,0,0);
        //-----------CAMERA-------------

        //-----------WASD-------------
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        //-----------WASD-------------

        //-----------GRAVITY-------------
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        //-----------GRAVITY-------------

        //-----------JUMP-------------
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        //-----------JUMP-------------
    }
}
