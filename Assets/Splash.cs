using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loading_Screen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator loading_Screen()
    {

        yield return new WaitForSeconds(5);
        //Application.LoadLevel(1);
        SceneManager.LoadScene(1);
    }
}
