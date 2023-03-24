using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildTrafficker : Spawner
{
    public GameObject enemy2;
    public GameObject enemy3;
    public GameObject enemy4;

    public override void spawnEnemy(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            int type = Random.Range(1, 5);
            GameObject baby = enemy;
            if(type == 1)
            {
                baby = enemy;
            }
            else if (type == 2)
            {
                baby = enemy2;
            }
            else if (type == 3)
            {
                baby = enemy3;
            }
           else if (type == 4)
           {
                baby = enemy4;
           }
           Instantiate(baby, randomPosition(), Quaternion.identity);
        }
    }

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        base.Update();
    }
}
