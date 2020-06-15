using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : ActiveElement
{
    
    void Start()
    {
        canBeDeactivated = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            Activate();
        }
    }
}
