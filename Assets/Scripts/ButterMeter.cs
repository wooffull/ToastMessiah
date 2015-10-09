using UnityEngine;
using System.Collections;

public class ButterMeter : MonoBehaviour {
    public FollowPlayer gameCamera;
    public GameObject filledButterMeter;
    public float percentage = 1.0f;
    public float lossAmount = 0.0075f;

    private GameObject manager;
    private StatesManager stateManager;

	// Use this for initialization
	void Start () {
        // Set up managers
        manager = GameObject.FindGameObjectWithTag("GameController");
        stateManager = manager.GetComponent<StatesManager>();
    }
	
	// Update is called if player isn't eaten
	public void UpdatePercentage() {
        // Limit the percentage interval between 0 and 1
        percentage = Mathf.Max(0.0f, Mathf.Min(percentage, 1.0f));

        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2
        (
            gameCamera.transform.position.x - 7.5f,
            gameCamera.transform.position.y + 2
        );

        filledButterMeter.transform.localScale = new Vector3
        (
            percentage,
            1,
            1
        );
        // Only decrease butter if playing
        if (stateManager.GetGameState() == StatesManager.GameplayState.PLAYING)
        {
            percentage -= lossAmount;
        }
        // When we run out of butter, go to Game Over state
        if (percentage < 0.001f) // Account for rounding errors and stuff
        {
            stateManager.SetGameState(StatesManager.GameplayState.END);
        }
    }

    public void AddButter(Pickup pickup)
    {
        percentage += pickup.butterAmount;
    }
}
