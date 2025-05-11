using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FriendSpidyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator Controller;
    public static FriendSpidyScript instance;
    public GameObject Blood;
    public int TotalHealth;
    public int health;
    public GameObject FriendHealth;
    public Image FriendHealthBar;
    public GameObject WayPoint;
    void Start()
    {
        instance = this;
        if(GameManager.EnabledLevel!=8)
        {
            FriendHealthBar.fillAmount = 1f;
            FriendHealth.SetActive(true);
            health = TotalHealth;
        }
        
    }

    // Update is called once per frame
    void Update()
    {  
        if(WayPoint)
        {
            float distance= Vector3.Distance(this.gameObject.transform.position, GameManager.instance.PlayerSpider.transform.position);
            if (distance<10f)
            {
                if(WayPoint.activeSelf)
                {
                    WayPoint.SetActive(false);
                }
            }
            else
            {
                if(!WayPoint.activeSelf)
                {
                    WayPoint.SetActive(true);
                }
            }
        }
    }
    public void BloodSplash()
    {
        if(this.gameObject.activeSelf)
        {
            float Negative=0;
            if (GameManager.EnabledLevel==6)
            {
                health -= 2;
                Negative = 2f / TotalHealth;
            }
            else if(GameManager.EnabledLevel==7)
            {
                health -= 10;
                Negative = 10f / TotalHealth;
            }
            if(health==0)
            {
                SpidyScript.instance.FriendDied();
                   
            }
            FriendHealthBar.fillAmount -= Negative;
            Controller.SetTrigger("damage");
            Blood.SetActive(true);
            Invoke("DisableBlood", 1f);
        }
    }
    public void DisableBlood()
    {
        Blood.SetActive(false);
    }
}
