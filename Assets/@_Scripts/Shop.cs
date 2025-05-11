using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public static Shop instance;
    public static GameObject PreviousScreen;
    public GameObject ShopScreen;
    public GameObject Coins;
    public GameObject Cash;
    public GameObject CoinsClicked;
    public GameObject CashClicked;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
    public void CashClick()
    {
        CoinsClicked.SetActive(false);
        CashClicked.SetActive(true);
        Coins.SetActive(false);
        Cash.SetActive(true);
    }
    public void CoinsClick()
    {
        CoinsClicked.SetActive(true);
        CashClicked.SetActive(false);
        Coins.SetActive(true);
        Cash.SetActive(false);
    }
    public void Back()
    {
        MenuManager.ActiveScreen.SetActive(true);
        ShopScreen.SetActive(false);
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
