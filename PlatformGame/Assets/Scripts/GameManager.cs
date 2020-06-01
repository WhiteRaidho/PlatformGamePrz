using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game properties")]
    public GameManagerProperties saveProperties;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Load();
        }
        else
        {
            Destroy(this);
        }
    }

    public void Save()
    {
        SaveManager.Save(instance.saveProperties);
    }

    public void Load()
    {
        instance.saveProperties = SaveManager.Load();
    }

    [Serializable]
    public class GameManagerProperties
    {
        public int unlockedLevel = 1;
        public int money = 0;
        public int startingLives = 3;
    }
}
