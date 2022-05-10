using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    
    public float health = 100.0f;
    public Movement Movement;
    public GameObject DeadCamera;
    public GameObject MainCamera;
    public Animator Animator;
    
    void Start() {
    // Change Camera on dead.
        DeadCamera.SetActive(false);
    }

    void Update() {
        if(health <= 0) {
            // Disable motion.
            Movement.enabled = false;
            DeadCamera.SetActive(true);
            MainCamera.SetActive(false);
            // Trigger Dead Animation
            Animator.SetTrigger("Dead");
        }
    }
}
