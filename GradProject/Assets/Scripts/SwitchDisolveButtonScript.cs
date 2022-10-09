using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchDisolveButtonScript : MonoBehaviour
{
    public GameObject button;
    public UnityEvent onPress;
    public UnityEvent onRelease;
    public GameObject presser;
    public bool isPressed;

    // Start is called before the first frame update
    void Start()
    {
        isPressed = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isPressed)
        {
            button.transform.localPosition = new Vector3(0f, 0.003f, 0f);
            presser = other.gameObject;
            onPress.Invoke();
            isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == presser)
        {
            button.transform.localPosition = new Vector3(0f, 0.015f, 0f);
            onRelease.Invoke();
            isPressed = false;
        }
    }
}