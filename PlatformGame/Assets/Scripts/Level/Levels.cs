using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Levels : MonoBehaviour
{
    [SerializeField]
    GameObject levelSelectButton;

    [SerializeField]
    Transform contentObject;
    
    void Start()
    {
        for(int i = 1; i <= GameManager.maxLevel; i++)
        {
            GameObject btn = Instantiate(levelSelectButton, contentObject) as GameObject;
            btn.GetComponent<SelectLevel>().SetLevel(i);
            if(GameManager.instance.saveProperties.unlockedLevel < i)
            {
                btn.GetComponent<SelectLevel>().LockLevel();
            }
        }
    }
}
