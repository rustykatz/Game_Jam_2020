using System.Collections;
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

    // Type to spawn
    public GameObject[] c1;
    public GameObject[] c2;
    public GameObject[] c3;
    public GameObject[] c4;


    // Spawn Locations
    public GameObject[] loc;

    // Choice Counter
    int cc =0;

    // public string[] choice1 = { "my", "pee", "balls"};
    // public string[] choice2 = { "my2", "pee2", "balls2"};
    // public string[] choice3 = { "my3", "pee3", "balls3"};
    // public string[] choice4 = { "my4", "pee4", "balls4"};

   
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
        //BigBrain(msg);
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
       // print(e.data.list[1]);
       // print(e.data.list[1].n ==6);

        // Spawn sub routine
        SpawnHandler(e.data.list[0].n, e.data.list[1].n, e.data.list[2].n, e.data.list[3].n);


        // Send next choices to server 
        SendStateData(c1[cc],c2[cc],c3[cc],c4[cc]);
        cc++; 
       //SendStateData("I","WANT","TO","DIE");
    }
    
    // Prepares and sends object to server
    
    public void SendStateData(string c1,string c2, string c3, string c4){
        JSONObject choice = new JSONObject(JSONObject.Type.OBJECT);
        choice.AddField("op1", c1);
        choice.AddField("op2", c2);
        choice.AddField("op3", c3);
        choice.AddField("op4", c4);
        socket.Emit("requestVote", choice);    
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

    /*
    It's 11:47 AM and spaghetti code is allowed. 
    I apologize to future me or whoever has to look at this mess of code
    for whatever reason. If I was a gud boi I would separate spawning 
    into another script i.e. using my SpawnAtLocation.cs but right now its
    chad programming time so lets just put it here and call it a day ;) 
    */
    
     public void SpawnHandler(float c1, float c2, float c3, float c4){
        // print("C1: " + c1.ToString());
        // print("C2: " + c2.ToString());
        // print("C3: " + c3.ToString());
        // print("C4: " + c4.ToString());

        // Gets Max of choices
        float maxC = Mathf.Max(c1,c2,c3,c4); 
        s1 = maxC.ToString();

        if(c1 == maxC){
            GameObject toSpawn = c1[cc];
        }
        else if(c2== maxC){
            GameObject toSpawn = c2[cc];
        }
        else if(c3== maxC){
            GameObject toSpawn = c3[cc];
        }
        else if(c4== maxC){
            GameObject toSpawn = c4[cc];
        }
        
        // Select random index 
        int rLoc = Random.Range(0,3);
        Instantiate(toSpawn, loc[rLoc].transform.position, Quaternity.identity);
        print("SPAWNING: " + toSpawn.ToString());


    }






     /*
    EVENTS 
    a) NEED To freeze game state until player moves

    1) Difficulty
        - Use Bool to freeze spawning, Difficulty factor 
    2) Choose Player weapon 
        - 4 choices
        1) Buster
        2) Excalibur
        3) Plank
        4) Scythe
    3) Choose which Enemies to Spawn
        1)
        2)
        3)
        4) 
    4) Choose Natural Disaster
        1)
        2)
        3)
        4)
    5) Choose Enemies to Spawn 
        1)
        2)
        3)
        4)
    6) Choose Boss
        1)
        2)
        3)
        4)
    */
    
}
