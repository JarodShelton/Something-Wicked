using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*public class TestChild : MonoBehaviour, Child
{
    public float followSpeed = 3;

    Player player;
    ChildManager manager;
    ChildState state;
    ChildType type = ChildType.TestChild;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        manager = FindObjectOfType<ChildManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(state == ChildState.Stationary)
            wait();
        else
            move();

        if(state == ChildState.Leader && Input.GetMouseButton(1)) // If child is the leader and right mouse button is pressed
            specialAttack();

    }

    void wait() //wait to be picked up by player
    {
        float distance = Vector3.Distance(transform.position, player.GetPosition());
        if(distance <= 2){
            Debug.Log("Added to child manager");
            //ChildManager.AddChild(this); // WHY DOESN'T THIS WORK
            state = ChildState.Follower;
        }
    }

    void move()
    {
        
    }

    void specialAttack()
    {
        //This will not be filled on the test child. Only the implemented children.
    }

    public ChildType GetChildType()
    {
        return type;
    }

    public ChildState GetChildState()
    {
        return ChildState.Stationary;
    }

    public void SetChildState(ChildState state)
    {
        this.state = state;
    }
}*/
