using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour {

    private float dmg; 
    public int speed = 10;

    void Awake(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player playerScript = player.GetComponent<Player>();
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
