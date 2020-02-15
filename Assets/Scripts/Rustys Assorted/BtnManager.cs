using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnManager : MonoBehaviour {

    /*  BtnManager handles all of the UI Button navigations
    */
    //  New Game: Load and Initialize level 1
    public void ChangeScene(string LoadLevel){
        try{
            Debug.Log("ATTEMPT: Load Level -> " + LoadLevel);
            SceneManager.LoadScene(LoadLevel);
            Debug.Log("SUCCESS: Load Level -> " + LoadLevel);
        }
        catch{
            Debug.Log("ERROR: Failed to Load Level -> " + LoadLevel);
        }
    }




    //  Exit application
    public void ExitGameBtn(){
        Debug.Log("Exiting application.");
        Application.Quit();
    }

}