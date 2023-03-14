using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Rays : MonoBehaviour {
    public Ray[] rays;
    private Vector3[] rayVectors;
    private float rayDistance = 200;

    // Start is called before the first frame update
    void Start() {
        rayVectors = new Vector3[] { vectorFromAngle(10f), vectorFromAngle(10f * 2f), vectorFromAngle(-10f), vectorFromAngle(-10f * 2), vectorFromAngle(-10f * 3), vectorFromAngle(10f * 3), vectorFromAngle(10f * 4), vectorFromAngle(-10f * 4), vectorFromAngle(10f * 5), vectorFromAngle(-10f * 5), vectorFromAngle(10f * 6), vectorFromAngle(-10f * 6), vectorFromAngle(10f * 7), vectorFromAngle(-10f * 7) };

        //rayVectors = new Vector3[] { vectorFromAngle(15.5f), vectorFromAngle(15.5f * 2f), vectorFromAngle(-15.5f), vectorFromAngle(-15.5f * 2), vectorFromAngle(-15.5f * 3), vectorFromAngle(15.5f * 3), vectorFromAngle(15.5f * 4), vectorFromAngle(-15.5f * 4), };
        //rays = new Ray[]{ new Ray(transform.position, transform.TransformDirection(Vector3.forward * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[0] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[1] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[2] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[3] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[4] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[5] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[6] * rayDistance)),
        //    new Ray(transform.position, transform.TransformDirection(rayVectors[7] * rayDistance))
        //};

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * rayDistance));
        for (int i = 0; i < rayVectors.Length; i++) {
            Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[i] * rayDistance));
        }
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[1] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[2] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[3] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[4] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[5] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[6] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[7] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[8] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[9] * rayDistance));
        //Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[10] * rayDistance));
    }
    private Vector3 vectorFromAngle(float degree) {
        //degree = 70. 0f;
        Vector3 noAngle = Vector3.forward;
        Quaternion spreadAngle = Quaternion.AngleAxis(degree, new Vector3(0, 1, 0));
        Vector3 newVector = spreadAngle * noAngle;
        return newVector;
    }

    //behaviour param
    //chaser agent
    //
    // Update is called once per frame
    void Update() {

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward * rayDistance));
        for (int i = 0; i < rayVectors.Length; i++) {
            Debug.DrawRay(transform.position, transform.TransformDirection(rayVectors[i] * rayDistance));
        }
    }

}
