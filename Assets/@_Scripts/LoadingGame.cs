using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGame : MonoBehaviour
{
    public bool Restarting;
    //public ShowBigBanner obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void NotRestart()
    {
        Restarting = false;
    }
    public void Restart()
    {
        Restarting = true;
    }
    private void OnEnable()
    {
        //obj.enabled = true;
        Invoke("PlayTheGame", 5f);
    }

    public void PlayTheGame()
    {
        if(Restarting)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(1);
        }
        //obj.enabled = false;
        
    }
    
}
