using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject startButton; // GameObject with start button

    private StatesManager stateManager;

    // Use this for initialization
    void Start()
    {
        stateManager = gameObject.GetComponent<StatesManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Start button de-activates itself when clicked, so we know it has been clicked if it is not active
        // Don't need to check if gameplay should start, unless the currrent game state is Start
        if (stateManager.GetGameState() == StatesManager.GameplayState.START && !startButton.activeInHierarchy)
        {
            stateManager.SetGameState(StatesManager.GameplayState.PLAYING);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (stateManager.GetGameState() == StatesManager.GameplayState.PLAYING) { stateManager.SetGameState(StatesManager.GameplayState.PAUSE); }
            else if (stateManager.GetGameState() == StatesManager.GameplayState.PAUSE) { stateManager.SetGameState(StatesManager.GameplayState.PLAYING); }
        }
    }
}
