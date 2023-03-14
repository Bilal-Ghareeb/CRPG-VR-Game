
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using static UnityEngine.Random;
using TMPro;

public class EnemyAgent : Agent {
    EnemyEnvController envController;

    //serialized fields
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject trainingEnv;
    [SerializeField]
    private float speed = 35;
    [SerializeField]
    TextMeshProUGUI scoreBoard;
    [SerializeField]
    GameObject ground;
    [SerializeField]
    int team;
    private const float turnAmount = 25;

    //reset variables
    new private Rigidbody rigidbody;
    private Vector3 originalTargetPosition;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float prevDistanceToTarget;
    int steps = 0;

    private EnvironmentParameters defaultParams;
    //environment edges
    private float groundEdge;
    private float negativeX;
    private float positiveX;
    private float negativeZ;
    private float positiveZ;

    //scoreboard variables
    static int episodesDone = 0;
    static int fails = 0;
    static int outOfSteps = 0;
    static int successes = 0;

    //Timer variables
    //protected float Timer = 0;
    //public float DelayAmount = 10f;

    //health variables
    public float healthPoints = 100;
    public float targetHealthPoints;
    public const float maxHealthPoints = 100;
    public const int damage = 5;

    void Start() {
        envController = trainingEnv.GetComponent<EnemyEnvController>();
    }
    public override void Initialize() {

        originalPosition = transform.position;
        originalRotation = transform.rotation;
        originalTargetPosition = target.transform.position;
        targetHealthPoints = target.GetComponent<EnemyAgent>().healthPoints;
        //GameObject parent = gameObject.transform.parent.gameObject;

        //Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        //foreach (Transform child in allChildren) {

        //    if (child.gameObject.tag == "Ground") {
        //        ground = child.gameObject;
        //    }
        //}

        groundEdge = ground.GetComponent<Renderer>().bounds.size.x / 2;
        negativeX = ground.transform.position.x - groundEdge;
        positiveX = ground.transform.position.x + groundEdge;
        negativeZ = ground.transform.position.z - groundEdge;
        positiveZ = ground.transform.position.z + groundEdge;
        rigidbody = GetComponent<Rigidbody>();
        defaultParams = Academy.Instance.EnvironmentParameters;
    }

    //---------------------------------------------------------------------

    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(healthPoints);
        sensor.AddObservation(healthPoints);
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

        //if (steps > 1000) {
        //    AddReward(-0.0005f);
        //}

        AddReward(-0.0005f);


        float turnDirection = 0f;

        if (actionBuffers.DiscreteActions[1] == 1f) {
            turnDirection = -1f;
        }
        else if (actionBuffers.DiscreteActions[1] == 2f) {
            turnDirection = 1f;
        }

        bool punch = false;
        if (actionBuffers.DiscreteActions[2] == 1f) {
            punch = true;
        }

        rigidbody.MovePosition(transform.position + transform.forward * forwardAmount * speed * Time.fixedDeltaTime);

        transform.Rotate(transform.up * turnDirection * turnAmount * Time.fixedDeltaTime);

    }

    //---------------------------------------------------------------------

    public override void OnEpisodeBegin() {
        healthPoints = maxHealthPoints;
        rigidbody.velocity = Vector3.zero;
        steps = 0;
        scoreBoard.text = "Episodes: " + episodesDone.ToString() + "\tSuccesses: " + successes.ToString() + "\tFails: " + fails.ToString() + "\nOut of Steps: " + outOfSteps.ToString();
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
            //else if (collision.gameObject.tag == "Player") {
            //    Debug.Log("gotcha");
            //    AddReward(1f);
            //    successes++;
            //    episodesDone++;
            //    EndEpisode();
            //}
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
        //targetHealthPoints = target.GetComponent<EnemyAgent>().healthPoints;
        steps++;
        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (targetHealthPoints <= 0f) {
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
            outOfSteps++;

            //add small negative reward
            EndEpisode();
        }

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

}