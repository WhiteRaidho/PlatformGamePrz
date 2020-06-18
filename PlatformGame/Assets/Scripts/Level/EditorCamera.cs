using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EditorCamera : MonoBehaviour
{

    [Range(1f, 10f)]
    public float camSpeed = 3f;
    
    private void Update()
    {
        Vector3 pos = this.transform.position;
        pos.x += Input.GetAxis("Horizontal") * camSpeed * Time.deltaTime;
        pos.y += Input.GetAxis("Vertical") * camSpeed * Time.deltaTime;
        pos.x = Mathf.Clamp(pos.x, -15, 15);
        pos.y = Mathf.Clamp(pos.y, -15, 15);
        this.transform.position = pos;
    }
}
