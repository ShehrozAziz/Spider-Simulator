using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modes : MonoBehaviour
{
    public static Modes instance;
    public GameObject GoldenFreeMode;
    public GameObject GoldenLevelMode;
    public GameObject MainMenuScreen;
    public GameObject LevelSelectionScreen;
    public GameObject CharacterSelectionScreen;
    public GameObject ModesScreen;
    public static bool LevelModeClicked=true;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        LevelModeClicked = true;
    }
    public void FreeMode()
    {
        LevelModeClicked = false;
        GoldenFreeMode.SetActive(true);
        GoldenLevelMode.SetActive(false);
    }
    public void LevelsMode()
    {
        LevelModeClicked = true;
        GoldenFreeMode.SetActive(false);
        GoldenLevelMode.SetActive(true);
    }
    public void Play()
    {
        if(LevelModeClicked)
        {
            MenuManager.ActiveScreen = LevelSelectionScreen;
            LevelSelectionScreen.SetActive(true);
            PlayerPrefs.SetInt("Free", -1);
        }
        else
        {
            MenuManager.ActiveScreen = CharacterSelectionScreen;
            CharacterSelectionScreen.SetActive(true);
            PlayerPrefs.SetInt("Free", 1);
        }
        ModesScreen.SetActive(false);
    }
    public void Back()
    {
        MenuManager.ActiveScreen = MainMenuScreen;
        MainMenuScreen.SetActive(true);
        ModesScreen.SetActive(false);
    }
    public void OpeningShop()
    {
        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
