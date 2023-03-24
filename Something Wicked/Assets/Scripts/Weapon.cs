using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject attackHitbox;
    public GameObject weapon;
    public Child child;
    public Animator weaponAnimator;
    public float range = 0.5f;
    public float recoilForce = 10f;
    //public float swingDuration = 0.2f;

    float shakeAmplitude = 0.2f;
    float shakeDuration = 0.1f;
    bool swing = false;
    //float swingTimer = 0f;
    float rangeOfMotion = 150f;
    Player player;
    Crosshair crosshair;
    CameraScript cam;
    PowerUpManager pManager;
    // Start is called before the first frame update
    void Start()
    {
        pManager = FindObjectOfType<PowerUpManager>();
        player = FindObjectOfType<Player>();
        crosshair = FindObjectOfType<Crosshair>();
        cam = FindObjectOfType<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<MenuManager>().GetComponent<MenuManager>().gamePaused)
        {
            weaponAnimator.SetInteger("Level", pManager.weaponLevel);

            Vector3 target = child.GetChildState() == ChildState.Follower ? player.GetPosition() : crosshair.GetPosition();
            float angle = Vector3.Angle(Vector3.right, target - transform.parent.position);
            float turnAngle = (rangeOfMotion * angle / 180) - (rangeOfMotion / 2);
            Vector3 targetRelative = target - transform.parent.position;
            //Debug.Log(targetRelative.y + " " + angle);

            if (targetRelative.y > 0 && angle > 15f && angle < 165f)
            {
                turnAngle = -(turnAngle + 180f);
                transform.position = new Vector3(-0.235f, -0.32f, 0.1f);
                transform.position += transform.parent.position;
            }
            else
            {
                transform.position = new Vector3(0.235f, -0.32f, -0.1f);
                transform.position += transform.parent.position;
            }

            if (!swing)
            {
                transform.rotation = Quaternion.Euler(0, 0, 5f - turnAngle);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 250f - turnAngle);
            }
        }
    }

    public void attack()
    {
        AudioManager.PlayMusic("Swing");
        swing = !swing;
        Vector3 direction = crosshair.GetPosition() - player.GetPosition();

        float angle = Vector3.Angle(Vector3.right, direction);
        if (crosshair.GetLocalPosition().y < 0)
            angle *= -1;

        direction = direction.normalized * range;
        direction = direction + player.GetPosition();

        direction.z = -2;
        GameObject melee = Instantiate(attackHitbox, direction, Quaternion.Euler(0, 0, angle)) as GameObject;
        if (swing)
        {
            melee.GetComponent<SpriteRenderer>().flipY = true;
        }

        melee.transform.parent = transform.parent;

        float angleRadians = angle * Mathf.Deg2Rad;
        Vector3 recoilVector = new Vector3(Mathf.Cos(angleRadians) * recoilForce, Mathf.Sin(angleRadians) * recoilForce, 0);
        player.ApplyForce(recoilVector);
        cam.Shake(angle, shakeAmplitude, shakeDuration);
    }
}
