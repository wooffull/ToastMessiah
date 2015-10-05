using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
    public float baseSpeed = 1;

    private Transform playerTransform;
    private Lane currentLane;

    // Use this for initialization
    void Start () {
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
        if (other.tag == "Butter")
        {
            Butter b = other.gameObject.GetComponent<Butter>();

            if (b.lane == currentLane)
            {
                Destroy(other.gameObject);
            }
        }
    }
}
