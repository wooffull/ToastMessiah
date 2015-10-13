using UnityEngine;
using System.Collections;

/** This class will handle logic for menu screens.  Gameplay screens will be handled by the "GameManager" class */
public class MenuManager : MonoBehaviour
{
    public GameObject startButton; // GameObject with start button
    public GameObject helpButton; // GameObject with help button
    public GameObject backButton; // GameObject with back button

    private StateManager stateManager;

	// Use this for initialization
	void Start () {
        stateManager = gameObject.GetComponent<StateManager>();
	}
	
	// Update is called once per frame
	void Update () {
        // Start button de-activates itself when clicked, so we know it has been clicked if it is not active
        // Don't need to check if level should load, unless the currrent game state is Start
        if (stateManager.GetGameState() == StateManager.GameplayState.START && !startButton.activeInHierarchy)
        {
            Application.LoadLevel("TutorialScene");
        }
        // Same deal as start button, but for checking if help text should be displayed
        else if (stateManager.GetGameState() == StateManager.GameplayState.START && !helpButton.activeInHierarchy)
        {
            startButton.SetActive(false);
            backButton.SetActive(true);
            stateManager.SetGameState(StateManager.GameplayState.PAUSE);
        }
        // Same deal as start button, but for checking if start and help buttons should be displayed
        if (stateManager.GetGameState() == StateManager.GameplayState.PAUSE && !backButton.activeInHierarchy)
        {
            startButton.SetActive(true);
            helpButton.SetActive(true);
            stateManager.SetGameState(StateManager.GameplayState.START);
        }
	}
}
