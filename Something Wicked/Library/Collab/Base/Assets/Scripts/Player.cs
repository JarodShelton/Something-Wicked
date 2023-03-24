using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float acceleration = 1f;
    public float drag = 2f;
    //public float maxVelocity = 10f;
    public Vector3 velocity;

    bool movingToLocation = false;
    Vector3 newLocation = Vector3.zero;
    float linearSpeed = 1f;
    float waitTimer = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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
        if(Vector3.Distance(transform.position, newLocation) < 0.15)
            movingToLocation = false;
        else{
            Vector3 direction =  newLocation - transform.position;
            transform.Translate(direction.normalized * linearSpeed * Time.deltaTime);
        }
    }

    public void GoToPosition(Vector3 newPosition, float speed){
        movingToLocation = true;
        newLocation = newPosition;
        linearSpeed = speed;
        velocity = Vector3.zero;
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
}
