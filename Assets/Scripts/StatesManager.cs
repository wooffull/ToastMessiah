using UnityEngine;
using System.Collections;

public class StatesManager : MonoBehaviour {
    public enum GameplayState
    {
        START,
        PLAYING,
        PAUSE,
        END
    }

    public GameObject pauseText;
    public GameObject endText;

    private GameplayState playState = GameplayState.START;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
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
