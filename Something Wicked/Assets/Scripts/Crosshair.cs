using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float cursorDistance = 20;
    public float mouseSensitivity = 100;

    public float maxX = 8;
    public float maxY = 4;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;
        transform.position = worldPosition;
        

        Vector3 tempPosition = transform.localPosition;
        if(tempPosition.x > maxX)
            tempPosition.x = maxX;
        else if(tempPosition.x < -maxX)
            tempPosition.x = -maxX;

        if(tempPosition.y > maxY)
            tempPosition.y = maxY;
        else if(tempPosition.y < -maxY)
            tempPosition.y = -maxY;
            
        transform.localPosition = tempPosition;
        
    }

    public Vector3 GetPosition(){
        return transform.position;
    }

    public Vector3 GetLocalPosition(){
        return transform.localPosition;
    }
}
