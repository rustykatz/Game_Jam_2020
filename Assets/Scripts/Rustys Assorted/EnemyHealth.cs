using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {

    [SerializeField] private HealthBar hp; 
    private float totalHealth; 
    public float health =10;
    private float hperc; 
  
    // public GameHandler gameHandler;
    GameObject gameHandler;
    public int points = 5;
    public float diffFactor = 0.01f;

	void Awake(){
        totalHealth = health;
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler");
    }

    // Damage handling 
    public void TakeDamage(float damage)
    {
        if(health > 0.1){
            health -= damage;
            if(health<=0){
                OnDeath(points);
                Destroy(gameObject);
                //hp.SetSize(0);
            }
        }
    }

    void OnDeath(int points){
        gameHandler.GetComponent<GameHandler>().AddScore(points);
        gameHandler.GetComponent<GameHandler>().AddDifficulty(diffFactor);

    }
}
