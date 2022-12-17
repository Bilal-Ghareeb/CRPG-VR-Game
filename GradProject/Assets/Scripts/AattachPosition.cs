using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AattachPosition : MonoBehaviour
{
    [Header("Object Reference")]
    public GameObject player;

    void LateUpdate()
    {
        transform.position = player.transform.position+ new Vector3(0f, -0.75f , 0.3f);
        transform.rotation = player.transform.rotation * Quaternion.Euler(180 , 0 , 0);
        //transform.Translate(new Vector3(0f, -0.04f, 0f), Space.Self);
    }
}
