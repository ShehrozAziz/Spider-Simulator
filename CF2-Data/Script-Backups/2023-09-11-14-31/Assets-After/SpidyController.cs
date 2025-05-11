using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpidyController : MonoBehaviour
{
    public Animator myAnimator;
    CharacterController Spidycontroller;

    public float MovingSpeed = 5f;
    public float RotationMovingSpeed = 10f;
    public float JumpForce = 8f;
    public float Jumpvelocity = -0.5f;

    private bool IsJumping;
    private bool IsDoingSomething;
    private float verticalVelocity = 0f;

   
    private void Start()
    {
        IsJumping = false;
        IsDoingSomething = false;
        Spidycontroller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float HorizontalInput = ControlFreak2.CF2Input.GetAxis("Horizontal");
        float VerticalInput = ControlFreak2.CF2Input.GetAxis("Vertical");

        Vector3 move = new Vector3(HorizontalInput, 0f, VerticalInput);
        //if (Mathf.Abs(HorizontalInput) > 0.1f && !IsDoingSomething && !IsJumping)
        //{
        //    float targetRotation = Mathf.Atan2(HorizontalInput, 0f) * Mathf.Rad2Deg;
        //    Quaternion newRotation = Quaternion.Euler(0f, targetRotation, 0f);
        //    transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, Time.deltaTime * RotationMovingSpeed);
        //}
        float AddedMovingSpeed = MovingSpeed;
        if ((HorizontalInput != 0 || VerticalInput != 0)  && !IsDoingSomething)
        {
            if (ControlFreak2.CF2Input.GetKey(KeyCode.LeftShift) && Spidycontroller.isGrounded)
            {
                myAnimator.SetInteger("movement", 2);
                AddedMovingSpeed = MovingSpeed * 2;
            }
            else
            {
                if(!IsJumping)
                    myAnimator.SetInteger("movement", 1);
                else
                {
                    myAnimator.SetInteger("movement", 0);
                    AddedMovingSpeed = MovingSpeed;
                }
                    
                
                ////else
                //{
                //    myAnimator.SetInteger("movement", 0);
                //    
                //}

            }
        }
        else
        {
            myAnimator.SetInteger("movement", 0);
            AddedMovingSpeed = 0f;
        }

        
        if (Spidycontroller.isGrounded)
        {
            IsJumping = false;
            verticalVelocity = Jumpvelocity;
            myAnimator.SetBool("jump", false);
            if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space))
            {
                verticalVelocity = JumpForce;
                IsJumping = true;
                myAnimator.SetBool("jump",true);
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }
        move = transform.TransformDirection(move);
        Spidycontroller.Move((move * AddedMovingSpeed + new Vector3(0f, verticalVelocity, 0f)) * Time.deltaTime);
        if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Q) && !IsDoingSomething)
        {
            myAnimator.SetTrigger("attack");
            IsDoingSomething = true;
            Invoke("DontDo", 1.33f);
        }
        if(ControlFreak2.CF2Input.GetKey(KeyCode.E) && !IsDoingSomething && !IsJumping)
        {
            myAnimator.SetTrigger("pounce");
            IsDoingSomething = true;
            Invoke("DontDo", 1.5f);
        }
        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.Z) && !IsDoingSomething)
        {
            myAnimator.SetTrigger("defend");
            IsDoingSomething = true;
            Invoke("DontDo", 1.33f);
        }
        if(ControlFreak2.CF2Input.GetKeyDown(KeyCode.B) && !IsDoingSomething)
        {
            myAnimator.SetTrigger("web");
            IsDoingSomething = true;
            Invoke("DontDo", 1.33f);
        }
    }
    public void DontDo()
    {
        IsDoingSomething = false;
    }
}