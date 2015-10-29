using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WinMarker : MonoBehaviour
{
    private StateManager stateManager;

    void Start(){
        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When running into player, run code!
        if (other.tag == "Player")
        {
            // Change end text to display win message
            stateManager.winText.SetActive(true);
            stateManager.SetGameState(StateManager.GameplayState.END);
        }
    }
}
