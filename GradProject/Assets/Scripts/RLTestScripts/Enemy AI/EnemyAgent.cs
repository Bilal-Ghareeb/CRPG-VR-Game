using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using static UnityEngine.Random;
using TMPro;
using Oculus.Interaction;
using Oculus.Platform.Models;
using Unity.MLAgents.Policies;
using static UnityEngine.GraphicsBuffer;
using Unity.Mathematics;


public class EnemyAgent : Agent {

    EnemyEnvController envController;

    //serialized fields
    [SerializeField]
    private GameObject targetGO;
    //[SerializeField]
    //EnemyAgent targetAgent;
    //[SerializeField]
    private float speed = 5f;

    //[SerializeField]
    //GameObject ground;
    [SerializeField]
    int teamId;
    private const float turnAmount = 25;

    //reset variables
    new private Rigidbody rigidbody;
    private Vector3 originalTargetPosition;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float prevDistanceToTarget;
    int steps = 0;

    private EnvironmentParameters defaultParams;

    //scoreboard variables
    ScoreBoard scoreBoard;

    //health variables
    [HideInInspector]
    public HealthBar healthBar;
    float targetHealthPoints;

    //animator
    EnemyTransitionController animatorController;

    void Start() {
        animatorController = GetComponent<EnemyTransitionController>();
        //envController = trainingEnv.GetComponent<EnemyEnvController>();
    }
    public override void Initialize() {
        scoreBoard = new ScoreBoard();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalTargetPosition = targetGO.transform.position;
        targetHealthPoints = targetGO.GetComponent<HealthBar>().getHealth();
        healthBar = GetComponent<HealthBar>();
        rigidbody = GetComponent<Rigidbody>();
        defaultParams = Academy.Instance.EnvironmentParameters;
    }

    //---------------------------------------------------------------------

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(targetGO.transform.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(transform.rotation.eulerAngles);
        sensor.AddObservation(healthBar.getHealth());
        sensor.AddObservation(targetHealthPoints);
    }

    //---------------------------------------------------------------------

    public override void Heuristic(in ActionBuffers actionsOut) {
        float[] actions = { Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") };
        //ActionBuffers.FromDiscreteActions(actions);

    }

    //---------------------------------------------------------------------

    public override void OnActionReceived(ActionBuffers actionBuffers) {

        float forwardAmount = 0f;
        if (actionBuffers.DiscreteActions[0] == 1f) {//walk
            forwardAmount = 1f;
            animatorController.StartWalkAnimation();
        }
        else if (actionBuffers.DiscreteActions[0] == 2f) {//attack
            animatorController.AttackAnimation();
            float distanceToTarget = Vector3.Distance(transform.position, targetGO.transform.position);
            if (distanceToTarget > 3f) {
                AddReward(-0.00005f);
            }
        }
        else if (actionBuffers.DiscreteActions[0] == 3f) { //block
            animatorController.BlockAnimation();
            float distanceToTarget = Vector3.Distance(transform.position, targetGO.transform.position);
            if (distanceToTarget > 3f) {
                AddReward(-0.00005f);
            }
        }
        else {
            animatorController.IdleAnimation();
        }

        AddReward(-0.0001f);


        float turnDirection = 0f;

        if (actionBuffers.DiscreteActions[1] == 1f) {
            turnDirection = -1f;
        }
        else if (actionBuffers.DiscreteActions[1] == 2f) {
            turnDirection = 1f;
        }


        if (forwardAmount == 1f) {

        }
        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * speed * Time.fixedDeltaTime);

        transform.Rotate(transform.up * turnDirection * turnAmount * Time.fixedDeltaTime);

    }

    //---------------------------------------------------------------------

    public override void OnEpisodeBegin() {
        healthBar.reset();
        rigidbody.velocity = Vector3.zero;
        steps = 0;
        //scoreBoard.text = "Episodes: " + episodesDone.ToString() + "\tSuccesses: " + successes.ToString() + "\tFails: " + fails.ToString() + "\nOut of Steps: " + outOfSteps.ToString();

        transform.position = originalPosition;
        //penalties = 0;
        transform.rotation = originalRotation;
        prevDistanceToTarget = Vector3.Distance(transform.position, targetGO.transform.position);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Wall") {
                //AddReward(-0.5f); //1
                SetReward(-0.5f);
                scoreBoard.failed();
                //episodesDone++;
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
        targetHealthPoints = targetGO.GetComponent<HealthBar>().getHealth();
        steps++;
        if (targetHealthPoints <= 0f) {
            SetReward(1f);
            scoreBoard.succeeded();
            //successes++;
            //episodesDone++;
            print(gameObject.name + " won");
            EndEpisode();
        }
        else if (healthBar.getHealth() <= 0f) {
            SetReward(-1f);
            EndEpisode();
        }

        if (steps % 75 == 0) {
            float distanceToTarget = Vector3.Distance(transform.position, targetGO.transform.position);
            if (distanceToTarget < 7f) {

            }
            else {
                var reward = (prevDistanceToTarget - distanceToTarget) / 16;
                AddReward(reward);
            }
            prevDistanceToTarget = distanceToTarget;
        }

        if (steps > 17500) {
            //episodesDone++;
            //outOfSteps++;
            scoreBoard.ranOutOfSteps();
            SetReward(-0.5f);
            EndEpisode();
        }

    }

}