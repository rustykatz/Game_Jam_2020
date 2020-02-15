using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour {

    // Allows us to see in private in inspector.
    [SerializeField] private string loadLevel;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            SceneManager.LoadScene(loadLevel);

        }
    }


}
