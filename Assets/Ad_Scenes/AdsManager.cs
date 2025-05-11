using System.Collections;

using UnityEngine;
using GoogleMobileAds.Api;

using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;
public class AdsManager : MonoBehaviour
{
    public bool isTest;

    public static AdsManager instance;
    public string Banner_ID;
    public string Big_Banner_ID;
    public string interstitial_ID;
    public string Rewarded_ID;

    private BannerView bannerView;
    private BannerView Big_bannerView;
    private InterstitialAd interstitialAd;
    private RewardedAd rewardedAd;



    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (instance != null)
        {
            //return;

        }
        else
        {
            print("Chalaaa . . . .");
            instance = this;

            if (isTest)
            {
                Banner_ID = "ca-app-pub-3940256099942544/6300978111";
                Big_Banner_ID = "ca-app-pub-3940256099942544/6300978111";
                interstitial_ID = "ca-app-pub-3940256099942544/1033173712";
                Rewarded_ID = "ca-app-pub-3940256099942544/5224354917";
            }
            // Initialize the Google Mobile Ads SDK.

            MobileAds.Initialize(initStatus => { });

            if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
            {
                this.RequestBanner();
            }
            StartCoroutine(LoadInter());
            StartCoroutine(LoadRewarded());
            DontDestroyOnLoad(this.gameObject);
        }

        StartCoroutine(loading_Screen());

    }
    IEnumerator loading_Screen()
    {

        yield return new WaitForSeconds(5);
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);
    }
    public void Call_Load_Interstitial()
    {
        StartCoroutine(LoadInter());
    }

    public void Call_Load_Rewarded()
    {
        StartCoroutine(LoadRewarded());
    }
    IEnumerator LoadInter()
    {
        yield return new WaitForSeconds(3);
        RequestInterstitial();
    }
    IEnumerator LoadRewarded()
    {
        yield return new WaitForSeconds(3);
        Request_Rewarded();
    }
    // Banner Ki Request yahan he
    private void RequestBanner()
    {
#if UNITY_ANDROID
        string adUnitId = Banner_ID;
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.TopRight);



        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

    }


    public void ShowBanner()
    {
        RequestBanner();
    }
    public void DestroyBanner()
    {
        bannerView.Destroy();
    }

    private void Request_Big_Banner()
    {
#if UNITY_ANDROID
        string adUnitId = Big_Banner_ID;
#elif UNITY_IPHONE
            string adUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
            string adUnitId = "unexpected_platform";
#endif

        // Create a 320x50 banner at the top of the screen.
        this.Big_bannerView = new BannerView(adUnitId, AdSize.MediumRectangle, AdPosition.BottomLeft);

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.Big_bannerView.LoadAd(request);

    }

    public void Show_Big_Banner()
    {
        Request_Big_Banner();
    }
    public void Destroy_Big_Banner()
    {
        Big_bannerView.Destroy();
    }
    // Interstitial Ki Request yahan he
    private void RequestInterstitial()
    {
#if UNITY_ANDROID
        string adUnitId = interstitial_ID;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif

        // Initialize an InterstitialAd.
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        InterstitialAd.Load(adUnitId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                          + ad.GetResponseInfo());

                interstitialAd = ad;
            });

        RegisterEventHandlers(interstitialAd);
    }

    private void RegisterEventHandlers(InterstitialAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Interstitial ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Interstitial ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Interstitial ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("------Interstitial ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Interstitial ad full screen content closed.");
            //  Call_Load_Interstitial();
            //   StartCoroutine(LoadInter());
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Interstitial ad failed to open full screen content " +
                           "with error : " + error);
            //  StartCoroutine(LoadInter());
            //   Call_Load_Interstitial();
        };
    }



    // Rewarded Ki Request yahan he
    private void Request_Rewarded()
    {
#if UNITY_ANDROID
        string adUnitId = Rewarded_ID;
#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-3940256099942544/4411468910";
#else
        string adUnitId = "unexpected_platform";
#endif



        // Clean up the old ad before loading a new one.
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();
        adRequest.Keywords.Add("unity-admob-sample");

        // send the request to load the ad.
        RewardedAd.Load(adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("Rewarded ad loaded with response : "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
            });


        RegisterEventHandlers(rewardedAd);
    }


    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            //   Call_Load_Rewarded();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error : " + error);

            //   Call_Load_Rewarded();
        };
    }

    Action Successfunction;
    public void Show_Interstitial()
    {

        Successfunction = null;
        if (PlayerPrefs.GetInt("ADSUNLOCK") != 1)
        {
            if (interstitialAd != null && interstitialAd.CanShowAd())
            {
                Debug.Log("Showing interstitial ad.");
                interstitialAd.Show();
                StartCoroutine(LoadInter());
            }
            else
            {
                //   RequestInterstitial();
                Debug.LogError("Interstitial ad is not ready yet.");
            }
        }
    }

    public void Show_Rewarded(Action RewardedFunction)
    {
        Successfunction = RewardedFunction;
        const string rewardMsg =
         "Rewarded ad rewarded the user. Type: {0}, amount: {1}.";

        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                if (Successfunction != null)
                {
                    Successfunction();
                }
                Call_Load_Rewarded();
                // TODO: Reward the user.
                Debug.Log(String.Format(rewardMsg, reward.Type, reward.Amount));
            });
        }

    }
}