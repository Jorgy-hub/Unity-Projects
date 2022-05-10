using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerOnlySword : MonoBehaviour
{
    // Movement Variables and Constants that will make the Movement work.
    public CharacterController Controller;
    public Transform GroundCheck;
    public LayerMask Ground;
    public GameObject DoubleJump;
    public bool Sprinting = false;
    public float Speed = 6f;
    public float MainSpeed = 6f;
    public float SmoothTime = 0.1f;
    public float Gravity = -9.81f;
    public float GroundDistance = 0.2f; 
    public float Jump = 4f;
    private Vector3 Velocity;
    public int Jumps = 0;
    private bool IsGrounded;
    private bool Blocking = false;
    
    // Rotation Variables that will change the rotation of the Character.
    public Transform Camera;
    public Animator PlayerAnimator;
    float turnSmoothVelocity;

    // Combat Variables needed for a combat System.
    public int ClickAmount = 0;
    public bool CanClick = true;

    /**
     * This will just set some inital variables. 
     */
    void Start(){
        DoubleJump.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
    /**
     * Do I srsly need to explain this?
     */
    void Update(){      
        Block();
        PunchAnimations();
        SafeMovement();
        UpdateJump();
    }
    

    void Block(){
        if(Input.GetKey(KeyCode.F)
            && 
            Jumps < 1 
            && 
            IsGrounded
        )
            Blocking = true;
        else
            Blocking = false;
        
        // Block System requires to disable combo system.
        if(Blocking){
            PlayerAnimator.SetBool("Block", true);
            PlayerAnimator.SetInteger("Attack", 0);
            CanClick = true;
            ClickAmount = 0;
        // Disabling Blocking Animations.
        } else 
            PlayerAnimator.SetBool("Block", false);    
    }

    void UpdateJump(){
        if(!PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump") && Jumps >= 1){
            DoubleJump.SetActive(false);
        }
    }

    /**
     * This function will determine all of the conditions for the movement to work just to clean the Update a bit.
     * 1. Block movement on combat system.
     */
    void SafeMovement(){
        if( ClickAmount == 0 
            && 
            !PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Draw")
            &&
            !Blocking
        )
            Movement();
        else {
            PlayerAnimator.SetBool("Run", false);
            PlayerAnimator.SetBool("Move", false); 
        }
    }  

    /**
     * This function Controls all the Movements of the Player.
     * 1. Determines if it's on the ground.
     * 2. Movement in the X & Z axis.
     * 3. Y axis & Gravity.
     */
    void Movement(){
        // Update Ground.
        IsGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, Ground);
        if(IsGrounded && Velocity.y < 0)
            Velocity.y = -2f;

        // X & Z Movement.
        float Horizontal = Input.GetAxisRaw("Horizontal");
        float Vertical = Input.GetAxisRaw("Vertical");
        Vector3 Direction = new Vector3(Horizontal, 0f, Vertical).normalized;
        if(Direction.magnitude >= 0.1f){
            PlayerAnimator.SetBool("Move", true);
            if(Input.GetKey(KeyCode.LeftShift))
                Sprinting = true;
            else 
                Sprinting = false;

            if(Sprinting == true){
                MainSpeed = Speed * 3;
                PlayerAnimator.SetBool("Run", true);
            } else {
                MainSpeed = Speed;
                PlayerAnimator.SetBool("Run", false);
            }
            float TargetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + Camera.eulerAngles.y;
            float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, TargetAngle, ref turnSmoothVelocity, SmoothTime);
            transform.rotation = Quaternion.Euler(0f, Angle, 0f);
            Vector3 MoveDirection = Quaternion.Euler(0f, TargetAngle, 0f) * Vector3.forward;   
            Controller.Move(MoveDirection.normalized * MainSpeed * Time.deltaTime);
        } else {
            PlayerAnimator.SetBool("Move", false); 
            PlayerAnimator.SetBool("Run", false);
        }
        
        // Y Movement.
        if(Input.GetButtonDown("Jump") && IsGrounded && Jumps <= 2){
            Velocity.y = Mathf.Sqrt(Jump * -2.0f * Gravity);
            PlayerAnimator.SetTrigger("Jump");     
            Jumps++;

            if(Jumps == 1)
                DoubleJump.SetActive(true);
            else if(Jumps > 1)
                Jumps = 0;
        }

        // Updates to Controller. 
        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
    }

    /**
     * This function Determines when there's a Punch.
     * 1. Just checks if the click is available, if it is starts the combo system. 
     */
    void PunchAnimations(){
        // Start and Update Combo System. 
        if(Input.GetMouseButtonDown(0)){
            if(CanClick && IsGrounded && Jumps < 1) 
                ClickAmount++;
            if(ClickAmount == 1)
                PlayerAnimator.SetInteger("Attack", 1);  
            else if(ClickAmount > 1)
                VerifyCombo();       
        }
    }

    /**
     * This function is the second part of the combat system that will execute the animations.
     * 1. Checks if Animation is doing the first Hit animation and determine if it should stop or continue.
     * 2. Checks if Animation is doing the second Hit animation and determine if it should stop or continue.
     * 3. Checks if Animation is doing the third Hit animation and determine if it should stop or continue.
     */
    public void VerifyCombo(){
        CanClick = false;
        // Cancel Combo if the User is Blocking.
        if(Blocking == true){
            PlayerAnimator.SetInteger("Attack", 0);
            CanClick = true;
            ClickAmount = 0;
        // Animations Check to see in what hit is working.
        } else {
            if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && ClickAmount == 1){
                PlayerAnimator.SetInteger("Attack", 0);
                CanClick = true;
                ClickAmount = 0;
            } else if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit1") && ClickAmount >= 2){
                PlayerAnimator.SetInteger("Attack", 2);
                CanClick = true;
            } else if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && ClickAmount == 2){
                PlayerAnimator.SetInteger("Attack", 0);
                CanClick = true;
                ClickAmount = 0;
            } else if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit2") && ClickAmount >= 3){
                PlayerAnimator.SetInteger("Attack", 3);
                CanClick = true;
            } else if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit3") && ClickAmount == 3){
                PlayerAnimator.SetInteger("Attack", 0);
                CanClick = true;
                ClickAmount = 0;
            } else if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit3") && ClickAmount >= 4){
                PlayerAnimator.SetInteger("Attack", 4);
                CanClick = true;
            } else if(PlayerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Hit4")){
                PlayerAnimator.SetInteger("Attack", 0);
                CanClick = true;
                ClickAmount = 0;
            }
        }
    }
}
