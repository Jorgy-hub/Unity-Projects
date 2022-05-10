using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public CharacterController Controller;
    public float Speed = 12f;
    public float Crouch_Speed = 7f;
    public float Camera_Crouch = 0.1f;
    public float Gravity = -9.81f;
    public float Jump = 3f;
    public float Ground_Distance = 0.4f;
    public LayerMask Ground_Mask;
    public Transform Ground_Check;
    public Transform Camera;
    public Animator Animator;
    public Animator GunAnimator;

    float Initial_Speed;
    Vector3 Camera_Initial_Position;
    Vector3 Velocity;
    bool Is_Grounded;
    bool Is_Crouching = false;

    void Start()  {
        Camera_Initial_Position = Camera.localPosition;
        Initial_Speed = Speed;
    }
    
    void Update() {
        // Gravity
        Is_Grounded = Physics.CheckSphere(Ground_Check.position, Ground_Distance, Ground_Mask); 
        
        if(Is_Grounded && Velocity.y < 0) {
            Velocity.y = -2f;
        } 

        // Movement.
        if(Is_Crouching == true) {
            Vector3 CrouchPosition = Camera_Initial_Position;
            CrouchPosition.y = Camera_Crouch;
            Camera.localPosition = CrouchPosition;
            Speed = Crouch_Speed;
            Animator.SetBool("Walking", false);
            if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
                Animator.SetBool("CrouchWalking", true);
                GunAnimator.SetBool("Walk", true);
            }
            if(Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical")) {
                GunAnimator.SetBool("Walk", false);
                Animator.SetBool("CrouchWalking", false);
            } 
        } else {
            Camera.localPosition = Camera_Initial_Position;
            Speed = Initial_Speed;
            Animator.SetBool("CrouchWalking", false);
            if(Input.GetButton("Horizontal") || Input.GetButton("Vertical")) {
                Animator.SetBool("Walking", true);
                GunAnimator.SetBool("Walk", true);
            }
            if(Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical")) {
                Animator.SetBool("Walking", false);
                GunAnimator.SetBool("Walk", false);
            }
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 Movement = transform.right * x + transform.forward * z;
        Controller.Move(Movement * Speed * Time.deltaTime);

        // Jumping
        if(Input.GetButtonDown("Jump") && Is_Grounded) {   
            Animator.SetTrigger("Jump");
            Velocity.y = Jump;
        }

        // Crouch
        if(Input.GetButtonDown("Crouch")) {
            Is_Crouching = !Is_Crouching;
            if(Is_Crouching == false) {
                Animator.SetBool("CrouchWalking", false);
                Animator.SetBool("Crouch", false);
            }
            if(Is_Crouching == true) {
                Animator.SetBool("Walking", false);
                Animator.SetBool("Crouch", true);
            }
        }  

        // Move Component.
        Velocity.y += Gravity * Time.deltaTime;
        Controller.Move(Velocity * Time.deltaTime);
    }
}
