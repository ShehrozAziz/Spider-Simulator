using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    
    public float _healthPoints;
    public static bool Destroyed;

    float TotalPoints;

    public Image HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        TotalPoints = _healthPoints;
    }

    
    public void Damage(float _damage)
    {
        _healthPoints -= _damage;
        print(_healthPoints+" : Enemy Health");
        if (_healthPoints < 1)
        {
            Destroyed = true;
            if(GameManager.EnabledLevel==3)
            {
                if(this.CompareTag("Finish"))
                {
                    SpidyScript.instance.Level3Ended();
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
            if(GameManager.EnabledLevel==5)
            {
                this.gameObject.SetActive(false);
                SpidyScript.AnimalonAttack = false;
                SpidyScript.Mosquitos--;
                if(SpidyScript.Mosquitos==0)
                {
                    SpidyScript.instance.Complete();
                }

            }
            if (GameManager.EnabledLevel == 6)
            {
                this.gameObject.SetActive(false);
                SpidyScript.AnimalonAttack = false;
            }
            else if(GameManager.EnabledLevel==9)
            {
                this.gameObject.SetActive(false);
                SpidyScript.AnimalonAttack = false;
                SpidyScript.ScrubsKilled++;
                if(SpidyScript.ScrubsKilled==6)
                {
                    SpidyScript.instance.Complete();
                }
            }
            else if(GameManager.EnabledLevel==10)
            {

                if(this.CompareTag("Cage"))
                {
                    SpidyScript.Enemies++;
                    this.gameObject.SetActive(false);
                    SpidyScript.instance.CageBroken();
                }
                else if(this.CompareTag("Wolf")|| this.CompareTag("Boar"))
                {
                    this.gameObject.GetComponent<BigEnemyScript>().Death();
                    this.gameObject.GetComponent<EnemyFollow>().enabled = false;
                    this.gameObject.GetComponent<StartFollow>().enabled = false;
                    this.gameObject.GetComponent<EnemyHealth>().enabled = false;
                }
            }
            else if(GameManager.EnabledLevel==1 || GameManager.EnabledLevel==2)
            {
                SpidyScript.AnimalonAttack = false;
                this.gameObject.SetActive(false);
                SpidyScript.BeesCaught++;
                if(GameManager.EnabledLevel==1)
                {
                    SpidyScript.instance.CheckLevel1();
                }
                else
                {
                    SpidyScript.instance.CheckLevel2();                
                }
            }
            else if(PlayerPrefs.GetInt("Free")==1)
            {
                Debug.Log("Killed");
                int temp = PlayerPrefs.GetInt("Cash");
                int temp2 = PlayerPrefs.GetInt("Coins");
                if(this.CompareTag("Bee"))
                {
                    temp += 100;
                    temp2 += 10;
                    this.gameObject.SetActive(false);
                }
                else if(this.CompareTag("Mosquito"))
                {
                    this.gameObject.GetComponent<EnemyAttack>().enabled = false;
                    temp += 300;
                    temp2 += 30;
                    this.gameObject.SetActive(false);
                }
                else if(this.CompareTag("Scarab"))
                {
                    this.gameObject.GetComponent<EnemyAttack>().enabled = false;
                    temp += 500;
                    temp2 += 50;
                    this.gameObject.SetActive(false);
                }
                else if(this.CompareTag("Wolf"))
                {
                    this.gameObject.GetComponent<EnemyAttack>().enabled = false;
                    this.gameObject.GetComponent<BigEnemyScript>().Death();
                    this.gameObject.GetComponent<EnemyFollow>().enabled = false;
                    this.gameObject.GetComponent<StartFollow>().enabled = false;
                    this.gameObject.GetComponent<EnemyHealth>().enabled = false;
                    temp += 2000;
                    temp2 += 200;
                }
                else if(this.CompareTag("Bear"))
                {
                    this.gameObject.GetComponent<EnemyAttack>().enabled = false;
                    this.gameObject.GetComponent<BigEnemyScript>().Death();
                    this.gameObject.GetComponent<EnemyFollow>().enabled = false;
                    this.gameObject.GetComponent<StartFollow>().enabled = false;
                    this.gameObject.GetComponent<EnemyHealth>().enabled = false;
                    
                    temp += 3000;
                    temp2 += 300;
                }
                  //hatadobhai
                PlayerPrefs.SetInt("Cash", temp);
                PlayerPrefs.SetInt("Coins", temp2);
            }
            
            SpidyScript.instance.EnemyDestroyed();
        }
    }
    private void Update()
    {
        if(HealthBar)
        {
            float temp = _healthPoints / TotalPoints;
            HealthBar.fillAmount = temp;
        }
    }
}
