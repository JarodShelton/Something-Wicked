using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    //Shop roof variables
    public Color roofOn, roofOff;
    public SpriteRenderer roofSprite;
    public float transitionSpeed;

    private Color currentColor, targetcolor;
    private float t = 1;

    //Other variables
    public int[] UpgradeCosts;
    public Collider2D NightCollider;

    public Transform kickoutPoint;
    // Start is called before the first frame update
    void Start()
    {
        setRoofColor(true);
    }


    // Update is called once per frame
    void Update()
    {
        if(TimeManager.time.isNight())
            NightCollider.enabled = true;
        else
            NightCollider.enabled = false;

        if (t < 1)
            t += Time.deltaTime;
        roofSprite.color = Color.Lerp(currentColor, targetcolor, t * transitionSpeed);
    }

    void setRoofColor(bool state)
    {
        t = 0;
        if (state == false)
        {
            currentColor = roofOn;
            targetcolor = roofOff;
        }
        else
        {
            currentColor = roofOff;
            targetcolor = roofOn;
        }
    }
    void OnTriggerStay2D(Collider2D col){
        if (col.gameObject.tag == "Player" && TimeManager.time.isNight())
        {
            setRoofColor(true);
            col.transform.position = kickoutPoint.position;
        }
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(!TimeManager.time.isNight())
                setRoofColor(false);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if(!TimeManager.time.isNight())
                setRoofColor(true);
        }
    }
}
