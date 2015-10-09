using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startButton; // GameObject with start button

    private StateManager stateManager;

    // Use this for initialization
    void Start()
    {
        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Start button de-activates itself when clicked, so we know it has been clicked if it is not active
        // Don't need to check if gameplay should start, unless the currrent game state is Start
        if (stateManager.GetGameState() == StateManager.GameplayState.START && !startButton.activeInHierarchy)
        {
            stateManager.SetGameState(StateManager.GameplayState.PLAYING);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (stateManager.GetGameState() == StateManager.GameplayState.PLAYING) { stateManager.SetGameState(StateManager.GameplayState.PAUSE); }
            else if (stateManager.GetGameState() == StateManager.GameplayState.PAUSE) { stateManager.SetGameState(StateManager.GameplayState.PLAYING); }
        }
    }
}
