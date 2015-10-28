using UnityEngine;
using System.Collections;

/** This class will handle logic for the tutorial screen.  Menu screens will be handled by the "MenuManager" class */
public class TutorialGameManager : MonoBehaviour
{
    private float MIN_DOG_FOLLOW_DISTANCE = 5.0f;

    public Dog dog;
    public Player player;

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

        float distance = ( dog.transform.position - player.transform.position ).magnitude;

        // Force the dog to not get too close to the player during the Tutorial
        if (distance < MIN_DOG_FOLLOW_DISTANCE)
        {
            dog.transform.position = new Vector3
            (
                player.transform.position.x - MIN_DOG_FOLLOW_DISTANCE,
                dog.currentLane.GetY(),
                dog.transform.position.z
            );
        }
    }
}
