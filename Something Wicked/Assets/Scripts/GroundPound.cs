using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPound : MonoBehaviour
{
    public float duration = 0.5f;
    public float damage = 1.5f;
    public float knockBack = 2f;
    public GameObject dust;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(dust, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if(duration <= 0)
            Destroy(gameObject);
        duration -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.layer == 6)
        {
            Vector3 knockBackVector = (collision.transform.position - transform.position);
            collision.transform.Translate(knockBackVector.normalized * knockBack);
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            float angle = Vector3.Angle(Vector3.right, collision.transform.position - transform.position);
            if(collision.transform.position.y<0)
                angle *= -1;
            enemy.TakeDmg(damage, Quaternion.Euler(0,0,angle));
        }
    }
}
