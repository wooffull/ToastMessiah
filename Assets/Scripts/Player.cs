using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float baseSpeed = 1;
    public ButterMeter butterMeter;

    private Transform playerTransform;
    private Lane currentLane;
    private GameObject manager;
    private StatesManager stateManager;

    // Use this for initialization
    void Start () {
        playerTransform = gameObject.GetComponent<Transform>();
        currentLane = Lane.MID;

        manager = GameObject.FindGameObjectWithTag("GameController");
        stateManager = manager.GetComponent<StatesManager>();
    }
	
	// Update is called once per frame
	void Update () {
        float butterPercentage = butterMeter.percentage;

        if (butterPercentage > 0 && (stateManager.GetGameState() == StatesManager.GameplayState.PLAYING))
        {
            playerTransform.Translate(baseSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);

            // Get input for lane changing
            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                ShiftLaneDown();
            }
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                ShiftLaneUp();
            }
        }
	}

    void ShiftLaneUp() {
        Debug.Log("Lane Up");
        switch (currentLane)
        {
            case Lane.CLOSE:
                currentLane = Lane.MID;
                break;
            case Lane.MID:
                currentLane = Lane.FAR;
                break;
            case Lane.FAR:
                break;
        }

        transform.position = new Vector3
        (
            transform.position.x,
            currentLane.GetY(),
            transform.position.z
        );
    }

    void ShiftLaneDown() {
        Debug.Log("Lane Down");
        switch (currentLane)
        {
            case Lane.CLOSE:
                break;
            case Lane.MID:
                currentLane = Lane.CLOSE;
                break;
            case Lane.FAR:
                currentLane = Lane.MID;
                break;
        }

        transform.position = new Vector3
        (
            transform.position.x,
            currentLane.GetY(),
            transform.position.z
        );
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Obstacles and Butter behave the same way, except Obstacles add negative Butter
        if (other.tag == "Pickup")
        {
            Pickup p = other.gameObject.GetComponent<Pickup>();

            if (p.lane == currentLane)
            {
                butterMeter.AddButter(p);
                Destroy(other.gameObject);
            }
        }
    }
}
