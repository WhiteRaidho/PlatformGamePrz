using UnityEngine;

public static class SaveManager
{
    public static void Save(GameManager.GameManagerProperties gm)
    {
        PlayerPrefs.SetInt("unlockedLevel", gm.unlockedLevel);
        PlayerPrefs.SetInt("money", gm.money);
        PlayerPrefs.Save();
    }

    public static GameManager.GameManagerProperties Load()
    {
        GameManager.GameManagerProperties result = new GameManager.GameManagerProperties();
        result.unlockedLevel = PlayerPrefs.GetInt("unlockedLevel");
        result.money = PlayerPrefs.GetInt("money");
        if (result.unlockedLevel <= 0) result.unlockedLevel = 1;
        if (result.money < 0) result.money = 0;
        return result;
    }
}
