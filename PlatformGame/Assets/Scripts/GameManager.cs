using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Game properties")]
    public GameManagerProperties saveProperties;
	
	[Header("Levels")]
    public string currentLevelName;
    public int currentLevel;
    public static int maxLevel = 1;

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
	
	public void LoadLevel(string levelName)
    {
        Save();
        currentLevelName = levelName;
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
