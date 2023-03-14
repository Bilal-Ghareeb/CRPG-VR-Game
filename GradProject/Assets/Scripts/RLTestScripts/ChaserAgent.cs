//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
//using Unity.MLAgents.Integrations.Match3;
//using UnityEngine.ProBuilder.Shapes;
//using System.Threading;
using static UnityEngine.Random;
using System;
using Unity.Mathematics;
using Oculus.Interaction;
using TMPro;

public class ChaserAgent : Agent {
    [SerializeField]
    private GameObject target;
    new private Rigidbody rigidbody;
    private Vector3 originalTargetPosition;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    [SerializeField]
    private float speed = 35;
    private const float turnAmount = 25;
    GameObject cube;
    private EnvironmentParameters defaultParams;
    private float groundEdge;
    private float negativeX;
    private float positiveX;
    private float negativeZ;
    private float positiveZ;
    private float prevDistanceToTarget;
    protected float Timer = 0;
    static int episodesDone = 0;
    static int fails = 0;
    static int successes = 0;
    int steps = 0;
    [SerializeField]
    TextMeshProUGUI scoreBoard;
    public float DelayAmount = 10f;
    public override void Initialize() {

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalTargetPosition = target.transform.position;

        GameObject parent = gameObject.transform.parent.gameObject;

        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren) {

            if (child.gameObject.tag == "Ground") {
                cube = child.gameObject;
            }
        }

        groundEdge = cube.GetComponent<Renderer>().bounds.size.x / 2;
        negativeX = cube.transform.position.x - groundEdge;
        positiveX = cube.transform.position.x + groundEdge;
        negativeZ = cube.transform.position.z - groundEdge;
        positiveZ = cube.transform.position.z + groundEdge;
        rigidbody = GetComponent<Rigidbody>();
        defaultParams = Academy.Instance.EnvironmentParameters;
    }

    //---------------------------------------------------------------------

    public Vector3 GetRandomSpawnPos() {
        var foundNewSpawnLocation = false;

        var randomPosX = Range(negativeX + 30,
                positiveX - 30);
        var randomPosZ = Range(negativeZ + 30,
           positiveZ - 30);
        var randomSpawnPos = new Vector3(randomPosX, originalTargetPosition.y, randomPosZ);

        while (foundNewSpawnLocation == false) {

            Collider[] testCollider = Physics.OverlapBox(randomSpawnPos, new Vector3(6f, 15f, 6f));
            if (testCollider.Length == 1) {
                break;
            }
            randomPosX = Range(negativeX + 30,
                positiveX - 30);
            randomPosZ = Range(negativeZ + 30,
               positiveZ - 30);

            randomSpawnPos = new Vector3(randomPosX, originalTargetPosition.y, randomPosZ);

        }

        return randomSpawnPos;
    }

    //---------------------------------------------------------------------

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(transform.localPosition);
    }

    //---------------------------------------------------------------------

    public override void Heuristic(in ActionBuffers actionsOut) {
        float[] actions = { Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") };
        ActionBuffers.FromDiscreteActions(actions);

    }

    //---------------------------------------------------------------------

    public override void OnActionReceived(ActionBuffers actionBuffers) {

        float forwardAmount = 0f;
        if (actionBuffers.DiscreteActions[0] == 1f) {
            forwardAmount = 1f;

        }
        if (steps > 1000) {
            AddReward(-0.0005f);
        }

        float turnDirection = 0f;

        if (actionBuffers.DiscreteActions[1] == 1f) {
            turnDirection = -1f;
        }
        else if (actionBuffers.DiscreteActions[1] == 2f) {
            turnDirection = 1f;
        }

        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * speed * Time.fixedDeltaTime);

        transform.Rotate(transform.up * turnDirection * turnAmount * Time.fixedDeltaTime);

    }

    //---------------------------------------------------------------------

    public override void OnEpisodeBegin() {
        rigidbody.velocity = Vector3.zero;
        steps = 0;
        scoreBoard.text = "Episodes: " + episodesDone.ToString() + "\tSuccesses: " + successes.ToString() + "\tFails: " + fails.ToString();
        target.transform.position = GetRandomSpawnPos();
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        prevDistanceToTarget = Vector3.Distance(transform.position, target.transform.position);
    }

    //---------------------------------------------------------------------

    private void OnCollisionEnter(Collision collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Wall") {
                AddReward(-1f);
                fails++;
                episodesDone++;
                EndEpisode();

            }
            else if (collision.gameObject.tag == "Player") {
                Debug.Log("gotcha");
                AddReward(1f);
                successes++;
                episodesDone++;
                EndEpisode();
            }
        }
    }

    //---------------------------------------------------------------------

    private void OnCollisionStay(Collision collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Obstacle") {
                AddReward(-0.001f);
            }
        }
    }

    //---------------------------------------------------------------------

    private void FixedUpdate() {
        steps++;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= 10f) {
            SetReward(1f);
            successes++;
            episodesDone++;
            EndEpisode();
        }

        if (steps % 75 == 0) {
            var reward = (prevDistanceToTarget - distanceToTarget) / 75f;
            AddReward(reward);
            prevDistanceToTarget = distanceToTarget;
        }

        if (steps > 7500) {
            episodesDone++;
            EndEpisode();
        }

    }

}
