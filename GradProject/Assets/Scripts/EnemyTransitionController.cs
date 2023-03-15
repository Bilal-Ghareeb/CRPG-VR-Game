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
            GetComponent<Animator>().SetBool("isPunch",true);
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.CompareTag("Player")) {
            GetComponent<Animator>().SetBool("isPunch",false);
        }
    }
}
