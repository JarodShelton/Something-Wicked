using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager time;

    //Total duration of a night, in minutes
    public float nightDuration = 5f;

    //Current time, in minutes
    public float currentTime = 0f;

    //Number of the current night
    public int currentNight = 1;

    private bool lightsOn = false;

    [Space]

    //Color of the sky over the night, from left to right
    public Gradient lightingColor;

    //If greater than 0, light turns on
    public AnimationCurve lightOnCurve;

    //Moon light that lights scene with light
    public Light2D moonLight;

    //TEMP: display current night
    public Text nightText;

    void Awake()
    {

        time = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0f;

        if (nightText != null)
            nightText.text = "Night " + currentNight;
        else
            Debug.Log("TimeManager is missing text component!");

    }

    // Update is called once per frame
    void Update()
    {

        //Advance the clock, which is based on minutes
        currentTime += Time.deltaTime / 60;

        //If current day is over, advance to next day
        if (currentTime > nightDuration)
        {
            currentTime = 0;
            currentNight++;

            if (nightText != null)
                nightText.text = "Night " + currentNight;
            else
                Debug.Log("TimeManager is missing text component!");
        }

        //Turn on Lights
        if (lightOnCurve.Evaluate(Mathf.InverseLerp(0, nightDuration, currentTime)) > 0.2f && !lightsOn)
        {
            lightsOn = true;
            foreach (GameObject light in GameObject.FindGameObjectsWithTag("WorldLight"))
            {
                if (light.GetComponent<NightLightController>())
                {
                    light.GetComponent<NightLightController>().changeLightState(true);
                }
            }
        }//Turn off lights
        else if (lightOnCurve.Evaluate(Mathf.InverseLerp(0, nightDuration, currentTime)) < 0.2f && lightsOn)
        {
            lightsOn = false;
            foreach (GameObject light in GameObject.FindGameObjectsWithTag("WorldLight"))
            {
                if (light.GetComponent<NightLightController>())
                {
                    light.GetComponent<NightLightController>().changeLightState(false);
                }
            }
        }
        //Set the moon to the appropriate light based on current time in the night
        moonLight.color = lightingColor.Evaluate(Mathf.InverseLerp(0, nightDuration, currentTime));
    }

    //just got whataburger, ate a patty melt :)
    public bool isNight()
    {
        return lightOnCurve.Evaluate(Mathf.InverseLerp(0, nightDuration, currentTime)) > 0.2f;
    }
}
