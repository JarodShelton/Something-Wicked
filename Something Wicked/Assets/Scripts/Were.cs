using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Were : Enemy
{
    bool charging = true;
    private float range;
    public Vector3 angle;
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(2);
    }
    
    protected override void move()
    {
        Vector3 direction = player.GetPosition() - transform.position;
        direction.Normalize();
        float distance = Vector3.Distance(player.GetPosition(), transform.position);
        if (distance >10 && distance <15)
        {
            transform.Translate((Quaternion.AngleAxis(180, Vector3.forward) * direction) * speedM * Time.deltaTime);
        }
        else if (distance >= 15 && distance <=20)
        {
            charging = true;
            ExampleCoroutine();
            float angle1 = Vector3.Angle(Vector3.right, direction);
            if (direction.y < 0)
                angle1 *= -1;
            angle = new Vector3(0, 0, angle1);
            //direction = direction.normalized * 0.3f;
            //direction = direction + transform.position;
            range = 20f;
        }
        else
        {
            transform.Translate(direction * speedM * Time.deltaTime);
        }
    }
    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected void Update()
    {
        if(charging)
        {
            range -= 10f * Time.deltaTime;
            if (range <= 0f)
                charging = false;
            transform.Translate(angle * 10f * Time.deltaTime);
        }
        else
        {
            base.Update();
        }
    }
}
