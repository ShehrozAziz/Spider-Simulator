using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpidyController : MonoBehaviour
{
    public static SpidyController instance;

    [Header("Input System")]
    public InputActionAsset inputActionAsset; // Drag your .inputactions asset here
    private InputActionMap playerMap;
    private InputAction moveAction, jumpAction, attackAction, pounceAction, defendAction, webAction, runAction;

    public Animator myAnimator;
    CharacterController _Spidycontroller;

    public float MovingSpeed = 5f;
    public float RotationMovingSpeed = 2f;
    public float JumpForce = 8f;
    public float Jumpvelocity = -0.5f;
    public float GroundedAdjustRadius = 0.5f;
    public float GroundedAdjustHeight = 0.1f;

    private bool isGrounded;
    private bool IsJumping;
    private bool IsDoingSomething;
    private float verticalVelocity = 0f;

    public AudioSource attackbutnothit;
    public AudioSource Attack;
    public AudioSource Damage;
    public AudioSource Walk;
    public AudioSource Jump;
    public bool HittingFlesh;

    private Vector2 movementInput;
    private bool jumpPressed, attackPressed, pouncePressed, defendPressed, webPressed, runPressed;

    private void OnEnable()
    {
        if (inputActionAsset != null)
        {
            playerMap = inputActionAsset.FindActionMap("Player");

            moveAction = playerMap.FindAction("Move");
            jumpAction = playerMap.FindAction("Jump");
            attackAction = playerMap.FindAction("Attack");
            pounceAction = playerMap.FindAction("Pounce");
            defendAction = playerMap.FindAction("Defend");
            webAction = playerMap.FindAction("Web");
            runAction = playerMap.FindAction("Run");

            moveAction.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
            moveAction.canceled += ctx => movementInput = Vector2.zero;
            jumpAction.performed += ctx => jumpPressed = true;
            attackAction.performed += ctx => attackPressed = true;
            pounceAction.performed += ctx => pouncePressed = true;
            defendAction.performed += ctx => defendPressed = true;
            webAction.performed += ctx => webPressed = true;
            runAction.performed += ctx => runPressed = true;
            runAction.canceled += ctx => runPressed = false;

            playerMap.Enable();
        }
    }

    private void OnDisable()
    {
        if (playerMap != null)
        {
            playerMap.Disable();
        }
    }

    private void Start()
    {
        instance = this;
        IsJumping = false;
        IsDoingSomething = false;
        _Spidycontroller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float HorizontalInput = ControlFreak2.CF2Input.GetAxis("Horizontal");
        float VerticalInput = ControlFreak2.CF2Input.GetAxis("Vertical");

        if (HorizontalInput == 0 && VerticalInput == 0 && movementInput != Vector2.zero)
        {
            HorizontalInput = movementInput.x;
            VerticalInput = movementInput.y;
        }

        Vector3 move = new Vector3(0f, 0f, VerticalInput);
        float AddedMovingSpeed = MovingSpeed;

        if ((HorizontalInput != 0 || VerticalInput != 0) && !IsDoingSomething)
        {
            PlayWalk();
            transform.Rotate(0, HorizontalInput * RotationMovingSpeed, 0);

            if ((ControlFreak2.CF2Input.GetKey(KeyCode.LeftShift) || runPressed) && _Spidycontroller.isGrounded)
            {
                myAnimator.SetInteger("movement", 2);
                AddedMovingSpeed = MovingSpeed * 2;
            }
            else
            {
                if (!IsJumping)
                    myAnimator.SetInteger("movement", 1);
                else
                {
                    myAnimator.SetInteger("movement", 0);
                    AddedMovingSpeed = MovingSpeed;
                }
            }
        }
        else
        {
            myAnimator.SetInteger("movement", 0);
            AddedMovingSpeed = 0f;
        }

        if (_Spidycontroller.isGrounded)
        {
            IsJumping = false;
            myAnimator.SetBool("jump", false);

            if (ControlFreak2.CF2Input.GetKeyDown(KeyCode.Space) || jumpPressed)
            {
                jumpPressed = false;
                Jump.Play();
                verticalVelocity = JumpForce;
                IsJumping = true;
                myAnimator.SetBool("jump", true);
            }
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        move = transform.TransformDirection(move);
        _Spidycontroller.Move((move * AddedMovingSpeed + new Vector3(0f, verticalVelocity, 0f)) * Time.deltaTime);

        if ((ControlFreak2.CF2Input.GetKeyDown(KeyCode.Q) || attackPressed) && !IsDoingSomething)
        {
            attackPressed = false;
            if (!HittingFlesh)
                attackbutnothit.Play();
            else
                Attack.Play();

            myAnimator.SetTrigger("attack");
            IsDoingSomething = true;
            SpidyScript.instance.Attack();
            Invoke("DontDo", 1.33f);
        }

        if ((ControlFreak2.CF2Input.GetKey(KeyCode.E) || pouncePressed) && !IsDoingSomething && !IsJumping)
        {
            pouncePressed = false;
            myAnimator.SetTrigger("pounce");
            IsDoingSomething = true;
            Invoke("DontDo", 1.5f);
        }

        if ((ControlFreak2.CF2Input.GetKeyDown(KeyCode.Z) || defendPressed) && !IsDoingSomething)
        {
            defendPressed = false;
            myAnimator.SetTrigger("defend");
            IsDoingSomething = true;
            Invoke("DontDo", 1.33f);
        }

        if ((ControlFreak2.CF2Input.GetKeyDown(KeyCode.B) || webPressed) && !IsDoingSomething)
        {
            webPressed = false;
            myAnimator.SetTrigger("web");
            IsDoingSomething = true;
            SpidyScript.instance.Web();
            Invoke("DontDo", 1.33f);
        }
    }

    public void PlayWalk()
    {
        Walk.Play();
    }

    public void TakeDamage()
    {
        Damage.Play();
        myAnimator.SetTrigger("damage");
        IsDoingSomething = true;
        SpidyScript.instance.BloodSplash();
        Invoke("DontDo", 0.8f);
    }

    public void DontDo()
    {
        IsDoingSomething = false;
    }

    public void IdleState()
    {
        myAnimator.SetInteger("movement", 0);
    }
}
