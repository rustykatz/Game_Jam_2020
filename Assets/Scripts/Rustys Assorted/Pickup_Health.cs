using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Health : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().Heal(healAmount);
            print("PICKED UP: Healing player by -> + " + healAmount.ToString());
            Destroy(gameObject);
        }
    }
}
