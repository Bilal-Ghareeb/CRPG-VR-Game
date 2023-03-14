using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder;
using UnityEngine.ProBuilder.Shapes;
using static UnityEngine.Random;

public class Spawner : MonoBehaviour {
    private const float groundEdge = 143;
    private float YBound;
    private float negativeX;
    private float positiveX;
    private float negativeZ;
    private float positiveZ;
    GameObject cube;
    private void Start() {


        GameObject parent = gameObject.transform.parent.gameObject;

        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren) {
            ////print(child.gameObject.tag);
            //if (child.gameObject.tag == "Player") {
            //    //target = child.gameObject;

            //}
            if (child.gameObject.tag == "Ground") {
                cube = child.gameObject;

            }
        }
        print(cube.transform.position);
        negativeX = cube.transform.position.x - groundEdge;
        positiveX = cube.transform.position.x + groundEdge;
        negativeZ = cube.transform.position.z - groundEdge;
        positiveZ = cube.transform.position.z + groundEdge;
        YBound = cube.transform.position.y + 8;
        gameObject.transform.position = spawnLocation();
    }
    Vector3 spawnLocation() {
        var foundNewSpawnLocation = false;

        var randomSpawnPos = Vector3.zero;

        while (foundNewSpawnLocation == false) {
            var randomPosX = Range(negativeX + 3,
                positiveX - 3);
            var randomPosZ = Range(negativeZ + 3,
               positiveZ - 3);
            print(randomPosX);
            print(randomPosZ);

            randomSpawnPos = new Vector3(randomPosX, YBound + 7, randomPosZ);
            print(randomSpawnPos);
            Collider[] testCollider = Physics.OverlapBox(randomSpawnPos, new Vector3(6f, 15f, 6f));
            print(testCollider.Length);
            //break;
            if (testCollider.Length == 1) {
                foundNewSpawnLocation = true;

            }

        }
        return randomSpawnPos;
    }
}
