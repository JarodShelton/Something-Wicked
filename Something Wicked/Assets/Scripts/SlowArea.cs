using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowArea : MonoBehaviour
{

    public float duration = 10f;
    public float slowFactor = 0.7f;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(duration <= 0)
            Destroy(gameObject);
        duration -= Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collision){
        if(collision != null && collision.gameObject.layer == 6)
            collision.gameObject.GetComponent<EnemyManager>().ModifySpeed(slowFactor);
    }

    void OnTriggerExit2D(Collider2D collision){
        if(collision != null && collision.gameObject.layer == 6)
            collision.gameObject.GetComponent<EnemyManager>().ResetSpeed();
    }
}
