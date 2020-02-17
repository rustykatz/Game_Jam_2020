using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBar : MonoBehaviour
{

    public Transform camera;

    // Update is called once per frame
    void Start() {
        camera = Camera.main.transform;
    }

    void LateUpdate() {
        transform.LookAt(transform.position + camera.forward);
    }
}
