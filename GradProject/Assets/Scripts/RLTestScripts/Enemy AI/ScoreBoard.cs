using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreBoard {
    static string text = "Episodes: 0\tSuccesses: 0\tFails: 0\nOut of Steps: 0";


    static int episodesDone = 0;
    static int fails = 0;
    static int outOfSteps = 0;
    static int successes = 0;
    // Start is called before the first frame update
    public void succeeded() {
        successes++;
        episodesDone++;
        Update();

    }
    public void failed() {
        episodesDone++;
        fails++;
        Update();

    }
    public void ranOutOfSteps() {
        episodesDone++;
        outOfSteps++;
        Update();
    }

    public int getEpisodes() {
        return episodesDone;
    }

    public string getText() {
        return text;
    }
    public int getFails() {
        return fails;
    }

    public int getSteps() {
        return outOfSteps;
    }
    // Update is called once per frame
    void Update() {
        text = "Episodes: " + episodesDone.ToString() + "\tSuccesses: " + successes.ToString() + "\tFails: " + fails.ToString() + "\nOut of Steps: " + outOfSteps.ToString();

    }
}
