using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotator : MonoBehaviour
{
    public int rotx;
    public int roty;
    public int rotz;
    

    // literally just rotates an item
    void Update()
    {
        transform.Rotate(rotx * Time.deltaTime, roty *Time.deltaTime, rotz * Time.deltaTime);
    }
}
