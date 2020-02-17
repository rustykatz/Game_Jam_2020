using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHealth : MonoBehaviour {

    [SerializeField] private HealthBarController healthBar; 
    private float totalHealth; 
    public float health =10;
    private float hperc; 
    AudioSource asource;
    public AudioClip takeDamage;

    // public GameHandler gameHandler;
    GameObject gameHandler;
    public int points = 5;
    public float diffFactor = 0.01f;

	void Awake(){
        asource = GetComponent<AudioSource>();
        totalHealth = health;
        healthBar.setMaxHealth(health);
        gameHandler = GameObject.FindGameObjectWithTag("GameHandler");
    }

    // Damage handling 
    public void TakeDamage(float damage)
    {
        if(health > 0.1){
            health -= damage;
            asource.PlayOneShot(takeDamage,1);
            if(health<=0){
                OnDeath(points);
                Destroy(gameObject);
                //hp.SetSize(0);
            }
            healthBar.setHealth(health);
        }
    }

    void OnDeath(int points){
        gameHandler.GetComponent<GameHandler>().AddScore(points);
        gameHandler.GetComponent<GameHandler>().AddDifficulty(diffFactor);
    }
}
