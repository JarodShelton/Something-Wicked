using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class NightLightController : MonoBehaviour
{

    TimeManager Time;

    public Light2D Light;
    SpriteRenderer spriteRenderer;

    private float baseIntensity;

    public Sprite turnedOffSprite;
    public Sprite turnedOnSprite; //...?

    // Start is called before the first frame update
    void Start()
    {
        baseIntensity = Light.intensity;

        //Start with lamp turned off
        Light.intensity = 0f;

        if(TimeManager.time == null){

            Debug.Log("Time Manager does not exist!!");

        }else{

            Time = TimeManager.time;

        }
    }

    public void changeLightState(bool state){
        if(state)
            Light.intensity = baseIntensity;
        else   
            Light.intensity = 0f;

        if(turnedOffSprite != null){
            if(state)
                GetComponent<SpriteRenderer>().sprite = turnedOnSprite;
            else
                GetComponent<SpriteRenderer>().sprite = turnedOffSprite;

        }
    }
}
