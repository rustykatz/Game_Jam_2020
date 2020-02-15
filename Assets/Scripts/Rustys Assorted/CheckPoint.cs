using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // Need to create a game manager that saves the latest checkpoint 
    public Vector3 savePos;

    // Update is called once per frame
    void Update()
    {
        CheckCheckpoint();
    }

    void CheckCheckpoint(){
    // If not reached before, append, else don't 
    }
    
    void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == "Player")
        {
            savePos = transform.position; 
        }

    }


}
