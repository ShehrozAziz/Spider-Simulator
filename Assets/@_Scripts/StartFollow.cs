using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFollow : MonoBehaviour
{
    public float DistanceOffset;
    public Transform Player;
    bool PlayerAssigned=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!PlayerAssigned)
        {
            if(GameManager.instance.PlayerSpider)
            {
                if (Player == GameManager.instance.FirstSpider.transform)
                {
                    Player = GameManager.instance.PlayerSpider.transform;
                    PlayerAssigned = true;
                }
            }
            
        }
        if(this.gameObject.activeSelf)
        {
            float distance = Vector3.Distance(this.transform.position, Player.position);
            if (distance < DistanceOffset)
            {
                this.GetComponent<EnemyFollow>().enabled = true;
            }
            else
            {
                this.GetComponent<EnemyFollow>().enabled = false;
            }
        }
        
    }
}
