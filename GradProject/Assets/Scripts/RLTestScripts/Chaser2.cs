using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
using static UnityEngine.Random;
using UnityEngine.UI;
using TMPro;
public class Chaser2 : Agent {
    public GameObject target;
    public GameObject agentObject;
    public float speed = 35;
    new private Rigidbody rigidbody;
    static int episodesDone = 0;
    static int fails = 0;
    static int successes = 0;
    [SerializeField]
    TextMeshProUGUI scoreBoard;

    private Vector3 originalTargetPosition;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private const float groundEdge = 84;

    private float negativeX;
    private float positiveX;
    private float negativeZ;
    private float positiveZ;
    EnvironmentParameters defaultParams;
    public override void Initialize() {
        defaultParams = Academy.Instance.EnvironmentParameters;
        originalPosition = transform.position;
        originalRotation = transform.rotation;

        originalTargetPosition = target.transform.position;

        GameObject parent = gameObject.transform.parent.gameObject;
        GameObject cube;

        Transform[] allChildren = parent.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren) {

            if (child.gameObject.tag == "Ground") {
                cube = child.gameObject;
                negativeX = cube.transform.position.x - groundEdge;
                positiveX = cube.transform.position.x + groundEdge;
                negativeZ = cube.transform.position.z - groundEdge;
                positiveZ = cube.transform.position.z + groundEdge;
            }
        }


        rigidbody = GetComponent<Rigidbody>();
        defaultParams = Academy.Instance.EnvironmentParameters;
    }
    public override void CollectObservations(VectorSensor sensor) {
        sensor.AddObservation(target.transform.localPosition);
        sensor.AddObservation(transform.localPosition);
    }
    public override void OnActionReceived(ActionBuffers vectorAction) {

        this.transform.Translate(Vector3.right * vectorAction.ContinuousActions[0] * speed * Time.deltaTime);
        this.transform.Translate(Vector3.forward * vectorAction.ContinuousActions[1] * speed * Time.deltaTime);
        AddReward(-0.0005f);

    }

    private void OnCollisionStay(Collision collision) {
        if (collision != null) {
            if (collision.gameObject.tag == "Wall") {
                AddReward(-1f);
                fails++;
                episodesDone++;
                EndEpisode();

            }
            else if (collision.gameObject.tag == "Player") {

                AddReward(1f);
                successes++;
                episodesDone++;
                EndEpisode();
            }
        }
    }

    public override void OnEpisodeBegin() {

        rigidbody.velocity = Vector3.zero;
        ResetParamters();
        scoreBoard.text = "Episodes: " + episodesDone.ToString() + "\tSuccesses: " + successes.ToString() + "\tFails: " + fails.ToString();
    }
    public void ResetParamters() {
        transform.position = originalPosition;
        transform.rotation = originalRotation;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        target.transform.position = GetRandomSpawnPos();
    }
    public override void Heuristic(in ActionBuffers actionsOut) {
        float[] actions = { Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") };
        ActionBuffers.FromDiscreteActions(actions);
    }
    void FixedUpdate() {

        float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
        if (distanceToTarget <= 10f) {
            SetReward(1f);
            successes++;
            episodesDone++;

            EndEpisode();
        }
    }

    public Vector3 GetRandomSpawnPos() {
        var randomPosX = Range(negativeX + 23,
                positiveX - 23);
        var randomPosZ = Range(negativeZ + 23,
           positiveZ - 23);
        var randomSpawnPos = new Vector3(randomPosX, originalTargetPosition.y, randomPosZ);

        return randomSpawnPos;
    }
}