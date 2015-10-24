using UnityEngine;
using System.Collections;

public class TextNotificationMarker : MonoBehaviour
{
    public GameObject text;
    public bool setActive = true;

    private StateManager stateManager;

    void Start(){
        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // When running into player, run code!
        if (other.tag == "Player")
        {
            text.SetActive(setActive);
            // Pause game when text is shown
            stateManager.SetGameState(StateManager.GameplayState.PAUSE);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Disable text when player leaves marker area
        if (other.tag == "Player"){
            text.SetActive(false);
        }
    }
}
