using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyPileController : MonoBehaviour
{
    public int numberOfCandies = 5;
    public GameObject candyObject;

    public float candyRange;
    public float activationDistance = 1;

    public GameObject activationEffect;

    Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) <= activationDistance){
            spawnCandies();
        }
    }
    void spawnCandies(){
        if(activationEffect)
            Instantiate(activationEffect, transform.position, Quaternion.identity);
            
        for (int i = 0; i < numberOfCandies; i++)
        {
            Vector2 pilePos = transform.position;
            Vector2 candyPos = pilePos + (Random.insideUnitCircle * candyRange);
            Instantiate(candyObject, candyPos, Quaternion.identity);
        }
        AudioManager.PlayMusic("Candy Pile");
        Destroy(gameObject);
    }
}
