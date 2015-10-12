using UnityEngine;
using System.Collections;

/** This class will handle logic for gameplay screens.  Menu screens will be handled by the "MenuManager" class */
public class GameManager : MonoBehaviour
{
    private StateManager stateManager;

    // Use this for initialization
    void Start()
    {
        //stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
        stateManager = gameObject.GetComponent<StateManager>();
        stateManager.SetGameState(StateManager.GameplayState.PLAYING); // Immediately start gameplay
    }

    // Update is called once per frame
    void Update()
    {
        // Check for pause/ unpause
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (stateManager.GetGameState() == StateManager.GameplayState.PLAYING) { stateManager.SetGameState(StateManager.GameplayState.PAUSE); }
            else if (stateManager.GetGameState() == StateManager.GameplayState.PAUSE) { stateManager.SetGameState(StateManager.GameplayState.PLAYING); }
        }
    }
}
