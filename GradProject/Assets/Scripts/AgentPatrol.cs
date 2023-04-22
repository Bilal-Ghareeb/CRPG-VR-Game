using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentPatrol : MonoBehaviour
{
    [Header("First point Reference")]
    public GameObject firstPoint;
    [Header("Second point Reference")]
    public GameObject secondPoint;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Transform>().LookAt(secondPoint.transform);
        GetComponent<Rigidbody>().velocity = transform.forward * 4;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Patrol Point 1"){
            GetComponent<Transform>().LookAt(secondPoint.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * 4;
        }
        else if(other.name == "Patrol Point 2"){
            GetComponent<Transform>().LookAt(firstPoint.transform);
            GetComponent<Rigidbody>().velocity = transform.forward * 4;
        }
    }

}
