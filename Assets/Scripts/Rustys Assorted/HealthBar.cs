using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    public GameObject target;
    // public float damping = 1f;
    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Bar");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    
        if(target != null){
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = rotation;
        //Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
        }

    }
    public void SetSize(float sizeNormalized){
        bar.localScale = new Vector3(sizeNormalized,1f);
    }
}
