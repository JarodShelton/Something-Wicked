using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public float duration = 0.2f;
    public float damage = 1f;
    public float knockBack = 1.5f;
    // Start is called before the first frame update
    protected void Start()
    {
        
    }

    // Update is called once per frame
    protected void Update()
    {
        if(duration <= 0)
            Destroy(gameObject);
        duration -= Time.deltaTime;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.layer == 6/*|| collider.gameObject.layer == 12*/)
        {
            Vector3 knockBackVector = (collision.transform.position - transform.position);
            collision.transform.Translate(knockBackVector.normalized * knockBack);
            EnemyHealth enemy = collision.gameObject.GetComponent<EnemyHealth>();
            enemy.TakeDmg(damage, transform.rotation);
        }
    }
}
