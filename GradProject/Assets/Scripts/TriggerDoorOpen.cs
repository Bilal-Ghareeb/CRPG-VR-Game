using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoorOpen : MonoBehaviour
{
    [SerializeField] private Animator MyDoor;

    public void OpenDoor()
    {
        MyDoor.Play("door_1_open", 0, 0.0f);
        MyDoor.SetBool("Opened", true);
    }

    public void CloseDoor()
    {
        MyDoor.SetBool("Opened", false);
    }
}
