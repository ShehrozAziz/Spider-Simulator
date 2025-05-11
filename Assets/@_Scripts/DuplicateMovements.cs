using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuplicateMovements : MonoBehaviour
{
    public Animator ThisPlayerAnims;
    private Vector3 previousPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position != previousPosition)
        {
            ThisPlayerAnims.SetInteger("movement", 1);
            // The character is moving.
        }
        else
        {
            ThisPlayerAnims.SetInteger("movement", 0);
            // The character is in an idle state.
        }

        // Update the previous position for the next frame.
        previousPosition = transform.position;
    }
}
