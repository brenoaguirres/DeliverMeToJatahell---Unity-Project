using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// Manages logic for UI

public class GUIController : MonoBehaviour
{
    #region Fields and Properties

    // Lifebar 
    public PlayerStatus _playerStatus;
    public Slider PlayerLifebar;

    // Game Over
    public GameObject GameOverWindow;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI BestScoreText;
    private float bestScoreTime;
    int minutes;
    int seconds;

    // Zombies Killed
    public int KilledZombies;
    public TextMeshProUGUI KilledZombiesText;
    public TextMeshProUGUI BossWarningText;

    // Ammunition
    public TextMeshProUGUI ammoCount;
    public Slider Reloadbar;
    public GunController playerGun;

    // Building Entrance
    public TextMeshProUGUI doNotHaveKeyText;
    public static GameObject enterBuildingUI;
    public TextMeshProUGUI enterBuildingText;
    public bool buildingWindowClick = false;

    // Inventory
    public GameObject playerInventoryWindow;
    public string inventoryGOName = "Inventory";
    #endregion

    #region Initialization 
    private void Start()
    {
        _playerStatus = GameObject.FindWithTag("Player")
            .GetComponent<PlayerStatus>();
        PlayerLifebar.maxValue = _playerStatus.life;
        PlayerLifebarUpdate();
        bestScoreTime = PlayerPrefs.GetFloat("BestScore");
        Reloadbar.gameObject.SetActive(false);
        KilledZombies = 299;
        UpdateKilledZombies();

        //Initialize Inventory
        playerInventoryWindow = GetComponentInChildren<InventoryFinder>(true).gameObject;
        playerInventoryWindow.SetActive(true);
        playerInventoryWindow.SetActive(false);

        InitializeEnterBuildingWindow();
    }
    #endregion

    #region Execution
    private void Update()
    {
        UpdateGameOverWindow();
        SaveBestScore(minutes, seconds);
    }
    #endregion

    #region Methods
    public void PlayerLifebarUpdate()
    {
        PlayerLifebar.value = _playerStatus.life;
    }

    public void GameOver()
    {
        GameOverWindow.SetActive(true);
    }

    public void RestartGame()
    {
        GameManager.CallScene("Restart");
    }

    public void UpdateGameOverWindow()
    {
        minutes = (int)Time.timeSinceLevelLoad / 60;
        seconds = (int)Time.timeSinceLevelLoad % 60;
        GameOverText.text = string.Format("You've " +
            "survived for {0} minutes and {1} seconds!", minutes, seconds);
    }

    public void SaveBestScore(int min, int sec)
    {
        if(Time.timeSinceLevelLoad > bestScoreTime)
        {
            bestScoreTime = Time.timeSinceLevelLoad;
            BestScoreText.text = string.Format("You've " +
            "survived for {0} minutes and {1} seconds!", min, sec);
            PlayerPrefs.SetFloat("BestScore", bestScoreTime);
        }
        if(BestScoreText.text != string.Empty)
        {
            min = (int)bestScoreTime / 60;
            sec = (int)bestScoreTime % 60;
            BestScoreText.text = string.Format("You've " +
            "survived for {0} minutes and {1} seconds!", min, sec);
        }
    }

    public void UpdateKilledZombies()
    {
        KilledZombies++;
        KilledZombiesText.text = string.Format("x {0}", KilledZombies);
    }

    public void UpdateKilledZombies(int value)
    {
        KilledZombies -= value;
        KilledZombiesText.text = string.Format("x {0}", KilledZombies);
    }

    public int GetKilledZombies()
    {
        return KilledZombies;
    }

    public void UpdateAmmoCount()
    {
        ammoCount.text = string.Format("{0} / {1}", _playerStatus.loadedAmmo,
            _playerStatus.reloadAmmo);
    }

    public void ShowUIText(TextMeshProUGUI txt)
    {
        txt.gameObject.SetActive(true);
    }

    public void HideUIText(TextMeshProUGUI txt)
    {
        txt.gameObject.SetActive(false);
    }

    public void FillReloadBar()
    {
        StartCoroutine(FillBarByTime(Reloadbar, playerGun.ReloadRate));
    }

    public IEnumerator ShowUITextWithGradient(float TimeToShow, TextMeshProUGUI txt)
    {
        ShowUIText(txt);
        Color textColor = txt.color;
        textColor.a = 1;
        txt.color = textColor;
        yield return new WaitForSeconds(TimeToShow);
        float count = 0;
        while (txt.color.a > 0)
        {
            count += Time.deltaTime / TimeToShow;
            textColor.a = Mathf.Lerp(1, 0, count);
            txt.color = textColor;
            if (txt.color.a < 0)
            {
                HideUIText(txt);
            }
            yield return null; //espera o proximo frame para cada itera��o
        }
    }

    public IEnumerator FillBarByTime(Slider bar, float TimeToFill)
    {
        bar.value = 0;
        bar.maxValue = 1;
        bar.gameObject.SetActive(true);
        float lerpRate = 0;
        while (bar.value < 1f)
        {
            lerpRate += Time.deltaTime / TimeToFill;
            bar.value = Mathf.Lerp(0, 1, lerpRate);
            if (bar.value >= 1)
            {
                bar.gameObject.SetActive(false);
            }
            yield return null;
        }
    }

    public void InitializeEnterBuildingWindow()
    {
        enterBuildingUI = GetComponentInChildren<GetComponentHelperScript>(true).gameObject;
        enterBuildingText = enterBuildingUI.GetComponentInChildren<TextMeshProUGUI>();
        enterBuildingUI.SetActive(false);
    }

    public void ShowEnterBuildingWindow(string buildingName)
    {
        enterBuildingText.text = string.Format("Do you wish to enter {0}?", buildingName);
        enterBuildingText.fontSize = 12;
        enterBuildingUI.SetActive(true);
    }

    public void HideEnterBuildingWindow()
    {
        if (enterBuildingUI.activeSelf == true)
        {
            enterBuildingUI.SetActive(false);
        } 
    }

    public void OnPlayerClickYes()
    {
        buildingWindowClick = true;
    }

    public void OnPlayerClickNo()
    {
        buildingWindowClick = false;
        enterBuildingUI.SetActive(false);
    }

    public void ToggleInventory()
    {
        playerInventoryWindow.SetActive(!playerInventoryWindow.activeSelf);
    }
    #endregion
}