using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game properties")]
    public GameManagerProperties saveProperties;

    [Header("Levels")]
    public string currentLevelName;
    public int currentLevel;
    public string customLevelName = "";

    public static int maxLevel = 3;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(this);
            Load();
        } else
        {
            currentLevelName = instance.currentLevelName;
            currentLevel = instance.currentLevel;
            customLevelName = instance.customLevelName;
            saveProperties = instance.saveProperties;
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadLevel(string levelName)
    {
        instance.Save();
        instance.currentLevelName = levelName;
        SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
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
