using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowBigBanner : MonoBehaviour
{
    private void OnEnable()
    {
        AdsManager.instance.Show_Big_Banner();
    }

    private void OnDisable()
    {
        AdsManager.instance.Destroy_Big_Banner();
    }
}
