using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float rotation;
    public float rotationY;
    public float speed=2f; 

    public Vector3 velocity;
    public float gravity = -9.8f;
    public CharacterController controller;
    float jumpHeight = 5f;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Mouse X"), 0);

        rotation -= Input.GetAxis("Mouse Y");
        rotationY = transform.Find("Main Camera").localEulerAngles.y;
        transform.Find("Main Camera").localEulerAngles = new Vector3(rotation, rotationY, 0);

        Vector3 move = transform.right * Input.GetAxis("Horizontal")*speed + transform.forward * Input.GetAxis("Vertical")*speed;
        controller.Move(move * Time.deltaTime);


        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if(controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
