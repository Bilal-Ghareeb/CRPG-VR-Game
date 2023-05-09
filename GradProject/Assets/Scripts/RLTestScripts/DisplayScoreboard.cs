using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayScoreboard : MonoBehaviour {
    ScoreBoard scoreBoard;
    [SerializeField]
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start() {
        scoreBoard = new ScoreBoard();
    }

    // Update is called once per frame
    void Update() {
        text.text = scoreBoard.getText();
    }
}
