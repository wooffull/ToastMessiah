using UnityEngine;
using System.Collections;

/** This class will handle logic for the tutorial screen.  Menu screens will be handled by the "MenuManager" class */
public class TutorialGameManager : MonoBehaviour
{
    private StateManager stateManager;

    // Use this for initialization
    void Start()
    {
        stateManager = gameObject.GetComponent<StateManager>();
        stateManager.SetGameState(StateManager.GameplayState.PLAYING); // Immediately start gameplay
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && stateManager.GetGameState() == StateManager.GameplayState.PAUSE)
        {
            stateManager.SetGameState(StateManager.GameplayState.PLAYING); 
        }
    }
}
