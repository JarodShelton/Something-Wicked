using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text CandyText;

    // Start is called before the first frame update
    void Start()
    {
        CandyText.text = "Candy: " + StatManager.Stats.CandyAmount;
    }
    void Update(){
        //It was being wierd with updating only when collecting, so here we are :)
        CandyText.text = "Candy: " + StatManager.Stats.CandyAmount;
    }
    // Update is called once per frame
    public void UpdateUI()
    {
        CandyText.text = "Candy: " + StatManager.Stats.CandyAmount;
    }
}
