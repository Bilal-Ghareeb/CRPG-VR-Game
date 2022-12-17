using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Oculus.Interaction.HandGrab;

public class BowStringController : MonoBehaviour
{
    [SerializeField]
    private BowString bowStringRenderer;

    private HandGrabInteractable interactable;

    [SerializeField]
    private Transform midPointGrabObject,midPointParent;

    private float bowStringStretchLimit = 0.24f;

    private void Awake()
    {
        interactable = midPointGrabObject.GetComponent<HandGrabInteractable>();
    }

    // private void LateUpdate() {
        
    // }

    // private void ResetBowString()
    // {
    //     midPointGrabObject.localPosition = Vector3.zero;
    //     midPointParent.localPosition = Vector3.zero;
    //     bowStringRenderer.CreateString(midPointParent.transform.position);
    // }

    private void Update()
    {
        if (interactable != null)
            {
                //Debug.Log("INTERACtables " + interactable.Interactors.Count);
                //convert bow string mid point position to the local space of the MidPoint
                Vector3 midPointLocalSpace = midPointParent.InverseTransformPoint(midPointGrabObject.position); // localPosition
                //Debug.Log("MIDPOINTLOCALSPACE " + midPointLocalSpace.x);
                //get the offset
                float midPointLocalXAbs = Mathf.Abs(midPointLocalSpace.x);
                //Debug.Log("X ABS  " + midPointLocalXAbs);

                HandleStringPushedBackToStart(midPointLocalSpace);

                HandleStringPulledBackTolimit(midPointLocalXAbs, midPointLocalSpace);

                HandlePullingString(midPointLocalXAbs, midPointLocalSpace);

                if(midPointGrabObject.localPosition == Vector3.zero){
                    bowStringRenderer.CreateString(null);
                }
                else{
                bowStringRenderer.CreateString(midPointGrabObject.transform.position);
                }
            }
    }

    private void HandlePullingString(float midPointLocalXAbs, Vector3 midPointLocalSpace)
    {
        //what happens when we are between point 0 and the string pull limit
        if (midPointLocalSpace.x < 0.2 && midPointLocalXAbs < bowStringStretchLimit)
        {
            midPointGrabObject.localPosition = new Vector3(midPointLocalSpace.x, 0, 0);
        }
    }

    private void HandleStringPulledBackTolimit(float midPointLocalXAbs, Vector3 midPointLocalSpace)
    {
        //We specify max pulling limit for the string. We don't allow the string to go any farther than "bowStringStretchLimit"
        if (midPointLocalSpace.x < 0.2 && midPointLocalXAbs > bowStringStretchLimit)
        {
            //Vector3 direction = midPointParent.TransformDirection(new Vector3(0, 0, midPointLocalSpace.z));
            midPointGrabObject.localPosition = new Vector3(-bowStringStretchLimit, 0, 0);
        }
    }

    private void HandleStringPushedBackToStart(Vector3 midPointLocalSpace)
    {
        if (midPointLocalSpace.x > bowStringStretchLimit && interactable.Interactors.Count == 0)
        {
        midPointParent.localPosition = Vector3.zero;
        midPointGrabObject.localPosition = Vector3.zero;
        }
    }
}