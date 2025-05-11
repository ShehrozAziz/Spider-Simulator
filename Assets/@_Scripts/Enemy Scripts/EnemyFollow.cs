using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public static EnemyFollow instance;
    public NavMeshAgent Enemy;
    public Transform PlayerToFollow;
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
                if (PlayerToFollow == GameManager.instance.FirstSpider.transform)
                {
                    PlayerToFollow = GameManager.instance.PlayerSpider.transform;
                    PlayerAssigned = true;
                }
            }

        }
        Enemy.SetDestination(PlayerToFollow.position);
    }
    public void ChangeEnemy(Transform Player)
    {
        PlayerToFollow = Player;
    }
}
