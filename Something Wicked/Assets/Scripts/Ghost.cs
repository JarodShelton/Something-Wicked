using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy
{
    float ball;
    public GameObject projectile;
    protected override void move()
    {
        Vector3 direction = player.GetPosition() - transform.position;
        direction.Normalize();
        float distance = Vector3.Distance(player.GetPosition(), transform.position);
        if (distance < 5)
        {
            transform.Translate((Quaternion.AngleAxis(180, Vector3.forward) * direction) * speedM * Time.deltaTime);
        }
        else if (distance < 10)
        {
            transform.Translate((Quaternion.AngleAxis(-90, Vector3.forward) * direction) * speedM*2 * Time.deltaTime);
            ball -= Time.deltaTime;
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
        ball = Random.Range(0f, 5f);
    }

    // Update is called once per frame
    protected void Update()
    {
        Vector3 direction = player.GetPosition()- transform.position;
        base.Update();
        if (ball<=0)
        {
            float angle = Vector3.Angle(Vector3.right, direction);
            if (direction.y < 0)
                angle *= -1;
            direction = direction.normalized * 0.3f;
            direction = direction + transform.position;
            //direction.z = -2;
            Instantiate(projectile, direction, Quaternion.Euler(0, 0, angle));

            ball = Random.Range(0f, 5f);
        }
    }
}
