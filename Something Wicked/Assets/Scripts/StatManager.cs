using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatManager : MonoBehaviour
{ 
    //Honestly this is a weird way of doing it, and definitely not the best way,but 
    //it works decently well, and can be used to bring data between scenes. game jam stuff i guess
    public static StatManager Stats;

    //Data all persists through levels
    public int CandyAmount = 0;

    public int TotalCandyAmount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        //Let the gameobject persist through scenes
        DontDestroyOnLoad(gameObject);
        //Check if the control instance is null
        if (Stats == null)
        {
            //This instance becomes the only stat instance
            Stats = this;
        }
        //Otherwise check if the control instance is not this one
        else if (Stats != this)
        {
            //In case there is a different instance destroy this one.
            Destroy(gameObject);
        }
    }
    public void changeCandyAmount(int amount){

        //Debug.Log("Candy amount changed by " + amount);
        if(FindObjectOfType<UIManager>())
            FindObjectOfType<UIManager>().UpdateUI();
        CandyAmount += amount;
        TotalCandyAmount += amount;
    }
}
//https://youtu.be/dQw4w9WgXcQ