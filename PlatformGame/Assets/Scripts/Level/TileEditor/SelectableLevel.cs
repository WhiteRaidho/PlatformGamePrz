using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableLevel : MonoBehaviour
{
    public string levelName;

    public void PlayLevel()
    {
        GameManager.instance.customLevelName = levelName;
        GameManager.instance.LoadLevel("CustomLevel");
    }

    public void EditLevel()
    {
        GameManager.instance.customLevelName = levelName;
        GameManager.instance.LoadLevel("LevelEditor");
    }
}
