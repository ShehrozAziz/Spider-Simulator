using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int WhichLevelBuddy;
    
    public static GameManager instance;
    public GameObject PlayerSpider;
    public GameObject FirstSpider;

    public static int Venom;
    public static int Health;
    public static int levelCleared;
    public static int EnabledLevel;
    public static bool RewardedClick = false;
    public GameObject YouDiedPanel;
    public GameObject ControllerPanel;
    public GameObject PausePanel;
    public List<GameObject> DeadPanelInners;
    public Button DoubleRewardButton;
    public GameObject GetDoubleReward;
    public GameObject ClaimedDoubleReward;

    public GameObject AllLevels;
    public List<GameObject> Levels;
    public List<GameObject> LevelsStartText;
    public List<GameObject> LevelsEndText;
    public List<GameObject> LevelPositions;
    Transform ThisLevelStartTransform;
    public GameObject LevelInfoPanel;
    public GameObject LevelClearPanel;
    public GameObject LoadingPanel;
    public Rigidbody Player;
    public List<GameObject> Players;
    public List<Camera> PlayerCameras;
    public List<GameObject> PlayerParents;

    public GameObject FreeMode;
    public GameObject FreeModeInfo;

    public Text Cash;
    public Text Coins;
    public List<int> LevelEndCash;
    public List<int> LevelEndCoins;


    public AudioSource Win;
    public AudioSource Loss;
    // Start is called before the first frame update

    void Start()
    {
        instance = this;
        diffuse(PlayerParents, PlayerParents.Count, MenuManager.SelectedSpider);
        diffuse(Players, Players.Count, MenuManager.SelectedSpider);
        GameObject Temp = Players[MenuManager.SelectedSpider];
        PlayerSpider = Temp;
        Player = Temp.GetComponent<Rigidbody>();
        if (PlayerPrefs.GetInt("Free")==-1)
        {
            Debug.Log("Starting Levels");
            EnabledLevel = MenuManager.SelectedLevel;
            diffuse(Levels, Levels.Count, EnabledLevel - 1);
            diffuse(LevelsStartText, LevelsStartText.Count, EnabledLevel - 1);
            ThisLevelStartTransform = LevelPositions[EnabledLevel - 1].transform;
            Player.transform.SetPositionAndRotation(ThisLevelStartTransform.position, ThisLevelStartTransform.rotation);
            if (EnabledLevel > 1)
            {
                Player.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                SpidyScript.BeesCaught = 0;
            }
            FreeMode.SetActive(false);
        }
        else
        {

            FreeMode.SetActive(true);
            AllLevels.SetActive(false);
            FreeModeInfo.SetActive(true);
        }
        //EnabledLevel = WhichLevelBuddy;
        


        SickscoreGames.HUDNavigationSystem.HUDNavigationSystem.Instance.ChangePlayerandCamera(PlayerSpider.transform,PlayerCameras[MenuManager.SelectedSpider]);
        LevelInfoPanel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Cash.text = PlayerPrefs.GetInt("Cash").ToString();
        Coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void KillTheGame()
    {
        Time.timeScale = 1;
        if (PausePanel.activeSelf)
            PausePanel.SetActive(false);
        if (YouDiedPanel.activeSelf)
            YouDiedPanel.SetActive(false);
        //AdsManager.instance.Show_Interstitial();
        LoadingPanel.SetActive(true);
    }
    public void GamePaused()
    {
        PausePanel.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameResumed()
    {
        PausePanel.SetActive(false);
        Time.timeScale = 1;
    }
    public void UserDied()
    {
        if (PlayerPrefs.GetInt("Free") == -1)
        {
            AllLevels.SetActive(false);
        }
        else
        {
            FreeMode.SetActive(false);
        }
        Loss.Play();
        YouDiedPanel.SetActive(true);
        diffuse(DeadPanelInners, DeadPanelInners.Count, 0);
    }
    public void FriendDied()
    {
        if (PlayerPrefs.GetInt("Free") == -1)
        {
            AllLevels.SetActive(false);
        }
        else
        {
            FreeMode.SetActive(false);
        }
        Loss.Play();
        YouDiedPanel.SetActive(true);
        diffuse(DeadPanelInners, DeadPanelInners.Count, 1);
    }
    public void HideDeadPanel()
    {
        YouDiedPanel.SetActive(false);
        KillTheGame();
    }
    public void HideStartPanel()
    {
        LevelInfoPanel.SetActive(false);
        ControllerPanel.SetActive(true);
    }
    public void HideEndPanel()
    {
        LevelClearPanel.SetActive(false);
        MenuManager.OpenLevels = true;
        KillTheGame();
    }
    public void DoubleReward()
    {
        LevelClearPanel.SetActive(false);
        RewardedClick = true;
        AdsManager.instance.Show_Rewarded(RewardRecevied);
    }
    public void RewardRecevied()
    {
        DoubleRewardButton.interactable = false;
        GetDoubleReward.SetActive(false);
        ClaimedDoubleReward.SetActive(true);
        ShowLevelComplete(GameManager.EnabledLevel);
    }
    public void ShowLevelComplete(int LevelNo)
    {
        if (PlayerPrefs.GetInt("Free") == -1)
        {
            AllLevels.SetActive(false);
        }
        else
        {
            FreeMode.SetActive(false);
        }
        Win.Play();
        Player.isKinematic = true;
        LevelClearPanel.SetActive(true);
        Debug.Log(LevelNo.ToString());
        diffuse(LevelsEndText,LevelsEndText.Count, LevelNo - 1);
        int temp = PlayerPrefs.GetInt("Cash");
        int temp2 = PlayerPrefs.GetInt("Coins");
        temp += LevelEndCash[LevelNo - 1];
        temp2 += LevelEndCoins[LevelNo - 1];
        PlayerPrefs.SetInt("Cash", temp);
        PlayerPrefs.SetInt("Coins", temp2);
        if(PlayerPrefs.GetInt("Cleared") < EnabledLevel)
        {
            PlayerPrefs.SetInt("Cleared", EnabledLevel);
        }

    }
    void diffuse(List<GameObject> List, int Size, int index)
    {
        for (int i = 0; i < Size; i++)
        {
            if (i == index)
            {
                List[i].SetActive(true);
            }
            else
            {
                List[i].SetActive(false);
            }
        }
    }
}
