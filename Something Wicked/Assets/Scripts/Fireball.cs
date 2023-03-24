using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public GameObject explosion;
    public float range = 20f;
    public float speed = 7.5f;
    public float damage = 0.75f;
    public float knockBack = 0.75f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        range -= speed * Time.deltaTime;
        if(range <= 0f)
            Destroy(gameObject);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.layer == 6)
        {
            Vector3 knockBackVector = (collision.transform.position - transform.position);
            collision.transform.Translate(knockBackVector.normalized * knockBack);
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.TakeDmg(damage, transform.rotation);
        }
        if (collision.gameObject.layer < 8 || collision.gameObject.layer > 10){
            Instantiate(explosion, transform.position, transform.rotation).transform.Rotate(0f, 0f, 180f);
            Destroy(gameObject);
        }
    }
}
