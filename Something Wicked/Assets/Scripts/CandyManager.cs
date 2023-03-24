using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyManager : MonoBehaviour
{
    //Picks random sprite when spawned
    public Sprite[] candySprites;

    //Amount this single candy gives
    public int candyAmount = 1;

    public float attractionDistance = 5f;
    public float attractionForce = 10f;
    public float pickupDistance = 1.5f;

    Player player;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();

        //Give candy random rotation
        Vector3 rot = transform.eulerAngles;
        rot.z = Random.Range(0f, 360f);
        transform.eulerAngles = rot;

        //Pick random sprite
        GetComponent<SpriteRenderer>().sprite = candySprites[Random.Range(0, candySprites.Length)];   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, player.transform.position) <= attractionDistance){
            Vector3 direction = player.transform.position - transform.position;
            direction = direction.normalized;
            rb2d.AddForce(direction * attractionForce);
        }

    }

    void Update(){

         if(Vector2.Distance(transform.position, player.transform.position) <= pickupDistance){
             //particle maybe???
             StatManager.Stats.changeCandyAmount(candyAmount);
             AudioManager.PlayMusic("Candy");
             Destroy(gameObject);
         }
    }
}
