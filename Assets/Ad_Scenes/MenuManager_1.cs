using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager_1 : MonoBehaviour
{

    public void ShowInterstitial()
    {
        AdsManager.instance.Show_Interstitial();
    }

    public void ShowRewarded()
    {
       AdsManager.instance.Show_Rewarded(GiveReward);
    }

    public void GiveReward()
    {
        print("<color=green> CONGRATULATIONS - Reward Granted  </color>");
    }

    public void ReloadAds()
    {
        Application.LoadLevel(0);
    }
}
