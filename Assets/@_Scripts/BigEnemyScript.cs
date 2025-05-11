using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyScript : MonoBehaviour
{
    public static BigEnemyScript instance;
    public Animator ThisPlayerAnims;
    public bool IsDead;
    private Vector3 previousPosition;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        previousPosition = this.transform.position;   
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != previousPosition)
        {
            ThisPlayerAnims.SetInteger("Walk", 1);
            // The character is moving.
        }
        else
        {
            ThisPlayerAnims.SetInteger("Walk", 0);
            // The character is in an idle state.
        }

        // Update the previous position for the next frame.
        previousPosition = transform.position;
    }
    public void Attack()
    {
        Debug.Log("Attacked");
        this.ThisPlayerAnims.SetTrigger("Attack");
    }
    public void Death()
    {
        if (GameManager.EnabledLevel == 10 &&!IsDead)
        {
            SpidyScript.Enemies++;
        }
        IsDead = true;
        
        this.ThisPlayerAnims.SetTrigger("Death");
        
    }
}
