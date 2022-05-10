using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour {
    public NavMeshAgent Mob;
    public GameObject Player;
    public GameObject Spawn;
    public Animator Animator;
    public float Mob_Run = 4.0f;
    public float Speed = 1f;
    public Health Health;
    public float Damage = 20;
    bool Colliding = false;

    void Start() {
       Mob = GetComponent<NavMeshAgent>(); 
    }

    void Update() {
        float Distance = Vector3.Distance(transform.localPosition, Player.transform.localPosition);

        if(Distance < Mob_Run) {
            Vector3 DirToPlayer = transform.localPosition - Player.transform.localPosition;
            Vector3 NewPos = transform.localPosition - DirToPlayer;  
            if(Colliding == true) {
                Animator.SetBool("Run", true);
                Animator.SetBool("Attack", true);
                Mob.SetDestination(transform.localPosition);
            } else {
                Animator.SetBool("Attack", false);
                Animator.SetBool("Run", true);
                Mob.SetDestination(NewPos);
            }
        } else {
            Mob.SetDestination(transform.localPosition);
            Animator.SetBool("Run", false);
        } 
        if(Health.health <= 0) Mob.SetDestination(Spawn.transform.localPosition);    
    }

    void OnTriggerEnter(Collider col) {
        if(col.transform.name == Player.transform.name) {
            Colliding = true;
            Health.health = Health.health - Damage;
        }
    }
    void OnTriggerExit(Collider col) {
        if(col.transform.name == Player.transform.name) Colliding = false; 
    }
}
