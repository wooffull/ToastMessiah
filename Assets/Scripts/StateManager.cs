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
    public GameObject endText;
    public GameplayState playState = GameplayState.START;

    void Start()
    {
    }

    private void HandleNewState(GameplayState handleState)
    {
        switch (playState)
        {
            case GameplayState.START:
                break;
            case GameplayState.PLAYING:
                // Hide pause text
                pauseText.gameObject.SetActive(false);
                break;
            case GameplayState.PAUSE:
                // Show pause text
                pauseText.gameObject.SetActive(true);
                break;
            case GameplayState.END:
                // Activate end text, since it is de-active by default and isn't changed anywhere else
                endText.gameObject.SetActive(true);
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
