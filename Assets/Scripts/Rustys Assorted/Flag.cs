using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    // Signify a captured flag
    public bool captured;
    // How long we would like to capture the flag for
    public float timeToCapture;
    // Time spent inside capture zone
    public float timeSoFar;

    // Update is called once per frame
    void Update()
    {
        CheckCaptured();
    }

    void CheckCaptured()
    {
        if (timeSoFar > timeToCapture)
        {
            captured = true;
           // Debug.Log("Flag is Captured");
        }
    }

    // Checks if player is still inside flag
    void OnTriggerStay(Collider col)
    {
        if ( col.gameObject.tag == "Player")
        {
            timeSoFar += Time.deltaTime;
        }
    }

    //Checks if flag is colliding with the player and if we havent captured the flag
    private void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {
            if (!captured)
            {
                timeSoFar = 0;
            }
        }
    }
}
