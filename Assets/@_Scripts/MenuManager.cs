using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject MainScreen;
    public GameObject LevelSelectionScreen;
    public GameObject ModesScreen;
    public GameObject ConfirmExitScreen;
    public GameObject InsuffBalanceScreen;
    public GameObject ShopScreen;
    public Animation RewardedCash;
    public AudioSource CashRecieved;
    public Text Cash;
    public Text Coins;
    public static GameObject ActiveScreen;
    public static int SelectedLevel = 0;
    public static int SelectedSpider = 0;

    public static bool OpenLevels = false;

    public bool ResetData;
    // Start is called before the first frame update

    //Turn off the Reset Data
    //Turn off istest on level selection
    //Turn on the interactive for locked levels
    //Reset Data Once
    public void Start()
    {
        ActiveScreen = MainScreen;
        PlayerPrefs.SetInt("CUnlock" + 0.ToString(), 1);
        if (!MainScreen.activeSelf)
        {
            MainScreen.SetActive(true);
            
        }    
        if(ResetData)
        {
            ResetDataFunction();
        }
        if(OpenLevels)
        {
            MainScreen.SetActive(false);
            LevelSelectionScreen.SetActive(true);
        }

    }
    public void ResetDataFunction()
    {
        PlayerPrefs.SetInt("Cash", 0);
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Cleared", 0);
        PlayerPrefs.SetInt("CUnlock" + 0.ToString(), 1);
        PlayerPrefs.SetInt("CUnlock" + 1.ToString(), 0);
        PlayerPrefs.SetInt("CUnlock" + 2.ToString(), 0);
        PlayerPrefs.SetInt("CUnlock" + 3.ToString(), 0);

    }
    private void Update()
    {
        Cash.text = PlayerPrefs.GetInt("Cash").ToString();
        Coins.text = PlayerPrefs.GetInt("Coins").ToString();
    }
    public void OnPlayClick()
    {
        MainScreen.SetActive(false);
        LevelSelectionScreen.SetActive(true);
    }
    public void Play1()
    {
        SelectedLevel = 1;
    }
    public void Play2()
    {
        SelectedLevel = 2;
    }
    public void Play3()
    {
        SelectedLevel = 3;
    }
    public void Play4()
    {
        SelectedLevel = 4;
    }
    public void Play5()
    {
        SelectedLevel = 5;
    }
    public void Play6()
    {
        SelectedLevel = 6;
    }
    public void Play7()
    {
        SelectedLevel = 7;
    }
    public void Play8()
    {
        SelectedLevel = 8;
    }
    public void Play9()
    {
        SelectedLevel = 9;
    }
    public void Play10()
    {
        SelectedLevel = 10;
    }
    public void RemoveAds()
    {

    }
    public void GetFreeCash()
    {
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 200);
        RewardedCash.Play();
        CashRecieved.Play();
    }
    public void RateUs()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=7770693116839328150");
    }
    public void Store()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=7770693116839328150");
    }
    public void Shop()
    {
        if(MainScreen.activeSelf)
        {
            MainScreen.SetActive(false);
        }
        ShopScreen.SetActive(true);
        if(LevelSelection.instance)
        {
            LevelSelection.instance.OpeningShop();
        }
        if(Modes.instance)
        {
            Modes.instance.OpeningShop();
        }
        if(CharacterSelection.instance)
        {
            CharacterSelection.instance.OpeningShop();
        }
        

    }
    public void ShowRewarded()
    {
        AdsManager.instance.Show_Rewarded(GetFreeCash);
    }
    public void Policy()
    {
        Application.OpenURL("https://spheregamez.com/privacy-policy");
    }
    public void DownloadNow()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=7770693116839328150");
    }
    public void BackonMain()
    {
        ConfirmExitScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
    public void Play()
    {
        ActiveScreen = ModesScreen;
        ModesScreen.SetActive(true);
        MainScreen.SetActive(false);
    }
    public void Back()
    {
        ConfirmExitScreen.SetActive(true);
        MainScreen.SetActive(false);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void BackfromExit()
    {
        ConfirmExitScreen.SetActive(false);
        MainScreen.SetActive(true);
    }
    public void HideInsufficientBalanceScreen()
    {
        InsuffBalanceScreen.SetActive(false);
        CharacterSelection.instance.enablespiders();
    }
    public void StartGame()
    {
        if (LevelSelectionScreen.activeSelf)
            LevelSelectionScreen.SetActive(false);
        SceneManager.LoadScene(2);
    }   

}
