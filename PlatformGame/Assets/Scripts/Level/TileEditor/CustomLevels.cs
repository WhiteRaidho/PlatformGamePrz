using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomLevels : MonoBehaviour
{
    [SerializeField]
    GameObject levelSelectButton;

    [SerializeField]
    Transform contentObject;

    void Start()
    {
        string[] saves = System.IO.Directory.GetFiles(Application.persistentDataPath + "/saves");
        foreach(string save in saves)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(save);
            GameObject obj = Instantiate(levelSelectButton, contentObject);
            var text = obj.GetComponentInChildren<TMP_Text>();
            text.text = info.Name;
            var sl = obj.GetComponent<SelectableLevel>();
            sl.levelName = info.Name;
        }
    }
}
