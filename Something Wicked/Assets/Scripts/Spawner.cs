using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnInterval = 15f;
    public float intervalFraction = 0.75f;
    public int initialEnemies = 0;
    public int spawnCap = 50;
    public float dist = 75.0f;
    public float spawnHeight = 10f;
    public float spawnWidth = 10f;

    protected Player player;
    protected int spawnAmount = 1;
    protected float spawnTimer;
    protected float noiseyspawnInterval;
    protected int totalEnemies;

    protected TimeManager timer;

    protected void Start()
    {
        player = FindObjectOfType<Player>();
        spawnTimer = 0;
        timer = FindObjectOfType<TimeManager>();

        noiseyspawnInterval = Random.Range(0f, spawnInterval);
        spawnEnemy(initialEnemies);
        totalEnemies = initialEnemies;
    }

    protected void Update()
    {
        if (TimeManager.time.isNight())
        {
            spawnTimer += Time.deltaTime;
            //if(Vector3.Distance(player.GetPosition(), transform.position) < dist)
            //{
                if (spawnTimer > noiseyspawnInterval)
                {
                    if (totalEnemies <= spawnCap)
                    {
                        spawnEnemy(spawnAmount);
                        totalEnemies += spawnAmount;
                    }
                    noiseyspawnInterval = Random.Range(0f, spawnInterval * Mathf.Pow(intervalFraction, timer.currentNight - 1));
                    spawnTimer = 0;
                }
            //}
        }
        else
        {
            totalEnemies = 0;
        }
    }

    public void death()//create spawner manager to manage total enemies
    {
        totalEnemies -= 1;
    }

    public virtual void spawnEnemy(int spawnAmount)
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            Instantiate(enemy, randomPosition(), Quaternion.identity);
        }
    }

    public Vector3 randomPosition()
    {
        Vector3 spawnPos = this.gameObject.transform.position;
        Vector2 randomPos = new Vector2(Random.Range(spawnPos.x - spawnWidth / 2, spawnPos.x + spawnWidth / 2), Random.Range(spawnPos.y - spawnHeight / 2, spawnPos.y + spawnHeight / 2));
        return randomPos;
    }
}
