using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpidyScript : MonoBehaviour
{
    
    public static SpidyScript instance;

    public SpidyController SpiderController;
    public Animator PlayerAnims;

    public GameObject web;
    public GameObject Blood;
    public GameObject BloodImg;
    public float blooddelay;
    public GameObject OtherAnimalBlood;
    public GameObject ControllerPanel;
    public GameObject MainCamera;
    public GameObject DuplicatePlayer;
    public GameObject Player;

    public BoxCollider damageCollider;

    public static bool AnimalonAttack;
    public float AttackDelayTime;
    public float TotalHealth;
    public float SpidyHealth;
    public Image SpidyHealthBar;

    bool Dead = false;
    //level 1
    public GameObject FrontCamera;
    public GameObject SizeUpText;
    public static int BeesCaught = 0;

    //level 2
    public GameObject Wife;
    public GameObject WebonTree;
    public GameObject GlueAmount;
    public GameObject GototheTreeText;
    public GameObject StayHereMakingGlue;
    public GameObject SideCamera;
    public GameObject WebMakingMachine;
    public Image GlueAmountImage;
    
    //level 3
    public GameObject TopViewCamera;
    public GameObject HoneyBees;
    public GameObject HoneyBeesTalkZone;
    public GameObject Scrubs;
    public GameObject ScrubsCave;
    public GameObject DestroyCaveText;
    
    //level 4
    public GameObject BoarEyes;
    public GameObject Boar;
    public EnemyFollow BoartoPlayer;
    public BigEnemyScript BoarController;
    public float DistanceOffset = 2.5f;
    
    //level 5
    public static int Mosquitos=6;
    public GameObject MosquitoObject;
    
    //level 6
    public GameObject FriendSpider;
    public GameObject Enemiesontheway;
    public bool IntheDropZone;
    
    //level 7
    public EnemyAttack WolfAttack;
    public EnemyFollow WolfFollow;
    public BigEnemyScript WolfBigEnemy;
    public GameObject BabySpiderDrop;
    public EnemyFollow BabySpiderFollow;
    public GameObject SpiderWayPoint;
    
    //Level 8
    public List<EnemyFollow> WolfsFollow;
    public List<Transform> WolfsPoints;
    public GameObject ToEnable;
    public GameObject CutScene;
    public GameObject GuideSpider;
    public GameObject BabySpiderPivot;
    public bool CutSceneEnded;

    //Level 9
    public Transform OneScrub;
    public GameObject ScrubsPoint;
    public static int ScrubsKilled=0;
    
    //Level 10
    public EnemyFollow Wifefollow;
    public GameObject Container;
    public GameObject FinalCutScene;
    public static int Enemies = 0;
    
    void Start()
    {
        blooddelay = 0.6f;
        instance = this;
    }
    void Update()
    {
        float temp = SpidyHealth / TotalHealth;
        SpidyHealthBar.fillAmount = temp;
        if(SpidyHealth<2)
        {
            Die();
        }
        //Level 6
        if(GameManager.EnabledLevel == 6)
        {
            if(IntheDropZone)
            {
                if(Vector3.Distance(Player.transform.position,FriendSpider.transform.position)<5f)
                {
                    IntheDropZone = false;
                    Complete();
                }
            }
        }
        //Level 7
        if(GameManager.EnabledLevel==7)
        {
            float Distance = Vector3.Distance(Player.transform.position, WolfBigEnemy.transform.position);
            if(Distance<DistanceOffset)
            {
                WolfFollow.ChangeEnemy(Player.transform);
                WolfAttack.ChangeEnemy(Player);
                BabySpiderFollow.enabled = true;
                SpiderWayPoint.SetActive(false);
            }
        }
        //Level 9
        if(GameManager.EnabledLevel==9)
        {
            float Distance = Vector3.Distance(Player.transform.position, OneScrub.position);
            if(Distance<DistanceOffset)
            {
                ScrubsPoint.SetActive(false);
            }
        }
    }
    
    public void Die()
    {
        if (!Dead)
        {
            GameManager.instance.UserDied();
            Dead = true;
        }
            
    }
   
    private void OnTriggerEnter(Collider other)
    {
        
        if (GameManager.EnabledLevel==2 && other.CompareTag("WebZone"))
        {
            StopWalk();
            other.gameObject.SetActive(false);
            Player.SetActive(false);
            DuplicatePlayer.SetActive(true);
            GototheTreeText.SetActive(false);
            MainCamera.SetActive(false);
            ControllerPanel.SetActive(false);
            SideCamera.SetActive(true);
            PlayerAnims.SetTrigger("Climbing");
            Invoke("StopWalk", 6f);
            Invoke("Complete", 10f);
        }
        if(GameManager.EnabledLevel==2&&other.CompareTag("WallRun"))
        {
            GlueAmount.SetActive(true);
            StayHereMakingGlue.SetActive(true);
            MakeWeb();
        }
        else if(GameManager.EnabledLevel == 6 && other.CompareTag("WallRun"))
        {
            if (!Enemiesontheway.activeSelf)
            {
                Enemiesontheway.SetActive(true);
                FriendSpider.GetComponent<EnemyFollow>().enabled = true;
                other.gameObject.SetActive(false);
            }
            else
            {
                IntheDropZone = true;
                
            }
        }
        else if(GameManager.EnabledLevel==7 && other.CompareTag("WallRun"))
        {
            Complete();
        }
        else if(GameManager.EnabledLevel==8 && other.CompareTag("WallRun"))
        {
            ControllerPanel.SetActive(false);
            StarttheScene();
        }
        if (other.CompareTag("TalkZone") && GameManager.EnabledLevel==3) 
        {
            Debug.Log("InTalkZone");
            TalkToBees();
            
        }
       
    }
    private void OnTriggerExit(Collider other)
    {
        if (GameManager.EnabledLevel == 2 && other.CompareTag("WallRun"))
        {
            BeesCaught = 5;
            GlueAmountImage.fillAmount = 0f;
            GlueAmount.SetActive(false);
            StayHereMakingGlue.SetActive(false);

        }
        if(Enemiesontheway.activeSelf && other.CompareTag("WallRun")) //Level 6
        {
            IntheDropZone = false;
        }
    }
    public void EnemyDestroyed()
    {
        if(PlayerPrefs.GetInt("Free")==-1)
        {
            
            if (GameManager.EnabledLevel == 4)
            {
                Debug.Log("Boar Died");
                BoarController.Death();
                BoartoPlayer.enabled = false;
                Invoke("Complete", 3.33f);
            }
            if (GameManager.EnabledLevel == 7)
            {
                WolfBigEnemy.Death();
                WolfFollow.enabled = false;
                WolfAttack.enabled = false;
                WolfBigEnemy.enabled = false;
                BabySpiderDrop.SetActive(true);
            }
            if (GameManager.EnabledLevel == 10)
            {
                if (Enemies == 6)
                {
                    AllKilled();
                }
            }

        }
        
        
    }
    public void Attack() => StartCoroutine(AttackTime()); //StartCoroutine(AttackTime());
    public void OtherBlood()
    {
        Invoke("EnableOtherBlood", 0.5f);
        Invoke("DisableOtherBlood", 1f);
    }
    IEnumerator AttackTime()
    {

        if (AnimalonAttack)
            OtherBlood();
        
        yield return new WaitForSeconds(AttackDelayTime);
        damageCollider.enabled = true;

        yield return new WaitForSeconds(0.2f);
        damageCollider.enabled = false;

    }
    public void BloodSplash()
    {
        Blood.SetActive(true);
        BloodImg.SetActive(true);
        Invoke("DisableBloodSplash", blooddelay);
    }
    public void DisableBloodSplash()
    {
        BloodImg.SetActive(false);
        Blood.SetActive(false);
    }
    public void EnableOtherBlood()
    {
        OtherAnimalBlood.SetActive(true);
    }
    public void DisableOtherBlood()
    {
        OtherAnimalBlood.SetActive(false);
    }
    public void Web()
    {
        web.SetActive(true);
        Invoke("DisableWeb", 1.33f);

    }
    public void DisableWeb()
    {
        web.SetActive(false);
    }
    public void FriendDied()
    {
        GameManager.instance.FriendDied();
        if (GameManager.EnabledLevel == 6)
        {
            Enemiesontheway.SetActive(false);
        }
        else if(GameManager.EnabledLevel==7)
        {
            WolfBigEnemy.gameObject.SetActive(false);
        }
    }
    public void TakeSmallDamage()
    {
        SpidyHealth -= 3;
        SpidyController.instance.TakeDamage();
    }
    public void TakeBigDamage()
    {
        SpidyHealth -= 10;
        SpidyController.instance.TakeDamage();
    }
    public void Complete()
    {
        if (GameManager.EnabledLevel == 2)
        {
            if (!MainCamera.activeSelf)
                MainCamera.SetActive(true);
            if (DuplicatePlayer.activeSelf)
            {
                DuplicatePlayer.SetActive(false);
                Player.SetActive(false);
            }
        }
        if (!MainCamera.activeSelf)
            MainCamera.SetActive(true);
        GameManager.levelCleared++;
        GameManager.instance.ShowLevelComplete(GameManager.EnabledLevel);
    }
    public void HideButtonClick()
    {
        if(GameManager.EnabledLevel==5)
        {
            MosquitoObject.SetActive(true);
        }
        else if(GameManager.EnabledLevel==8)
        {
            GuideSpider.SetActive(true);
        }
    }


    //-----------------------------------------------------------------------------------------------------------------------------------//
    //Level 1
    public void CheckLevel1()
    {
        if(BeesCaught==3)
        {
            StopWalk();
            SpidyController.instance.IdleState();
            DuplicatePlayer.SetActive(true);
            DuplicatePlayer.transform.SetPositionAndRotation(Player.transform.position, Player.transform.rotation);
            DuplicatePlayer.transform.position = new Vector3(DuplicatePlayer.transform.position.x, DuplicatePlayer.transform.position.y+0.5f, DuplicatePlayer.transform.position.z);
            Player.SetActive(false);
            SizeUpText.SetActive(true);
            MainCamera.SetActive(false);
            FrontCamera.SetActive(true);
            ControllerPanel.SetActive(false);
            //PlayerAnims.enabled = true;
            PlayerAnims.SetTrigger("SizeUp");
            
            //LevelsStartText[GameManager.EnabledLevel - 1].SetActive(true);
            //LevelInfoPanel.SetActive(true);
            Invoke("Complete", 4f);
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------------//
    //Level 2
    public void CheckLevel2()
    {
        if (BeesCaught >= 3)
        {
            Wife.SetActive(false);
            WebMakingMachine.SetActive(true);
        }
    }
    
    public void MakeWeb()
    {

        if (BeesCaught > 0)
        {
            BeesCaught--;
            GlueAmountImage.fillAmount += 0.3f;
            Invoke("MakeWeb", 2f);
        }
        else
        {
            GlueAmount.SetActive(false);
            GlueAmountImage.fillAmount = 0f;
            StayHereMakingGlue.SetActive(false);
            WebMakingMachine.SetActive(false);
            GototheTreeText.SetActive(true);
            WebonTree.SetActive(true);
        }
    }
    public void StopWalk()
    {
        //ControllerAnims.SetInteger("movement", 0);
    }
    //-----------------------------------------------------------------------------------------------------------------------------------//
    //Level 3
    public void Level3Ended()
    {
        Scrubs.SetActive(false);
        ScrubsCave.SetActive(false);
        Complete();
    }
    public void TalkToBees()
    {
        HoneyBees.SetActive(true);
        ControllerPanel.SetActive(false);
        MainCamera.SetActive(false);
        TopViewCamera.SetActive(true);
        SpiderController.IdleState();
        DuplicatePlayer.SetActive(true);
        Player.SetActive(false);
        StopWalk();
        DuplicatePlayer.transform.SetPositionAndRotation(Player.transform.position, Player.transform.rotation);
        Invoke("EndTalk", 7f);
    }
    public void EndTalk()
    {
        HoneyBees.SetActive(false);
        HoneyBeesTalkZone.SetActive(false);
        ControllerPanel.SetActive(true);
        MainCamera.SetActive(true);
        TopViewCamera.SetActive(false);
        Player.SetActive(true);
        DuplicatePlayer.SetActive(false);
        Scrubs.SetActive(true);
        ScrubsCave.SetActive(true);
        DestroyCaveText.SetActive(true);
        Invoke("HideDestroyCaveText", 5);
    }
    public void HideDestroyCaveText()
    {
        DestroyCaveText.SetActive(false);
    }
    //-----------------------------------------------------------------------------------------------------------------------------------//
    //Level 8
    public void StarttheScene()
    {
        MainCamera.SetActive(false);
        ToEnable.SetActive(true);
        CutScene.SetActive(true);
        BabySpiderPivot.SetActive(false);
        StartMovement();
        Invoke("CutSceneEnd", 15f);
    }
    public void CutSceneEnd()
    {
        CutSceneEnded = true;
        CutScene.SetActive(false);
        ToEnable.SetActive(false);
        MainCamera.SetActive(true);
        ControllerPanel.SetActive(true);
        Complete();
    }
    public void StartMovement()
    { 
        StartCoroutine(ChangePaths(3,0));
    }
    private IEnumerator ChangePaths(float Seconds,int StartingIndex)
    {
        if(!CutSceneEnded)
        {
            yield return new WaitForSeconds(Seconds);
            WolfsFollow[StartingIndex%4].ChangeEnemy(WolfsPoints[(StartingIndex%4)]);
            WolfsFollow[(StartingIndex + 1)%4].ChangeEnemy(WolfsPoints[(StartingIndex + 1) % 4]);
            WolfsFollow[(StartingIndex + 2)%4].ChangeEnemy(WolfsPoints[(StartingIndex + 2) % 4]);
            WolfsFollow[(StartingIndex + 3)%4].ChangeEnemy(WolfsPoints[(StartingIndex + 3) % 4]);
            StartingIndex++;
            StartCoroutine(ChangePaths(3, StartingIndex));
        }

    }
    //-----------------------------------------------------------------------------------------------------------------------------------//
    //Level 10
    public void CageBroken()
    {
        Wifefollow.enabled = true;
    }
    public void AllKilled()
    {
        MainCamera.SetActive(false);
        ControllerPanel.SetActive(false);
        Player.SetActive(false);
        Container.SetActive(false);
        FinalCutScene.SetActive(true);
        Invoke("Complete", 10f);
    }
}
