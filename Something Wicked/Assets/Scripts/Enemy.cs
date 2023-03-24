using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2.0f;
    public float noise = 0.25f;
    public float feedTime = 5.0f;
    public GameObject corpse;
    protected Player player;
    protected float speedM;
    private Vector3 velocity;
    private float cooldown = 0.0f;
    private bool sleeping = false;
    protected SpriteRenderer render;
    float cameraHeight = 0f;

    // Start is called before the first frame update
    protected void Start()
    {
        speed *= Random.Range(1.0f - noise, 1.0f + noise);
        speedM = speed;
        player = FindObjectOfType<Player>();
        render = GetComponent<SpriteRenderer>();
        cameraHeight = transform.position.z;
    }

    // Update is called once per frame
    protected void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, cameraHeight);
        Vector3 direction = player.GetPosition()- transform.position;
        if(direction.x < 0)
            render.flipX = true;
        else
            render.flipX = false;
        
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else if (!sleeping)
        {
            move();
        }
        if (!TimeManager.time.isNight())
        {
            //death
            Destroy(this.gameObject);
        }
    }
    protected virtual void move()
    {
        Vector3 direction = player.GetPosition() - transform.position;
        direction.Normalize();
        transform.Translate(direction * speedM * Time.deltaTime);
    }

    public void ModifySpeed(float mult)
    {
        speedM *= mult;
    }

    public void ResetSpeed()
    {
        speedM = speed;
    }

    public void GoToSleep()
    {
        sleeping = true;
    }

    public void WakeUp()
    {
        sleeping = false;
    }

}
