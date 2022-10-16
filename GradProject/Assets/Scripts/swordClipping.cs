using Oculus.Interaction;
using Oculus.Interaction.Grab;
using Oculus.Interaction.HandGrab;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class swordClipping : MonoBehaviour {
    private bool isGrabbed;
    //private bool collided;
    //private Vector3 colPos;

    private void Awake() {

        //interactor = GetComponent<XRDirectInteractor>();
        isGrabbed = false;
        //collided = false;
        //colPos = transform.position;

    }

    private void FixedUpdate() {
        int num_of_interactors = 0;

        HandGrabInteractable[] hands = gameObject.GetComponentsInChildren<HandGrabInteractable>();
        foreach (HandGrabInteractable hand in hands) {
            num_of_interactors += hand.Interactors.Count;
        }
        //Debug.Log("INTERACTORS " + num_of_interactors);
        //Debug.Log("ISKINEMATIC OR NAH? " + gameObject.GetComponent<Rigidbody>().isKinematic);
        if (num_of_interactors > 0) {
            isGrabbed = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            /*if (collided) {
                transform.position = colPos;
            }*/
        }
        else {
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            isGrabbed = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        GameObject collidedObject = other.gameObject;
        //colPos = transform.position;
        if (collidedObject.tag == "Finish") { //this is a test for colliding with the cube
            //Debug.Log("cooooooooooooooooooooooooooooooooooooolCUBE");
            if (isGrabbed) {
                //transform.position = colPos;
                //collided = true;
            }
        }

        //Debug.Log("TRIGGGGGGGGGGGGGGGGGGGGGGG");
        //if (collidedObject.tag == "Player") {
        //    Debug.Log("GRABBED");
        //    isGrabbed = true;
        //}
        //else {
        //    isGrabbed = false;
        //}
    }

    private void OnTriggerExit(Collider other) {
        //GameObject collidedObject = other.gameObject;
        //colPos = transform.position;

        //if (collidedObject.tag == null) {
        //    Debug.Log("NOTGRABBED");
        //    isGrabbed = false;
        //}

    }
    /*
    private void OnCollisionEnter(Collision collision) {
        GameObject collidedObject = collision.gameObject;
        colPos = transform.position;
        if (collidedObject != null) {
            //Debug.Log("coooooooooooooooooooooooooooooooooooool");
            if (isGrabbed) {
                transform.position = colPos;
            }
        }

    }*/

    private void OnDestroy() {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

}
