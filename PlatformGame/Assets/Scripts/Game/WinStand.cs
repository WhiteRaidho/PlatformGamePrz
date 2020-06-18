using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStand : MonoBehaviour
{
    bool win = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!win && collision.tag.Equals("Player"))
        {
            win = true;
            LevelManager.instance.Win();
        }
    }
}
