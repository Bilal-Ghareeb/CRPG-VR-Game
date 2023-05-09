using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponCollision : MonoBehaviour {
    [SerializeField]
    EnemyAgent agent;
    [SerializeField]
    GameObject target;
    public readonly int damage = 10;

    bool hit = false;
    //private void Start() {
    //    agent = GetComponent<EnemyAgent>();
    //}
    private void OnTriggerEnter(Collider other) {
        if (other != null) {
            if (other.gameObject.tag == "Player") {
                if (!hit) {
                    agent.AddReward(0.1f);
                    hit = true;
                    target.GetComponent<HealthBar>().takeDamage(damage);
                    //target.penalties -= 0.1f;
                    //target.AddReward(-0.1f);
                    print(agent.name + " hit " + target.name + ",\ntarget health: " + target.GetComponent<HealthBar>().getHealth());
                }
            }
            else if (other.gameObject.tag == "Shield") {
                print("blocked");
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other != null) {
            if (other.gameObject.tag == "Player") {
                hit = false;
            }
        }
    }
    // Start is called before the first frame update
    //void Start() {

    //}

    //// Update is called once per frame
    //void Update() {

    //}
}
