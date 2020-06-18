using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{

    GameObject cameraObject;

    public Vector2 offset;

    public float zPosition = 10f;

    private void Start()
    {
        if (offset == Vector2.zero) offset = transform.position;
        cameraObject = Camera.main.gameObject;
    }
    
    

    void LateUpdate()
    {   

        transform.position = new Vector3(cameraObject.transform.localPosition.x/zPosition + offset.x, cameraObject.transform.localPosition.y/zPosition + offset.y, zPosition);
    }
}
