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

    GameObject toSpawn; 
    // Spawn Locations
    public GameObject[] loc;

    // Choice Counter
    int cc =0;

    public GameObject[] CoolSwords; 

    public GameObject sword1;
    public GameObject sword2;
    public GameObject sword3;
    public GameObject sword4;


    /*
    ORDER OF OP
    1) Difficulty
    2) Weapon Select
    3) Spawn-1
    4) Spawn-2
    5) Change Weapon
    6) Spawn-3
    7) Spawn- TITAN FORCED 

    Blue 
    Red
    Green
    Yellow

    Hobgoblin
    undead
    troll warlock
    flesh amalgm 
    john from marketing 


    Titan

                                20            15               15              15              15             15                      15
    */
    string[] choice1 = { "Easy",      "Buster",    "Hobgoblin",     "Hobgoblin",     "Buster",     "John from Marketing", "Destroyer of Worlds"};
    string[] choice2 = { "Medium",    "Excalibur", "Undead",        "Undead",        "Excalibur",  "Undead",              "Biggest Boss"};
    string[] choice3 = { "Hard",      "Plank",     "Troll Warlock", "Troll Warlock", "Plank",      "Troll Warlock",       "Deathbringer"};
    string[] choice4 = { "Pro Gamer", "Scythe",    "Flesh Amalgm",  "Flesh Amalgm",  "Sycthe",     "Flesh Amalgm",        "The Unit" };

    bool begin; 
   
    // Start is called before the first frame update
    void Start()
    {
        gameRunning = true;
        diffTimerRun = false; 
        diffTimer = 1; 
        score= 0;

        s1= "NONE";
        s2= "NONE";
        //sss.SetActive(true);
        //for socket:
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        socket.On("sendResults", getData); 

        sword1.SetActive(false);
        sword2.SetActive(false);
        sword3.SetActive(false);
        sword4.SetActive(false);
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


        if(Input.GetKeyDown(KeyCode.K)){
            SendStateData(choice1[cc],choice2[cc],choice3[cc],choice4[cc]);
            print("SENDING THE DATA ");

            if(cc >=6){
                cc=6;
            }

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
        try{
            SpawnHandler(e.data.list[0].n, e.data.list[1].n, e.data.list[2].n, e.data.list[3].n);
        }
        catch{
            print("error lool");
        }
        cc++; 
        // Send next choices to server 
        SendStateData(choice1[cc],choice2[cc],choice3[cc],choice4[cc]);
        print("getData IN CYCLE");
        
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
            difficulty += 0.1f;
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
    
     public void SpawnHandler(float op1, float op2, float op3, float op4){
        // print("C1: " + c1.ToString());
        // print("C2: " + c2.ToString());
        // print("C3: " + c3.ToString());
        // print("C4: " + c4.ToString());

        // Gets Max of choices
        float maxC = Mathf.Max(op1,op2,op3,op4); 
        s1 = maxC.ToString();
        int cidx =0;
        string choice = "";

        if(op1 == maxC){
            toSpawn = c1[cc];
            choice = choice1[cc];
            cidx = 0;
        }
        else if(op2== maxC){
            toSpawn = c2[cc];
            choice = choice2[cc];
            cidx = 1;
        }
        else if(op3== maxC){
            toSpawn = c3[cc];
            choice = choice3[cc];
            cidx= 2;
        }
        else if(op4== maxC){
            toSpawn = c4[cc];
            choice = choice4[cc];
            cidx= 3;
        }
        
        int numSpawn = 0; 
        int rUnit = Random.Range(1,3);
        numSpawn = (int)Mathf.Floor(2* difficulty + rUnit); 
        // Round Logic
        if(cc== 0){
            // SELECT DIFFICULTY
            difficulty += cidx; 
            print("Difficulty set -> "+ difficulty.ToString());
           
        }
        else if(cc==1){
            // Select Weapon
            //swords[cidx].SetActive(true);
            if(cidx==0){
                sword1.SetActive(true); 
            }
            else if(cidx==1){
                sword2.SetActive(true);
            }
            else if(cidx ==2){
                sword3.SetActive(true);
            }
            else{
                sword4.SetActive(true);
            }

            print("IN SWORD");
            print(cidx);

        }
        else if(cc==2){
            // Select Enemies
            for(int i=0; i<= numSpawn; i++){
                Instantiate(toSpawn, loc[cidx].transform.position, Quaternion.identity);
                print("SPAWNING "+ choice.ToString() + "#: "+ i.ToString());
            }
        }
        else if(cc==3){
            // Select Enemies
            for(int i=0; i<= numSpawn; i++){
                Instantiate(toSpawn, loc[cidx].transform.position, Quaternion.identity);
                print("SPAWNING "+ choice.ToString() + "#: "+ i.ToString());
            }
        }
        else if(cc==4){
            
            // Select Weapon
            if(cidx==0){
                weaponOff();
                sword1.SetActive(true);
            }
           else if(cidx==1){
                weaponOff();
                sword2.SetActive(true);
            }
            else if(cidx ==2){
                weaponOff();
                sword3.SetActive(true);
            }
            else if(cidx ==3){
                weaponOff();
                sword4.SetActive(true);
            }
        }
        else if(cc==5){
            // Select Enemies
            for(int i=0; i<= numSpawn; i++){
                Instantiate(toSpawn, loc[cidx].transform.position, Quaternion.identity);
                print("SPAWNING "+ choice.ToString() + "#: "+ i.ToString());
            }
        }
        // Anththing beyond level 6 will onlt spawn bosses 
        else if(cc>=6){
            // Select Boss
            Instantiate(toSpawn, loc[cidx].transform.position, Quaternion.identity);

        }


        //Instantiate(toSpawn, loc[cidx].transform.position, Quaternion.identity);
    }


    void weaponOff(){
        sword1.SetActive(false);
        sword2.SetActive(false);
        sword3.SetActive(false);
        sword4.SetActive(false);
    }
    
}
