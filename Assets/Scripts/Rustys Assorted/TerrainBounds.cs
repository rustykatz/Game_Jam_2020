using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainBounds : MonoBehaviour
{
    public GameObject respawn; 
    public Vector3 pos;
    public Vector3 offset; 
    // Start is called before the first frame update
    void Start()
    {
        pos = respawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Check update of respawn location
        
    }

    void OnCollisionEnter(Collision col){
        offset = new Vector3(0,5,0);
        if(col.gameObject.tag == "Player"){
            col.gameObject.transform.position = pos + offset; 
            print("Player out of bounds"); 
        }
    }
}
