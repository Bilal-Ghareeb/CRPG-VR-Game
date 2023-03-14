using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class transitionController : MonoBehaviour {
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            GetComponent<Animator>().SetBool("clostToPlayer", true);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.CompareTag("Player")) {
            GetComponent<Animator>().SetBool("clostToPlayer", false);
        }
    }
}
