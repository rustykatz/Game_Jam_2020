using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootEnemy : BasicEnemy
{
    // Inherits functions from BasicEnemy script
    public float shootDistance;
    public float rateOfFire;
    public float lastShotTime;
    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        // Inherits the setup fxn from BasicEnemy Script
        NavAgentSetup();
    }

    // Update is called once per frame
    void Update()
    {
        InRange();
       
    }

    void InRange()
    {
        // Check if player in enemy attack range 
        if (Vector3.Distance(transform.position, target.transform.position) >= shootDistance)
        {
            // Inherited from BasicEnemy
            CheckRange();

        }
        // In range shoot
        else
        {
            navAgent.destination = transform.position;
            if (lastShotTime + rateOfFire < Time.time)
            {
                ShootProjectile();
                lastShotTime = Time.time;
            }
        }
    }


    void ShootProjectile()
    {
        GameObject bullet = Instantiate(projectile, transform.position, transform.rotation);
        bullet.transform.LookAt(target.transform, Vector3.up);

    }
}
