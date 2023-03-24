using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VampireTrigger : MonoBehaviour
{
    public LayerMask mask;

    float duration = 0.2f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(duration <= 0)
            Destroy(gameObject);
        duration -= Time.deltaTime;
    }

    public void SetLocation(Vector3 location){
        transform.position = location;
    }

    public GameObject GetClosestEnemy(){
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1f, mask);
        GameObject closest = null;
        float distance = 10000f;
        foreach(Collider2D col in enemies){
            GameObject obj = col.gameObject;
            if(closest == null){
                closest = obj;
                distance = Vector3.Distance(obj.transform.position, closest.transform.position);
            }else{                        
                float tempDistance = Vector3.Distance(obj.transform.position, closest.transform.position);
                if(tempDistance < distance){
                    closest = obj;
                    distance = tempDistance;
                }
            }
        }
        return closest;
    }

}
