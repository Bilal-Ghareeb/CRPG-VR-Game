using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RightHandButtonScript : MonoBehaviour
{
    [Header("Right Hand Reference")]
    public GameObject Rhand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        transform.position = Rhand.transform.position;
        transform.rotation = Rhand.transform.rotation;
        transform.Translate(new Vector3(-0.05f, 0.01f, 0f), Space.Self);
    }
}
