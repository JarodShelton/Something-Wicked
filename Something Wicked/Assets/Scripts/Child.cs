using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    public ChildType type = ChildType.TestChild;
    public Weapon weapon;
    public float basicAttackCooldown = 0.3f;
    public GameObject specialProjectile;
    public GameObject childBlood;
    public GameObject childFlesh;

    Player player;
    ChildManager manager;
    ChildState state = ChildState.Stationary;
    CameraScript cam;
    Crosshair crosshair;
    Rigidbody2D body;
    Animator anim;
    SpriteRenderer childRenderer;
    float playerDistance;
    float pickupDistance = 3f;
    float followDistance = 2.5f;
    float basicAttackTimer = 0f;
    float sepcialAttackTimer = 0f;
    float transitionSpeed = 25f;
    float transitionShakeAmplitude = 0.2f;
    float transitionShakeDuration = 0.1f;
    float vampireEndLag = 0.5f;
    float grappleTimer = 0f;
    Vector3 vampireDashLocation = Vector3.zero;
    GameObject vampireTarget = null;
    bool isVisible = true;
    float flashDuration = 0f;
    float flashTimer = 0f;
    float flashFrequency = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<ChildManager>();
        crosshair = FindObjectOfType<Crosshair>();
        cam = FindObjectOfType<CameraScript>();
        childRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<MenuManager>().GetComponent<MenuManager>().gamePaused)
        {
            flash();
            playerDistance = Vector3.Distance(transform.position, player.GetPosition());
            if (anim != null)
                animate();
            if (type == ChildType.Vampire)
                VampireActions();
            if (state == ChildState.Stationary)
                wait();
            else
                move();
            if (sepcialAttackTimer > 0)
                sepcialAttackTimer -= Time.deltaTime;
            if (basicAttackTimer > 0)
                basicAttackTimer -= Time.deltaTime;
            if (state == ChildState.Leader && Input.GetMouseButton(1) && sepcialAttackTimer <= 0) // If child is the leader and right mouse button is pressed
                specialAttack();
            if (state == ChildState.Leader && Input.GetMouseButton(0) && basicAttackTimer <= 0)
            {// If child is the leader and right mouse button is pressed
                weapon.attack();
                basicAttackTimer = basicAttackCooldown;
            }
        }
    }

    void animate()
    {
        Vector3 target = state == ChildState.Follower ? player.GetPosition() : crosshair.GetPosition();
        float angle = Vector3.Angle(Vector3.right, target - transform.position);
        Vector3 targetRelative = target - transform.position;
        if (targetRelative.y < 0)
            angle = 360 - angle;
        anim.SetFloat("Direction", angle);

        switch (state)
        {
            case ChildState.Leader:
                if (player.velocity.magnitude > 3)
                    anim.SetBool("Is Moving", true);
                else
                    anim.SetBool("Is Moving", false);
                break;
            case ChildState.Follower:
                if (playerDistance > followDistance)
                    anim.SetBool("Is Moving", true);
                else
                    anim.SetBool("Is Moving", false);
                break;
            case ChildState.Transitioning:
                anim.SetBool("Is Moving", true);
                break;
        }

    }

    void wait() //wait to be picked up by player
    {
        if (playerDistance <= pickupDistance)
        {
            //Debug.Log("Added to child manager");
            manager.AddChild(this);
            state = ChildState.Follower;
        }
    }

    void move()
    {
        Vector3 direction = player.GetPosition() - transform.position;
        switch (state)
        {
            case ChildState.Follower:
                if (playerDistance > followDistance)
                {
                    if (player.velocity.magnitude > 2f)
                        transform.Translate(direction.normalized * player.velocity.magnitude * Time.deltaTime);
                    else
                        transform.Translate(direction.normalized * 7.5f * Time.deltaTime);
                }
                break;
            case ChildState.Transitioning:
                if (playerDistance > 0.15)
                {
                    body.isKinematic = true;
                    transform.Translate(direction.normalized * transitionSpeed * Time.deltaTime);
                }
                else
                {
                    body.isKinematic = false;
                    state = ChildState.Leader;
                    cam.Shake(direction, transitionShakeAmplitude, transitionShakeDuration);
                }
                break;
            case ChildState.Leader:
                transform.position = player.GetPosition();
                break;
        }
    }

    void specialAttack()
    {
        if (type == ChildType.Mummy)
        {
            GameObject[] existingAttacks = GameObject.FindGameObjectsWithTag("MummyAttack");
            foreach (GameObject obj in existingAttacks)
                Destroy(obj);
        }

        Vector3 direction = crosshair.GetPosition() - player.GetPosition();
        float angle = Vector3.Angle(Vector3.right, direction);
        if (crosshair.GetLocalPosition().y < 0)
            angle = 360 - angle;
        direction = direction.normalized * 0.3f;
        direction = direction + player.GetPosition();
        direction.z = -2;
        GameObject projectile = Instantiate(specialProjectile, direction, Quaternion.Euler(0, 0, angle)) as GameObject;
        float angleRadians = angle * Mathf.Deg2Rad;
        Vector3 recoilVector = new Vector3(Mathf.Cos(angleRadians), Mathf.Sin(angleRadians), 0);

        switch (type)
        {
            case ChildType.Frankenstein:
                AudioManager.PlayMusic("Ground Pound");
                player.ApplyForce(recoilVector * 2f);
                cam.Shake(angle, 0.5f, 0.2f);
                sepcialAttackTimer = 0.8f;
                break;
            case ChildType.Witch:
                AudioManager.PlayMusic("Shoot Fireball");
                player.ApplyForce(recoilVector * -0.75f);
                cam.Shake(angle + 180, 0.15f, 0.1f);
                sepcialAttackTimer = 0.3f;
                break;
            case ChildType.Vampire:
                VampireTrigger trigger = projectile.GetComponent<VampireTrigger>();
                trigger.SetLocation(crosshair.GetPosition());
                vampireTarget = trigger.GetClosestEnemy();
                if (vampireTarget == null){
                    vampireDashLocation = crosshair.GetPosition();
                }
                else{
                    vampireDashLocation = vampireTarget.transform.position;
                    vampireTarget.GetComponent<EnemyManager>().GoToSleep();
                }
                player.GoToPosition(vampireDashLocation, 25f);
                state = ChildState.Dash;
                AudioManager.PlayMusic("Dash");
                cam.Shake(angle + 180, 0.075f, 0.1f);
                sepcialAttackTimer = 1.5f;
                break;
            case ChildType.Mummy:
                AudioManager.PlayMusic("Mummy Special");
                projectile.GetComponent<MummyProjectile>().SetEndpoint(crosshair.GetPosition());
                player.ApplyForce(recoilVector * -0.65f);
                cam.Shake(angle + 180, 0.1f, 0.1f);
                sepcialAttackTimer = 0.8f;
                break;
        }
    }

    void VampireActions()
    {
        switch (state)
        {
            case ChildState.Dash:
                Vector3 lastPosition = transform.position;
                transform.position = player.GetPosition();
                if (Vector3.Distance(transform.position, vampireDashLocation) < 0.5f || !player.isMovingToLocation())
                {
                    player.Wait(vampireEndLag);
                    state = ChildState.Grapple;
                    grappleTimer = vampireEndLag;
                    anim.SetBool("Is Grappled", true);
                    AudioManager.PlayMusic("Cling");
                    Vector3 direction = transform.position - lastPosition;
                    cam.Shake(direction, 0.1f, 0.1f);
                }
                break;

            case ChildState.Grapple:
                grappleTimer -= Time.deltaTime;
                if (grappleTimer <= 0)
                {
                    if (vampireTarget != null)
                    {
                        vampireTarget.GetComponent<EnemyManager>().WakeUp();
                        vampireTarget.GetComponent<EnemyHealth>().TakeDmg(3f);
                    }
                    player.GiveInvulnerability(0.3f);
                    AudioManager.PlayMusic("Crunch");
                    anim.SetBool("Is Grappled", false);
                    state = ChildState.Leader;
                    cam.UndirectedShake(0.3f, 0.2f);
                }
                break;
        }
    }

    void flash()
    {
        if (flashDuration > 0)
        {
            if (flashTimer <= 0)
            {
                if (isVisible)
                    childRenderer.enabled = false;
                else
                    childRenderer.enabled = true;
                isVisible = !isVisible;
                flashTimer = flashFrequency;
            }
            else
            {
                flashTimer -= Time.deltaTime;
            }
            flashDuration -= Time.deltaTime;
        }
        else
        {
            if (!isVisible)
            {
                childRenderer.enabled = true;
                isVisible = true;
            }
        }
    }

    public void Kill()
    {
        transform.Translate(0f, 0f, 0.5f);
        cam.UndirectedShake(0.8f, 0.5f);
        Instantiate(childBlood, transform.position, Quaternion.identity);
        Instantiate(childFlesh, transform.position, Quaternion.identity);
        AudioManager.PlayMusic("Child Kill");
        Destroy(gameObject);
    }

    public void StartFlash(float duration, float frequency)
    {
        flashDuration = duration;
        flashFrequency = frequency;
        flashTimer = 0f;
    }

    public ChildType GetChildType()
    {
        return type;
    }

    public ChildState GetChildState()
    {
        return state;
    }

    public void SetChildState(ChildState state)
    {
        this.state = state;
    }
}

public enum ChildType
{
    Frankenstein = 0,
    Witch = 1,
    Vampire = 2,
    Mummy = 3,
    TestChild = 4
}

public enum ChildState
{
    Stationary = 0,
    Follower = 1,
    Transitioning = 2,
    Leader = 3,
    Dash = 4,
    Grapple = 5
}