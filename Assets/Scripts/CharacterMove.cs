using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float baseSpeed;

    private Transform playerTransform;
    private Lane currentLane;

    private enum Lane {
        FAR,
        MID,
        CLOSE
    };

	// Use this for initialization
	void Start () {
        baseSpeed = 1.0f;
        playerTransform = gameObject.GetComponent<Transform>();
        currentLane = Lane.MID;
	}
	
	// Update is called once per frame
	void Update () {
        playerTransform.Translate(baseSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);

        // Get input for lane changing
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            ShiftLaneDown();
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            ShiftLaneUp();
        }
	}

    void ShiftLaneUp() {
        Debug.Log("Lane Up");
        switch (currentLane) {
            case Lane.CLOSE:
                playerTransform.Translate(0.0f, 0.0f, 2.0f, Space.World);
                currentLane = Lane.MID;
                break;
            case Lane.MID:
                playerTransform.Translate(0.0f, 0.0f, 2.0f, Space.World);
                currentLane = Lane.FAR;
                break;
            case Lane.FAR:
                break;
        }
    }

    void ShiftLaneDown() {
        Debug.Log("Lane Down");
        switch (currentLane)
        {
            case Lane.CLOSE:
                break;
            case Lane.MID:
                playerTransform.Translate(0.0f, 0.0f, -2.0f, Space.World);
                currentLane = Lane.CLOSE;
                break;
            case Lane.FAR:
                playerTransform.Translate(0.0f, 0.0f, -2.0f, Space.World);
                currentLane = Lane.MID;
                break;
        }
    }
}
