using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour {   
    public Vector3 Aim_Position;
    public float Aim_Speed = 5;
    bool isAiming = false;
    bool Aiming = false;
    
    void Update() {
        
        if(Input.GetButtonDown("Fire2")) isAiming = !isAiming;
        
        if(isAiming==true && Aiming == false) {   
            transform.localPosition = Vector3.Lerp(transform.localPosition, Aim_Position, Time.deltaTime * Aim_Speed); 
            if(transform.localPosition == Aim_Position) Aiming = true;
        } else if(isAiming==true && Aiming == true)
            Debug.Log("Apuntando");
    }
}
