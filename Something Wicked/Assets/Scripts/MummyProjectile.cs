using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MummyProjectile : MonoBehaviour
{
    public GameObject splashEffect;
    public float range = 10f;
    public float speed = 12.5f;
    Vector3 endpoint = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, endpoint);
        if(distance <= 0.3f){
            Instantiate(splashEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
        Vector3 direction = endpoint - transform.position;
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
        //transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    public void SetEndpoint(Vector3 endpoint){
        this.endpoint = endpoint;
    }
}
