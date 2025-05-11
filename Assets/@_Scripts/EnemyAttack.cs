using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject Player;
    public GameObject Enemy;
    public bool CanAttack=false;
    public float AttackDelay;
    public BigEnemyScript BigEnemy;
    bool once = false;
    bool invoked=false;
    bool OpenedAttack = false;
    bool PlayerAssigned = false;
    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerAssigned)
        {
            if (GameManager.instance.PlayerSpider)
            {
                if (Enemy == Player)
                {
                    Enemy = GameManager.instance.PlayerSpider;
                    PlayerAssigned = true;
                }
                Player = GameManager.instance.PlayerSpider;
            }
        }
        float Distance = Vector3.Distance(this.transform.position, Enemy.transform.position);
        if(Distance<=5f && !once)
        {
            Debug.Log("Opened Attack");
            OpenedAttack = true;
            if(!invoked)
            {
                CanAttack = true;
            }
            once = true;
        }
        if(Distance>5f)
        {
            OpenedAttack = false;
            once = false;
        }
        if (CanAttack)
        {
            if (GameManager.EnabledLevel == 6)
            {
                FriendSpidyScript.instance.BloodSplash();
            }
            else if((GameManager.EnabledLevel==7 || GameManager.EnabledLevel == 4 || GameManager.EnabledLevel==10) && !BigEnemy.IsDead)
            {
                BigEnemy.Attack();
                if(GameManager.EnabledLevel==4 || (GameManager.EnabledLevel==10 && this.CompareTag("Boar")))
                {
                    Invoke("AnimationDelay", 0.82f);
                }
                else if(GameManager.EnabledLevel==7 || (GameManager.EnabledLevel == 10 && this.CompareTag("Wolf")))
                {
                    Invoke("AnimationDelay", 0.82f);
                }
                
            }
            else if(GameManager.EnabledLevel==9 || GameManager.EnabledLevel==3 || GameManager.EnabledLevel==5)
            {
                SpidyScript.instance.TakeSmallDamage();
            }
            else if(PlayerPrefs.GetInt("Free")==1)
            {
                if(this.CompareTag("Wolf")|| this.CompareTag("Bear"))
                {
                    BigEnemy.Attack();
                    //SpidyScript.instance.TakeBigDamage();
                    Invoke("AnimationDelay", 0.82f);
                }
                else if(this.CompareTag("Mosquito")|| this.CompareTag("Scarab"))
                {
                    SpidyScript.instance.TakeSmallDamage();
                    Debug.Log("Attacked");
                }
            }
            //Debug.Log("Attacked");
            CanAttack = false;
            invoked = true;
            Invoke("OpenAttack", AttackDelay);  
        }
    }
    public void AnimationDelay()
    {
        if(Enemy==GameManager.instance.PlayerSpider)
        {
            SpidyScript.instance.TakeBigDamage();
        }
        else
        {
            FriendSpidyScript.instance.BloodSplash();
        }
        
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject == Enemy.gameObject && GameManager.EnabledLevel!=7)
    //    { 
    //        Debug.Log("Opened Attack");
    //        CanAttack = true;
    //    }
    //}
    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject == Enemy.gameObject && GameManager.EnabledLevel != 7)
    //    {
    //        CanAttack = false;
    //    }
    //}

    public void OpenAttack()
    {
        if(OpenedAttack)
        {
            CanAttack = true;
        }
        invoked = false;
        
    }
    public void ChangeEnemy(GameObject Send)
    {
        Debug.Log("Enemy Changed");
        Enemy = null;
        Enemy = Send;
    }
}
