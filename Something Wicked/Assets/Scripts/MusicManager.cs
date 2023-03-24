using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip dayClip, nightClip;
    public static MusicManager instance;

    TimeManager timeManager;
    AudioSource day, night;
    bool isDay = false;


    private void Awake()
    {
        if(instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        day = gameObject.AddComponent<AudioSource>();
        night = gameObject.AddComponent<AudioSource>();
        timeManager = FindObjectOfType<TimeManager>();

        SwapTrack(dayClip);
    }

    // Update is called once per frame
    void Update()
    {
        day.loop = true;
        night.loop = true;
        if(isDay && timeManager.isNight()){
            SwapTrack(nightClip);
        }else if(!isDay && !timeManager.isNight()){
            SwapTrack(dayClip);
        }
    }

    public void SwapTrack(AudioClip newClip)
    {
        isDay = !isDay;
        StartCoroutine(fadeTrack(newClip));
        Debug.Log("Swapping track");
        
    }

    private IEnumerator fadeTrack(AudioClip newClip){
        float timeToFade = 5f;
        float timeElapsed = 0f;

        if(isDay){
            night.clip = newClip;
            night.Play();

            while(timeElapsed < timeToFade){
                night.volume = Mathf.Lerp(0, 0.5f, timeElapsed / timeToFade);
                day.volume = Mathf.Lerp(0.5f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            day.Stop();
        }else{
            day.clip = newClip;
            day.Play();

            while(timeElapsed < timeToFade){
                day.volume = Mathf.Lerp(0, 1, timeElapsed / timeToFade);
                night.volume = Mathf.Lerp(1, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            night.Stop();
        }
    }
}
