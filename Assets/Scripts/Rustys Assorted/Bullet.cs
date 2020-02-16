using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {

    private float dmg; 

    void Awake(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerGood playerScript = player.GetComponent<PlayerGood>();
        dmg = playerScript.damage;
    }


    void Update(){

    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Enemy"))
        {
            // Accesses enemy health variable and applys damage to it 
            coll.GetComponent<EnemyHealth>().TakeDamage(dmg);
            Destroy(gameObject);

        }
    }
}
