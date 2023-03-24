using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float displacementFactor = 1f;
    Player player;
    Crosshair crosshair;
    Vector3 shakeVector = Vector3.zero;
    float shakeDuration = 0f;
    float shakeTimer = 0f;
    float undirectedShakeAmplitude = 0f;
    bool directedShake = true;
    float cameraHeight = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        crosshair = FindObjectOfType<Crosshair>();
        cameraHeight = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<MenuManager>().GetComponent<MenuManager>().gamePaused)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, cameraHeight);
            Vector3 tempPosition = player.GetPosition();
            tempPosition.z = transform.position.z;
            Vector3 crosshairDisplacement = crosshair.GetLocalPosition();
            tempPosition.x += crosshairDisplacement.x / crosshair.maxX * displacementFactor;
            tempPosition.y += crosshairDisplacement.y / crosshair.maxY * displacementFactor;

            if (shakeTimer < shakeDuration)
            {
                if (directedShake)
                {
                    tempPosition += shakeVector * Mathf.Sin(Mathf.PI / shakeDuration * shakeTimer);
                }
                else
                {
                    float angle = Random.Range(0f, Mathf.PI * 2f);
                    Vector3 randomVector = new Vector3(Mathf.Cos(angle) * undirectedShakeAmplitude, Mathf.Sin(angle) * undirectedShakeAmplitude, 0);
                    tempPosition += randomVector * Mathf.Sin(Mathf.PI / shakeDuration * shakeTimer);
                }

                shakeTimer += Time.deltaTime;
            }

            transform.position = tempPosition;
        }
    }

    public void UndirectedShake(float amplitude, float duration)
    {
        if (shakeTimer >= shakeDuration || amplitude >= shakeVector.magnitude)
        {
            directedShake = false;
            shakeDuration = duration;
            undirectedShakeAmplitude = amplitude;
        }
    }

    public void Shake(float angle, float amplitude, float duration)
    {
        //Debug.Log(shakeTimer+"/"+shakeDuration+" | "+amplitude+"/"+shakeVector.magnitude);
        float angleRadians = angle * Mathf.Deg2Rad;
        if (shakeTimer >= shakeDuration || amplitude >= shakeVector.magnitude)
        {
            directedShake = true;
            shakeVector = new Vector3(Mathf.Cos(angleRadians) * amplitude, Mathf.Sin(angleRadians) * amplitude, 0);
            shakeDuration = duration;
            shakeTimer = 0f;
        }
    }

    public void Shake(Vector3 direction, float amplitude, float duration)
    {
        //Debug.Log(shakeTimer+"/"+shakeDuration+" | "+amplitude+"/"+shakeVector.magnitude);
        if (shakeTimer >= shakeDuration || amplitude >= shakeVector.magnitude)
        {
            directedShake = true;
            shakeVector = direction.normalized * amplitude;
            shakeDuration = duration;
            shakeTimer = 0f;
        }
    }

}
