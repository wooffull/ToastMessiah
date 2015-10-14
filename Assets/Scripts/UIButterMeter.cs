using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIButterMeter : MonoBehaviour {
    public float percentage = 1.0f;
    public float lossAmount = 0.0075f;

    private Slider butterSlider;
    private StateManager stateManager;

	// Use this for initialization
	void Start () {
        butterSlider = gameObject.GetComponent<Slider>();
        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
    }
	
	// Update is called if player isn't eaten
	public void UpdatePercentage() {
        // Limit the percentage interval between 0 and 1
        percentage = Mathf.Max(0.0f, Mathf.Min(percentage, 1.0f));

        butterSlider.value = percentage;

        // Only decrease butter if playing
        if (stateManager.GetGameState() == StateManager.GameplayState.PLAYING)
        {
            percentage -= lossAmount;
        }
        // When we run out of butter, go to Game Over state
        if (percentage < 0.001f) // Account for rounding errors and stuff
        {
            stateManager.SetGameState(StateManager.GameplayState.END);
        }
    }

    public void AddButter(Pickup pickup)
    {
        percentage += pickup.butterAmount;
    }
}
