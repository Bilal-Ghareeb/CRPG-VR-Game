using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class temp : MonoBehaviour {
    [SerializeField]
    GameObject target;
    // Start is called before the first frame update
    void Start() {

    }

    private void FixedUpdate() {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * 150));

        //var t = Mathf.Atan2(target.transform.rotation.y - transform.position.x, target.transform.rotation.y - transform.position.z)t;
        //print(t);
        var quat = Quaternion.LookRotation(target.transform.position);
        print(quat);
        print(transform.rotation);
        //print(Vector3.Angle(transform.TransformDirection(Vector3.forward), target.transform.TransformDirection(Vector3.forward)));


    }
}
