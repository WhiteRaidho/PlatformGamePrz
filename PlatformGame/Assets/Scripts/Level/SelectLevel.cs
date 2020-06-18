using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;

    int level;

    [SerializeField]
    GameObject lockPanel;

    public void SetLevel(int level)
    {
        this.level = level;
        text.SetText(level.ToString());
    }

    public void LockLevel()
    {
        this.GetComponent<Button>().interactable = false;
        lockPanel.SetActive(true);
    }

    public void GoToThisLevel()
    {
        GameManager.instance.currentLevel = level;
        GameManager.instance.LoadLevel($"Level{level}");
    }
}
