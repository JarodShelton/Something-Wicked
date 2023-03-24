using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages the states of all children
public class ChildManager : MonoBehaviour
{
    public Child initialLeadChild;
    Child leadChild;
    LinkedList<Child> children;
    int[] numTypes = new int[4];
    float switchTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        children = new LinkedList<Child>();
        AddChild(initialLeadChild);
        SetLeadChild(initialLeadChild);
    }

    // Update is called once per frame
    void Update()
    {
        if(switchTimer > 0)
            switchTimer -= Time.deltaTime;
        if(leadChild.GetChildState() != ChildState.Dash && leadChild.GetChildState() != ChildState.Grapple){
            if(Input.GetKey(KeyCode.Alpha1))
                FindNewLeader(ChildType.Frankenstein);
            if(Input.GetKey(KeyCode.Alpha2))
                FindNewLeader(ChildType.Witch);
            if(Input.GetKey(KeyCode.Alpha3))
                FindNewLeader(ChildType.Vampire);
            if(Input.GetKey(KeyCode.Alpha4))
                FindNewLeader(ChildType.Mummy);

            if(switchTimer <= 0 && (Input.GetKey(KeyCode.LeftShift) || Input.mouseScrollDelta.y > 0)){
                int counter = 0;
                switch(leadChild.GetChildType()){
                    case ChildType.Frankenstein: counter = 0; break;
                    case ChildType.Witch: counter = 1; break;
                    case ChildType.Vampire: counter = 2; break;
                    case ChildType.Mummy: counter = 3; break;
                }
                for(int i = 1; i < 4; i++){
                    int temp = (counter + i)%4;
                    if(numTypes[temp] > 0){
                        switch(temp){
                            case 0: FindNewLeader(ChildType.Frankenstein); break;
                            case 1: FindNewLeader(ChildType.Witch); break;
                            case 2: FindNewLeader(ChildType.Vampire); break;
                            case 3: FindNewLeader(ChildType.Mummy); break;
                        }
                        switchTimer = 0.2f;
                        break;
                    }
                }

            }else if(switchTimer <= 0 && (Input.GetKey(KeyCode.LeftControl)  || Input.mouseScrollDelta.y < 0)){
                int counter = 0;
                switch(leadChild.GetChildType()){
                    case ChildType.Frankenstein: counter = 0; break;
                    case ChildType.Witch: counter = 1; break;
                    case ChildType.Vampire: counter = 2; break;
                    case ChildType.Mummy: counter = 3; break;
                }
                counter += 4;
                for(int i = 1; i < 4; i++){
                    int temp = (counter - i)%4;
                    if(numTypes[temp] > 0){
                        switch(temp){
                            case 0: FindNewLeader(ChildType.Frankenstein); break;
                            case 1: FindNewLeader(ChildType.Witch); break;
                            case 2: FindNewLeader(ChildType.Vampire); break;
                            case 3: FindNewLeader(ChildType.Mummy); break;
                        }
                        switchTimer = 0.2f;
                        break;
                    }
                }
            }
        }
        
    }

    public void KillLeader(){
        if(children.Count <= 1){
            gameOver();
        }else{
            children.Remove(leadChild);
            leadChild.Kill();
            AudioManager.PlayMusic("Swap");
            SetLeadChild(children.First.Value);
        }
    }

    void gameOver(){
        Debug.Log("Game Over");
        FindObjectOfType<MenuManager>().GetComponent<MenuManager>().showDeathScreen();
    }

    public void Flash(float duration, float frequency){
        foreach(Child child in children)
            child.StartFlash(duration, frequency);
    }    

    public void FindNewLeader(ChildType type){
        if(leadChild.GetChildType() != type){
            foreach(Child child in children){
                if(child.GetChildType() == type){
                    SetLeadChild(child);
                    AudioManager.PlayMusic("Swap");
                    break;
                }
            }
        }
    }

    public void SetLeadChild(Child child)
    {
        if(leadChild != null)
            leadChild.SetChildState(ChildState.Follower);
        child.SetChildState(ChildState.Transitioning);
        leadChild = child;
    }

    public Child GetLeader(){
        return leadChild;
    }

    public void AddChild(Child child)  
    {
        if(!children.Contains(child)){
            children.AddLast(child);
            switch(child.GetChildType()){
                case ChildType.Frankenstein:
                    numTypes[0]++;
                    break;
                case ChildType.Witch:
                    numTypes[1]++;
                    break;
                case ChildType.Vampire:
                    numTypes[2]++;
                    break;
                case ChildType.Mummy:
                    numTypes[3]++;
                    break;
            }
        }
    }
}
