using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour {

    public int score = 0;

    private Text textBox;
    private StateManager stateManager;

    // Use this for initialization
    void Start () {
        textBox = GetComponent<Text>();
        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
    }
	
	// Update is called once per frame
	void Update () {
        /* Increase score as the player is player (the longer you go, the better)
        if (stateManager.GetGameState() != StateManager.GameplayState.END)
        {
            score++;
        }
        */

        textBox.text = "Score: " + score;
	}
}
