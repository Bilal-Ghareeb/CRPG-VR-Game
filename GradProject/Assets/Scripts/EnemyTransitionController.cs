using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransitionController : MonoBehaviour {
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    //void OnTriggerEnter(Collider other) {
    //    if (other.CompareTag("Player")) {
    //        OnAttackTrigger();
    //        GetComponent<Animator>().SetBool("isAttack",true);
    //    }
    //}

    //void OnTriggerExit (Collider other) {
    //    if (other.CompareTag("Player")) {
    //        GetComponent<Animator>().SetBool("isAttack",false);
    //    }
    //}

    //void OnAttackTrigger(){
    //    float blendValue = Random.Range(0f, 1f);
    //    Debug.Log("Done "+blendValue);
    //    GetComponent<Animator>().SetFloat("AttackBlend",blendValue);
    //}
    public void AttackAnimation() {
        animator.SetTrigger("attack");
        //GetComponent<Animator>().ResetTrigger("attack");
    }

    public void BlockAnimation() {
        animator.SetTrigger("block");
        //GetComponent<Animator>().ResetTrigger("block");

    }

    public void StartWalkAnimation() {
        animator.SetBool("walk", true);
    }

    public void StopWalkAnimation() {
        animator.SetBool("walk", false);
    }

    public void IdleAnimation() {
        StopWalkAnimation();
        animator.ResetTrigger("block");
        animator.ResetTrigger("attack");
    }

    //void OnTriggerEnter(Collider other) {
    //    if (other.CompareTag("Player")) {
    //        GetComponent<Animator>().SetBool("isPunch", true);
    //    }
    //}

    //void OnTriggerExit(Collider other) {
    //    if (other.CompareTag("Player")) {
    //        GetComponent<Animator>().SetBool("isPunch", false);
    //    }
    //}
}
