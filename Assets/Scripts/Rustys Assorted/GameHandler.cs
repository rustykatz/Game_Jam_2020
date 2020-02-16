﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SocketIO;

public class GameHandler : MonoBehaviour
{
    SocketIOComponent socket;
    public int Score; 
    public bool gameRunning; 
    public bool diffTimerRun;
    public float diffTimer = 0f;

    public float difficulty =1; 

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI difficultyText; 

    public TextMeshProUGUI server1;
    public TextMeshProUGUI server2;

    public string s1;
    public string s2;

    public string msg;

    public int score;



    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        diffTimerRun = false; 
        diffTimer = 1; 
        score= 0;

        s1= "NONE";
        s2= "NONE";
        
        //for socket:
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("sendResults", getData); 
    }

    // Update is called once per frame
    void Update()
    {
        BigBrain(msg);
        UpdateText();
        if(gameRunning == true){
            diffTimerRun = true; 
        }
        else{ 
            diffTimerRun = false; 
        }
        if(diffTimerRun == true){
            StartCoroutine(DifficultyScale());
        }
    }

    void UpdateText(){
        scoreText.GetComponent<TextMeshProUGUI>().SetText("Score: " + score.ToString()); 
        difficultyText.GetComponent<TextMeshProUGUI>().SetText("Difficulty: " + difficulty.ToString()); 

        server1.GetComponent<TextMeshProUGUI>().SetText("Server MSG: " + s1);
        server2.GetComponent<TextMeshProUGUI>().SetText("OP Stat: " + s2); 

    }

    /*
        1) RECEIVES STRING 
        GetServerData(string s)

        2) MODIFIES GAME STATE

        3) SENDS NEXT CHOICES
        SendStateData([s,s1,s2,s3])
    */


  

    // Gets user choice from server
    
    public void getData(SocketIOEvent e){
       print(e.data);
       //to access individual parts, try:
       //e.data.ob1, ob2, ob3, ob4
    }
    
    // Prepares and sends object to server
    
    public void SendStateData(string c1,string c2, string c3, string c4){
        JSONObject choice = new JSONObject(JSONObject.Type.OBJECT);
        choice.AddField("op1:", c1);
        choice.AddField("op2:", c2);
        choice.AddField("op3:", c3);
        choice.AddField("op4:", c4);
        socket.Emit("requestVote", choice);
        
    }
    

    /*
        Big brain takes an input message s from the server and makes the 
        changes to the game.
    */

    public void BigBrain(string s){
        if(s == "test123"){
            s1 = s;
            // Do something
        }
        else if(s == "test456"){
            s1 =s;
            // Do something

        }

    }

    IEnumerator DifficultyScale(){
        diffTimer += 1;
        if(diffTimer >60){
            difficulty += 0.01f;
            diffTimer = 0;
            // print("Increasing difficulty!");
        }
        yield return new WaitForSeconds(1);
    }

    public void AddScore(int _score){
        score += _score;
    }

    public void AddDifficulty(float _diff){
        difficulty += _diff;
    }


    public void GetMsg(string _msg){
        msg = _msg;
    }

    
}
