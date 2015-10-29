using UnityEngine;
using System.Collections;

public class StateManager : MonoBehaviour {
    public enum GameplayState
    {
        START,
        PLAYING,
        PAUSE,
        END
    }

    public GameObject pauseText;
    public GameObject loseText;
    public GameObject winText;
    public GameplayState playState = GameplayState.START;

    void Start()
    {
    }

    private void HandleNewState(GameplayState handleState)
    {
        switch (playState)
        {
            case GameplayState.START:
                // Hide pause text
                if (pauseText)
                {
                    pauseText.gameObject.SetActive(false);
                }
                break;
            case GameplayState.PLAYING:
                if (pauseText)
                {
                    pauseText.gameObject.SetActive(false);
                }
                break;
            case GameplayState.PAUSE:
                // Show pause text
                if(pauseText) {
                    pauseText.gameObject.SetActive(true);
                }
                break;
            case GameplayState.END:
                // Game is over, so if win GO isn't active, then the player lost.
                // If that's the case, activate the lose GO
                if (!winText.activeInHierarchy) {
                    loseText.gameObject.SetActive(true);
                }
                // Should probably add in a reset button at some point
                break;
        }
    }

    public void SetGameState(GameplayState newState)
    {
        playState = newState;
        HandleNewState(newState);
    }

    public GameplayState GetGameState() { return playState;  }
}
