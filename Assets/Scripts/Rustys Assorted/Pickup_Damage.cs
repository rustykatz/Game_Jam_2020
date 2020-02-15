using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Damage : MonoBehaviour
{
    [SerializeField] private int dmgAmount = 1;

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().BoostDamage(dmgAmount);
            print("PICKED UP: Damage boost player by -> + " + dmgAmount.ToString());
            Destroy(gameObject);
        }
    }
}
