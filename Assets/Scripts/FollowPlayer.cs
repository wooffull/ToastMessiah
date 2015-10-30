using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

    public Player player;

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        float newX = player.gameObject.GetComponent<Transform>().position.x - player.currentLane.GetOffset().x + 15.0f; // Keep camera a little ahead of player
        transform.position = new Vector3
        (
            newX,
            transform.position.y,
            transform.position.z
        );
	}
}
