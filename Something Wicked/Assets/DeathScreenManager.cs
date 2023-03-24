using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenManager : MonoBehaviour
{

    public Text CandyText;

    public void updateCandyText()
    {
        CandyText.text = "Total Candy Collected: " + StatManager.Stats.TotalCandyAmount;
    }
}
