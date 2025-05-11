using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawAttackZone : MonoBehaviour
{
    public float DamageAmount;

    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        //   SpidyScript.instance.AddinAttackZone(other);
        if (other.gameObject.GetComponent<EnemyHealth>())
        {
            other.gameObject.GetComponent<EnemyHealth>().Damage(DamageAmount);
            if(other.CompareTag("Cage") || other.CompareTag("Finish"))
            {
                SpidyController.instance.HittingFlesh = false;
            }
            else
            {
                SpidyController.instance.HittingFlesh = true;
            }
            
        }
        else
        {
            SpidyController.instance.HittingFlesh = false;
        }
    }
    
    
}
