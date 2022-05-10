using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {   
    // Aiming.
    float Sway_Speed;
    float Sway_Smooth;
    public bool isAiming = false;
    public bool Aiming = false;
    public float Aim_Speed;
    public Sway Sway_Script;
    public Animator Animator;
    public Vector3 Aim_Position;
    public GameObject Crosshair;
    private Vector3 Initial_Position; 

    // Shoot
    public float Damage = 10f;
    public float Range = 100f;
    public float Mag = 30;
    public float totalMag = 180;
    public Camera Camera;

    void Start() {
        Sway_Speed = Sway_Script.Amount;
        Sway_Smooth = Sway_Script.Smooth_Amount;
        Initial_Position = transform.localPosition;
        Crosshair.SetActive(true);
    }

    void Update() {
        Aim();
 
       // Shoot Button.
        if(Input.GetButtonDown("Fire1"))
        {
            Animator.SetTrigger("Shoot");
            Shoot();
        }

        /// Reload Button.
        if(Input.GetButtonDown("Reload"))
        {
            Reload();
        }
    }

    /**
    Reloading
    - Make a complex system of Bullet Reload
    */
    void Reload() {
        Animator.SetTrigger("Reload");
        /**
        - Disable Aiming while Reloading.
        - Reset Magazine.
        - Re-Enable after finishing Animation.
        */
        Mag = 30;
    }

    // Shoot Main Function.
    void Shoot() {
        RaycastHit hit;
        if (Physics.Raycast(Camera.transform.localPosition, Camera.transform.forward, out hit, Range)) {
            Debug.Log(hit.transform.name);   
        }
    }

    // Aim Main Function.
    void Aim() {
        if(Input.GetButtonDown("Fire2")) isAiming = !isAiming;   
        if(isAiming == true && Aiming == false) {
            Sway_Script.enabled = false;
            Sway_Script.Initial_Position = Aim_Position;
            Sway_Script.Amount = Sway_Speed / 3;
            Sway_Script.Smooth_Amount =  Sway_Smooth / 3;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Aim_Position, Time.deltaTime * Aim_Speed);    
            Crosshair.SetActive(false);
            StartCoroutine(EnableSway_1());
        }
        else if(isAiming == false && Aiming == true) {
            Sway_Script.enabled = false;
            Sway_Script.Initial_Position = Initial_Position;
            Sway_Script.Amount = Sway_Speed;
            Sway_Script.Smooth_Amount = Sway_Smooth;
            transform.localPosition = Vector3.Lerp(transform.localPosition, Initial_Position, Time.deltaTime * Aim_Speed);
            Crosshair.SetActive(true);
            StartCoroutine(EnableSway_2());
        }
    }

    // Enable Sway after disabling
    IEnumerator EnableSway_1() {
        yield return new WaitForSeconds(0.4f);
        Aiming = true;
        Sway_Script.enabled = true;
    }

    // Enable Sway after disabling
    IEnumerator EnableSway_2() {
        yield return new WaitForSeconds(0.4f);
        Aiming = false;
        Sway_Script.enabled = true;
    }
}
