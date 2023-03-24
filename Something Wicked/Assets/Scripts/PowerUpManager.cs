using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    public int weaponLevel = 0; // weapon levels range from 0-4

    Dictionary<string, float[]> healthValues;

    // Start is called before the first frame update
    void Start()
    {
        float[] skeletonHealth = new float[] {3, 2, 1, 1, 1};   // Skeleton health
        float[] zombieHealth = new float[] {5, 4, 3, 2, 1};    // Zombie health
        float[] ghostHealth = new float[] {3, 2, 1, 1, 1};    // Ghost health
        float[] werewolfHealth = new float[] {10, 8, 6, 5, 4};  // Werewolf health

        healthValues = new Dictionary<string, float[]>();
        healthValues.Add("Skeleton", skeletonHealth);
        healthValues.Add("Zombie", zombieHealth);
        healthValues.Add("Ghost", ghostHealth);
        healthValues.Add("Werewolf", werewolfHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetHealth(string enemyType){
        if(healthValues.ContainsKey(enemyType))
            return healthValues[enemyType][weaponLevel];
        return -1;
    }
}
