using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Loading : MonoBehaviour
{
    // Start is called before the first frame update
    //public ShowBigBanner Obj;
    static bool SameScene;
    void Start()
    {
        
    }
    private void OnEnable()
    {
        //Obj.enabled = true;
        Invoke("PlayTheGame", 5f);
    }

    public void PlayTheGame()
    {
        //Obj.enabled = false;
        if(!SameScene)
        {
            SceneManager.LoadScene(2);
        }
    }
}
