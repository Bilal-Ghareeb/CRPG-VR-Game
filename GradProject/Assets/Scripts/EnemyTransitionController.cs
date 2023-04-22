using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTransitionController : MonoBehaviour
{
    Animator animator;
    
    void Start () {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            OnAttackTrigger();
            GetComponent<Animator>().SetBool("isAttack",true);
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            GetComponent<Animator>().SetBool("isAttack",false);
        }
    }

    void OnAttackTrigger(){
        float blendValue = Random.Range(0f, 1f);
        Debug.Log("Done "+blendValue);
        GetComponent<Animator>().SetFloat("AttackBlend",blendValue);
    }
}
