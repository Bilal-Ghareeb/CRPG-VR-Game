using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomHand : MonoBehaviour
{
    
    public FingerPinch OnIndexPinch = new FingerPinch();
    public FingerPinch OnMiddlePinch = new FingerPinch();

    public float timeBetweenTP = 0.3333f;  

    private float timestamp;

    public OVRHand Hand { get; private set; } = null;

    private void Awake()
    {
        Hand = GetComponent<OVRHand>();
    }

    private void Update()
    {
        if (Hand.IsSystemGestureInProgress)
        {
            return;
        }

        if (Hand.GetFingerIsPinching(OVRHand.HandFinger.Middle))
        {
            OnMiddlePinch.Invoke(this);
        }   

        if (Time.time >= timestamp && Hand.GetFingerIsPinching(OVRHand.HandFinger.Index))
        {
            OnIndexPinch.Invoke(this);
            timestamp = Time.time + timeBetweenTP;
        }
    }

    
    [Serializable] public class FingerPinch : UnityEvent<CustomHand> { }
}
