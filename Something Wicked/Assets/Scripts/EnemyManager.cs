using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemy;

    Enemy skeleton;
    Enemy zombie;
    Ghost ghost;
    Were were;
    Collider2D col;

    // This implementation is kind of cancerous but it works and I don't have time to do it cleanly

    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<Collider2D>();

        switch(gameObject.tag){
            case "Skeleton":
                skeleton = GetComponent<Enemy>();
                break;
            case "Zombie":
                zombie = GetComponent<Enemy>();
                break;
            case "Ghost":
                ghost = GetComponent<Ghost>();
                break;
            case "Werewolf":
                were = GetComponent<Were>();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ModifySpeed(float multiplier){
        switch(gameObject.tag){
            case "Skeleton":
                skeleton.ModifySpeed(multiplier);
                break;
            case "Zombie":
                zombie.ModifySpeed(multiplier);
                break;
            case "Ghost":
                ghost.ModifySpeed(multiplier);
                break;
            case "Werewolf":
                were.ModifySpeed(multiplier);
                break;
        }
    }

    public void ResetSpeed()
    {
        switch(gameObject.tag){
            case "Skeleton":
                skeleton.ResetSpeed();
                break;
            case "Zombie":
                zombie.ResetSpeed();
                break;
            case "Ghost":
                ghost.ResetSpeed();
                break;
            case "Werewolf":
                were.ResetSpeed();
                break;
        }
    }

    public void GoToSleep()
    {
        col.isTrigger = true;
        switch(gameObject.tag){
            case "Skeleton":
                skeleton.GoToSleep();
                break;
            case "Zombie":
                zombie.GoToSleep();
                break;
            case "Ghost":
                ghost.GoToSleep();
                break;
            case "Werewolf":
                were.GoToSleep();
                break;
        }
    }

    public void WakeUp()
    {
        col.isTrigger = false;
        switch(gameObject.tag){
            case "Skeleton":
                skeleton.WakeUp();
                break;
            case "Zombie":
                zombie.WakeUp();
                break;
            case "Ghost":
                ghost.WakeUp();
                break;
            case "Werewolf":
                were.WakeUp();
                break;
        }
    }
}
