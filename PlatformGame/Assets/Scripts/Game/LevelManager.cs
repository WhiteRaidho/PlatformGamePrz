using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public GameObject gameOverPanel;
    public GameObject winPanel;
    public GameObject adPanel;

    public TMP_Text HPText;
    public TMP_Text MoneyText;

    public Player player;

    public Button adButton;
    public Button nextLevelButton;

    public Checkpoint startCheckpoint;

    public bool adWatched = false;

    public int obtainedMoney = 0;

    [SerializeField]
    private List<ChangeStand> changeStands = new List<ChangeStand>();

    private void Awake()
    {
        if (!instance) instance = this;
        else Destroy(this);
    }

    public void Start()
    {
        adButton = GameObject.Find("AdButton").GetComponent<Button>();
        nextLevelButton = GameObject.Find("NextButton").GetComponent<Button>();
        gameOverPanel = GameObject.Find("GameOverPanel");
        winPanel = GameObject.Find("WinPanel");
        adPanel = GameObject.Find("AdPanel");

        HPText = GameObject.Find("HPText").GetComponent<TMP_Text>();
        MoneyText = GameObject.Find("MoneyText").GetComponent<TMP_Text>();

        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);

        HPText.text = GameManager.instance.saveProperties.startingLives.ToString();
        MoneyText.text = GameManager.instance.saveProperties.money.ToString();

        changeStands.Clear();
        changeStands = new List<ChangeStand>(FindObjectsOfType<ChangeStand>());
    }

    public void Restore()
    {
        foreach(ChangeStand stand in changeStands)
        {
            stand.ChangeShape(stand.savedShape);
        }
    }

    public void SaveStates()
    {
        foreach (ChangeStand stand in changeStands)
        {
            stand.savedShape = stand.currentShape;
        }
    }

    public void GameOver() {
        LockMovement(true);
        gameOverPanel.SetActive(true);
        if (adWatched) adPanel.SetActive(false);
        else StartCoroutine(UnlockAdButton());
    }

    IEnumerator UnlockAdButton()
    {
        yield return new WaitForSeconds(1f);
        adButton.interactable = true;
    }

    public void Quit()
    {
        GameManager.instance.LoadLevel("LevelSelect");
    }

    public void TryAgain()
    {
        GameManager.instance.LoadLevel(GameManager.instance.currentLevelName);
    }

    public void NextLevel()
    {
        if (GameManager.instance.currentLevel + 1 <= GameManager.maxLevel)
        {
            GameManager.instance.currentLevel++;
            GameManager.instance.LoadLevel($"Level{GameManager.instance.currentLevel}");
        }
    }

    public void Win()
    {
        LockMovement(true);
        winPanel.SetActive(true);
        if(GameManager.instance.currentLevel == GameManager.maxLevel)
        {
            nextLevelButton.gameObject.SetActive(false);
        }
        GameManager.instance.saveProperties.unlockedLevel++;
        GameManager.instance.saveProperties.money += obtainedMoney;
        GameManager.instance.Save();
    }

    public void WatchAd()
    {
        adWatched = true;
        gameOverPanel.SetActive(false);
        adButton.interactable = false;
        player.AddLive();
        LockMovement(false);
    }

    public void AddMoney()
    {
        obtainedMoney++;
        MoneyText.SetText((obtainedMoney + GameManager.instance.saveProperties.money).ToString());
    }

    public void LockMovement(bool lockMove)
    {
        player.GetComponent<PlayerMovement>().canMove = !lockMove;
    }
}

