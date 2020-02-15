using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup_Upgrade : MonoBehaviour
{
    [SerializeField] private int upgradeAmount = 1;

    void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "Player")
        {
            coll.GetComponent<Player>().WeaponUpgrade(upgradeAmount);
            print("PICKED UP: Weapon Upgrade Level by -> + " + upgradeAmount.ToString());
            Destroy(gameObject);
        }
    }
}
