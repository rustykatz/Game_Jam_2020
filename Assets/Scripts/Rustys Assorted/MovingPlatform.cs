using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public GameObject destination;
    public Vector2 dest;
    public Vector2 origin;

    public int direction = 1;
    public float speed = 5;
  

    void Start()
    {
        origin.x = transform.position.x;
        dest.x = destination.transform.position.x;
    }
    void FixedUpdate()
    {
        if(direction == 1 && transform.position.x > dest.x)
        {
            direction = -1;
        }
    
        if(direction == -1 && transform.position.x < origin.x)
        {
            direction = 1;
        }
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }
}
