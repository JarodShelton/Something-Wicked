using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timedDestroy : MonoBehaviour
{
    public float timeUntilDestroy;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer(){
        yield return new WaitForSeconds(timeUntilDestroy);
        Destroy(gameObject);
    }
}
