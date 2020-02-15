using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAtLocations : MonoBehaviour
{
    // Spawn Locations
    public GameObject spawn_1;
    public GameObject spawn_2;
    public GameObject spawn_3;
    public GameObject spawn_4;
    public bool spawnOne;
    public bool spawnRepeat;
    public float timerInterval = 10;
    float timer = 0; 
    // Object you want to spawn
    public GameObject toSpawn; 
    // Stores random spawn location index
    public int obj_index; 
    // Stores list of locations
    public List<GameObject> loc = new List<GameObject>();

    void Start()
    {
        spawnOne = false;
        spawnRepeat = true; 
        loc.Add(spawn_1);
        loc.Add(spawn_2);
        loc.Add(spawn_3);
        loc.Add(spawn_3);


        if( spawnOne == true){
            Instantiate(toSpawn, loc[obj_index].transform.position, Quaternion.identity);
        }
        
    }

    void Update(){
        if(spawnRepeat == true){
            obj_index = Random.Range(0, 3);
            timer += Time.deltaTime;
            if( timer > timerInterval){
                Instantiate(toSpawn, loc[obj_index].transform.position, Quaternion.identity);
                timer = 0;
            }
        } 
    }

}
