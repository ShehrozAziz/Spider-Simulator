using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public static CharacterSelection instance;
    public GameObject CharacterSelectionScreen;
    public GameObject LevelSelectionScreen;
    public GameObject ModesScreen;
    public GameObject SpecsandSprites;
    public GameObject BuyonPlayButton;
    public GameObject StartonPlayButton;
    public GameObject InsufficientBalanceScreen;
    public GameObject LoadingScreen;
    public List<int> Unlocks;
    public List<int> Prices;
    public List<GameObject> Goldens;
    public List<GameObject> Specs;
    public GameObject Spiders;
    public List<GameObject> Sprites;
    public List<Animator> SpidersControllers;
    public List<GameObject> Locks;
    public List<GameObject> Price;
    public List<GameObject> Purchased;
    int Clicked = 0;
    public static int Selected = 0;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        for (int i = 0; i < 4; i++)
        {
            if (PlayerPrefs.GetInt("CUnlock" + i.ToString()) == 1)
            {
                Locks[i].SetActive(false);
            }
            else
            {
                Locks[i].SetActive(true);
            }
        }
    }
    private void OnEnable()
    {
        //AdsManager.instance.Show_Interstitial();
        Spiders.SetActive(true);
        Invoke("ShowAttack", 0.2f);
    }
    public void enablespiders()
    {
        if(!Spiders.activeSelf)
        {
            Spiders.SetActive(true);
            Invoke("ShowAttack", 0.2f);
        }
    }
    public void diffuse(List<GameObject> list,int size,int index)
    {
        for(int i=0;i<size;i++)
        {
            if(i==index)
            {
                list[i].SetActive(true);
            }
            else
            {
                list[i].SetActive(false);
            }
        }
    }
    public void ShowAttack()
    {
        SpidersControllers[Clicked].SetTrigger("attack");
    }
    public void Clickone()
    {
        Clicked = 0;
        SpecsandSprites.SetActive(false);
        diffuse(Specs, Specs.Count, 0);
        Spiders.SetActive(false);
        diffuse(Sprites, Sprites.Count, 0);
        Spiders.SetActive(true);
        Invoke("ShowAttack", 0.2f);
        
        if (PlayerPrefs.GetInt("CUnlock" + 0.ToString()) == 1)
        {


            Price[0].SetActive(false);
            Purchased[0].SetActive(true);
            diffuse(Goldens, Goldens.Count, 0);
            BuyonPlayButton.SetActive(false);
            StartonPlayButton.SetActive(true);
        }
        else
        {
            BuyonPlayButton.SetActive(true);
            StartonPlayButton.SetActive(false);
        }
        SpecsandSprites.SetActive(true);
    }
   
    public void Clicktwo()
    {
        Clicked = 1;
        SpecsandSprites.SetActive(false);
        diffuse(Specs, Specs.Count, 1);
        Spiders.SetActive(false);
        diffuse(Sprites, Sprites.Count, 1);
        Spiders.SetActive(true);
        Invoke("ShowAttack", 0.2f);

        if (PlayerPrefs.GetInt("CUnlock" + 1.ToString()) == 1)
        {
            Price[1].SetActive(false);
            Purchased[1].SetActive(true);
            diffuse(Goldens, Goldens.Count, 1);
           
            
            BuyonPlayButton.SetActive(false);
            StartonPlayButton.SetActive(true);
        }
        else
        {
            BuyonPlayButton.SetActive(true);
            StartonPlayButton.SetActive(false);
        }
        SpecsandSprites.SetActive(true);
    }
    public void Clickthree()
    {
        Clicked = 2;
        SpecsandSprites.SetActive(false);
        diffuse(Specs, Specs.Count, 2);
        Spiders.SetActive(false);
        diffuse(Sprites, Sprites.Count, 2);
        Spiders.SetActive(true);
        Invoke("ShowAttack", 0.2f);

        if (PlayerPrefs.GetInt("CUnlock" + 2.ToString()) == 1)
        {

            Price[2].SetActive(false);
            Purchased[2].SetActive(true);
            diffuse(Goldens, Goldens.Count, 2);
           
           
            BuyonPlayButton.SetActive(false);
            StartonPlayButton.SetActive(true);
        }
        else
        {
            BuyonPlayButton.SetActive(true);
            StartonPlayButton.SetActive(false);

        }
        SpecsandSprites.SetActive(true);
    }
    public void Clickfour()
    {
        Clicked = 3;
        SpecsandSprites.SetActive(false);
        diffuse(Specs, Specs.Count, 3);
        Spiders.SetActive(false);
        diffuse(Sprites, Sprites.Count, 3);
        Spiders.SetActive(true);
        Invoke("ShowAttack", 0.2f);

        if (PlayerPrefs.GetInt("CUnlock" + 3.ToString()) == 1)
        {
            Price[3].SetActive(false);
            Purchased[3].SetActive(true);
            diffuse(Goldens, Goldens.Count, 3);
            
            BuyonPlayButton.SetActive(false);
            StartonPlayButton.SetActive(true);
        }
        else
        {
            BuyonPlayButton.SetActive(true);
            StartonPlayButton.SetActive(false);
        }
        SpecsandSprites.SetActive(true);
    }
    public void PlayorBuy()
    {
        if (PlayerPrefs.GetInt("CUnlock" + Clicked.ToString()) == 1)
        {
            Selected = Clicked;
            Spiders.SetActive(false);
            Play();
        }
        else
        {
            if(PlayerPrefs.GetInt("Cash") > Prices[Clicked])
            {
                Locks[Clicked].SetActive(false);
                int temp = PlayerPrefs.GetInt("Cash");
                temp -= Prices[Clicked];
                PlayerPrefs.SetInt("Cash", temp);
                BuyonPlayButton.SetActive(false);
                StartonPlayButton.SetActive(true);
                SpecsandSprites.SetActive(false);
                diffuse(Goldens, Goldens.Count, Clicked);
                diffuse(Specs, Specs.Count, Clicked);
                diffuse(Sprites, Sprites.Count, Clicked);
                SpecsandSprites.SetActive(true);
                //Unlocks[Clicked] = 1;
                PlayerPrefs.SetInt("CUnlock" + Clicked.ToString(), 1);
                if(Clicked==0)
                {
                    Clickone();
                }
                else if(Clicked==1)
                {
                    Clicktwo();
                }
                else if(Clicked==2)
                {
                    Clickthree();
                }
                else if(Clicked==3)
                {
                    Clickfour();
                }
            }
            else
            {
                Spiders.SetActive(false);
                InsufficientBalanceScreen.SetActive(true);
            }
        }
    }
    public void Back()
    {
        if (Modes.LevelModeClicked)
        {
            LevelSelectionScreen.SetActive(true);
            CharacterSelectionScreen.SetActive(false);
        }
        else
        {
            CharacterSelectionScreen.SetActive(false);
            ModesScreen.SetActive(true);
        }
        Spiders.SetActive(false);
        CharacterSelectionScreen.SetActive(false);
            
    }
    public void OpeningShop()
    {
        if (this.gameObject.activeSelf)
        {
            Spiders.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
    public void Play()
    {
        MenuManager.SelectedSpider = Selected;
        //AdsManager.instance.Show_Interstitial();
        LoadingScreen.SetActive(true);
        CharacterSelectionScreen.SetActive(false);
    }
}
