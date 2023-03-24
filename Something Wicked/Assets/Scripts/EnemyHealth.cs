using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public GameObject deathExplosion;
    public GameObject hurtEffect;
    //private GameObject timeMgmt;
    public float invincibilityDuration = 0.2f;
    public float shakeAmplitude = 0.4f;
    public float shakeDuration = 0.2f;
    float hp = 1f;          //enemy hp
    float iFrameTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        PowerUpManager pManager = FindObjectOfType<PowerUpManager>();
        hp = pManager.GetHealth(gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        if(iFrameTimer > 0)
            iFrameTimer -= Time.deltaTime;
        if (hp <= 0){
            if(deathExplosion != null)
                Instantiate(deathExplosion, transform.position, Quaternion.identity);
            switch(gameObject.tag){
            case "Skeleton":
                AudioManager.PlayMusic("Kill");
                break;
            case "Ghost":
                AudioManager.PlayMusic("Kill");
                break;
            default:
                AudioManager.PlayMusic("Big Kill");
                break;
            }
            Vector3 shakeVector = FindObjectOfType<Player>().GetPosition() - transform.position;
            FindObjectOfType<CameraScript>().Shake(shakeVector, shakeAmplitude, shakeDuration);
            Destroy(gameObject);
        }
    }

    public void TakeDmg(float dmg)
    {
        switch(gameObject.tag){
            case "Skeleton":
                AudioManager.PlayMusic("Bone Hit");
                break;
            case "Ghost":
                AudioManager.PlayMusic("Ghost Hit");
                break;
            default:
                AudioManager.PlayMusic("Flesh Hit");
                break;
        }
        if(iFrameTimer <= 0){
            hp = hp - dmg;
            //Debug.Log("Took "+dmg+" Damage");
            iFrameTimer = invincibilityDuration;
        }
        if(!IsDead() && hurtEffect != null)
            Instantiate(hurtEffect, transform.position, Quaternion.identity);
    }

    public void TakeDmg(float dmg, Quaternion direction)
    {
        switch(gameObject.tag){
            case "Skeleton":
                AudioManager.PlayMusic("Bone Hit");
                break;
            case "Ghost":
                AudioManager.PlayMusic("Ghost Hit");
                break;
            default:
                AudioManager.PlayMusic("Flesh Hit");
                break;
        }
        if(iFrameTimer <= 0){
            hp = hp - dmg;
            //Debug.Log("Took "+dmg+" Damage");
            iFrameTimer = invincibilityDuration;
        }
        if(!IsDead() && hurtEffect != null)
            Instantiate(hurtEffect, transform.position, direction);
    }

    public bool IsDead(){
        return hp <= 0;
    }
}
