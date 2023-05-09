using Oculus.Platform.Models;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using static UnityEngine.GraphicsBuffer;

public class EnemyEnvController : MonoBehaviour {

    [SerializeField]
    GameObject ground;
    [SerializeField]
    EnemyAgent agent1, agent2;

    public List<EnemyAgent> AgentsList = new List<EnemyAgent>();
    List<Renderer> RenderersList = new List<Renderer>();
    Rigidbody agent1Rb, agent2Rb;
    int lastHitterTeam;

    private Vector3 originalTargetPosition;
    private float groundEdge;
    private float negativeX;
    private float positiveX;
    private float negativeZ;
    private float positiveZ;

    private int resetTimer;
    public int MaxEnvironmentSteps;
    // Start is called before the first frame update
    void Start() {
        agent1Rb = agent1.GetComponent<Rigidbody>();
        agent2Rb = agent2.GetComponent<Rigidbody>();
        originalTargetPosition = agent1.transform.position;
        groundEdge = ground.GetComponent<Renderer>().bounds.size.x / 2;
        negativeX = ground.transform.position.x - groundEdge;
        positiveX = ground.transform.position.x + groundEdge;
        negativeZ = ground.transform.position.z - groundEdge;
        positiveZ = ground.transform.position.z + groundEdge;
        ResetScene();
    }
    public void UpdateLastHitter(int team) {
        lastHitterTeam = team;
    }

    // Update is called once per frame
    void Update() {

    }

    public void ResetScene() {
        resetTimer = 0;

        //lastHitter = Team.Default; // reset last hitter

        foreach (var agent in AgentsList) {
            // randomise starting positions and rotations
            var randomPosX = Random.Range(-2f, 2f);
            var randomPosZ = Random.Range(-2f, 2f);
            var randomPosY = Random.Range(0.5f, 3.75f); // depends on jump height
            var randomRot = Random.Range(-45f, 45f);

            agent.transform.localPosition = new Vector3(randomPosX, randomPosY, randomPosZ);
            agent.transform.eulerAngles = new Vector3(0, randomRot, 0);

            agent.GetComponent<Rigidbody>().velocity = default(Vector3);
        }

        // reset ball to starting conditions
        //ResetBall();
    }

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
