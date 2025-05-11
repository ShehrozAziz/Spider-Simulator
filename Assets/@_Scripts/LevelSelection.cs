using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelection : MonoBehaviour
{
    public static LevelSelection instance;
    public GameObject LevelSelectionScreen;
    public GameObject CharacterSelectionScreen;
    public GameObject ModesScreen;
    public List<GameObject> Locks;
    public List<GameObject> Cleared;
    public List<GameObject> ToPlay;
    public List<GameObject> Selected;
    public List<Button> Levels;
    public bool Istest = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
        MenuManager.SelectedLevel = PlayerPrefs.GetInt("Cleared") + 1;
        for (int i = 0; i < 10; i++)
        {
            if (i < PlayerPrefs.GetInt("Cleared"))
            {
                Cleared[i].SetActive(true);
                Locks[i].SetActive(false);
                ToPlay[i].SetActive(false);
                Selected[i].SetActive(false);
            }
            else if (i == PlayerPrefs.GetInt("Cleared"))
            {
                Cleared[i].SetActive(false);
                ToPlay[i].SetActive(true);
                Selected[i].SetActive(true);
                Locks[i].SetActive(false);
                Levels[i].gameObject.GetComponent<Animator>().enabled = true;

            }
            else
            {
                Selected[i].SetActive(false);
                if(!Istest)
                {
                    Levels[i].interactable = false;  //<----This is what I am Talking about
                }
                Cleared[i].SetActive(false);
                ToPlay[i].SetActive(false);
                Locks[i].SetActive(true);
            }
        }
    }
    public void Diffuse(List<GameObject> Set,int total,int index)
    {
        for(int i=0;i<total;i++)
        {
            if(i==index)
            {
                Set[i].SetActive(true);
            }
            else
            {
                Set[i].SetActive(false);
            }
        }
    }
    public void One()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 0 || Istest)
        {
            MenuManager.SelectedLevel = 1;
            Diffuse(Selected, Selected.Count, 0);
        }

    }
    public void Two()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 1 || Istest)
        {
            MenuManager.SelectedLevel = 2;
            Diffuse(Selected, Selected.Count, 1);
        }
    }
    public void Three()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 2 || Istest)
        {
            MenuManager.SelectedLevel = 3;
            Diffuse(Selected, Selected.Count, 2);
        }
    }
    public void Four()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 3 || Istest)
        {
            MenuManager.SelectedLevel = 4;
            Diffuse(Selected, Selected.Count, 3);
        }
    }
    public void Five()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 4 || Istest)
        {
            MenuManager.SelectedLevel = 5;
            Diffuse(Selected, Selected.Count, 4);
        }
    }
    public void Six()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 5 || Istest)
        {
            MenuManager.SelectedLevel = 6;
            Diffuse(Selected, Selected.Count, 5);
        }
    }
    public void Seven()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 6 || Istest)
        {
            MenuManager.SelectedLevel = 7;
            Diffuse(Selected, Selected.Count, 6);
        }
    }
    public void Eight()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 7 || Istest)
        {
            MenuManager.SelectedLevel = 8;
            Diffuse(Selected, Selected.Count, 7);
        }
    }
    public void Nine()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 8 || Istest)
        {
            MenuManager.SelectedLevel = 9;
            Diffuse(Selected, Selected.Count, 8);
        }
    }
    public void Ten()
    {
        if (PlayerPrefs.GetInt("Cleared") >= 9 || Istest)
        {
            MenuManager.SelectedLevel = 10;
            Diffuse(Selected, Selected.Count, 9);
        }
    }
    public void Play()
    {
        MenuManager.ActiveScreen = CharacterSelectionScreen;
        LevelSelectionScreen.SetActive(false);
        CharacterSelectionScreen.SetActive(true);
    }
    public void Back()
    {
        MenuManager.ActiveScreen = ModesScreen;
        ModesScreen.SetActive(true);
        LevelSelectionScreen.SetActive(false);
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
