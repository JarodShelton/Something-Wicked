using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float acceleration = 1f;
    public float drag = 2f;
    public float invincibilityDuration = 1f;
    public bool isInvincible = false;
    //public float maxVelocity = 10f;
    public Vector3 velocity;

    ChildManager children;
    bool movingToLocation = false;
    Vector3 newLocation = Vector3.zero;
    float linearSpeed = 1f;
    float waitTimer = 0f;
    float iTimer = 0f;
    float failTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        children = FindObjectOfType<ChildManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, -1.1f);
        if(iTimer > 0)
            iTimer -= Time.deltaTime;
        if(waitTimer <= 0){
            if(!movingToLocation)
                move();
            else
                moveLinearly();
        }else{
            waitTimer -= Time.deltaTime;
        }
        
    }

    void move(){
        float xAcceleration = 0f;
        float yAcceleration = 0f;
        if(Input.GetKey(KeyCode.A))
            xAcceleration -= acceleration;
        if(Input.GetKey(KeyCode.D))
            xAcceleration += acceleration;
        if(Input.GetKey(KeyCode.S))
            yAcceleration -= acceleration;
        if(Input.GetKey(KeyCode.W))
            yAcceleration += acceleration;
        velocity.x = velocity.x + (xAcceleration - velocity.x*drag)*Time.deltaTime;
        velocity.y = velocity.y + (yAcceleration - velocity.y*drag)*Time.deltaTime;

        transform.Translate(velocity * Time.deltaTime);
    }

    void moveLinearly(){
        float moveDistance = linearSpeed * Time.deltaTime;
        //Debug.Log(transform.position + " | " + newLocation);
        //Debug.Log(transform.position + " | " + newLocation + "\n" +Vector3.Distance(transform.position, newLocation) + " | " + moveDistance);
        if(Vector3.Distance(transform.position, newLocation) <= moveDistance || failTimer <= 0){
            if(failTimer>0)
                transform.position = newLocation;
            movingToLocation = false;
        }else{
            failTimer -= Time.deltaTime;
            Vector3 direction =  newLocation - transform.position;
            transform.Translate(direction.normalized * moveDistance);
        }
    }

    public void GiveInvulnerability(float duration){
        if(duration > iTimer)
            iTimer = duration;
    }

    public bool isMovingToLocation(){
        return movingToLocation;
    }

    public void GoToPosition(Vector3 newPosition, float speed){
        movingToLocation = true;
        newLocation = newPosition;
        newLocation.z = transform.position.z;
        linearSpeed = speed;
        velocity = Vector3.zero;
        failTimer = 0.7f;
    }

    public void Wait(float duration){
        waitTimer = duration;
    }

    public void ApplyForce(Vector3 force){
        velocity += force;
    }

    public Vector3 GetPosition(){
        return transform.position;
    }
    void OnCollisionEnter2D(Collision2D collider){
        if(children.GetLeader().GetChildState() != ChildState.Grapple && children.GetLeader().GetChildState() != ChildState.Dash){
            if((collider.gameObject.layer == 6 || collider.gameObject.layer == 12) && iTimer <=0 && !isInvincible){
                children.KillLeader();
                children.Flash(invincibilityDuration, 0.075f);
                iTimer = invincibilityDuration;
            }
        }
        
    }
}
