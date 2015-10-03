using UnityEngine;
using System.Collections;

public class CharacterMove : MonoBehaviour {
    public float baseSpeed;

    private Transform playerTransform;

	// Use this for initialization
	void Start () {
        baseSpeed = 1.0f;
        playerTransform = gameObject.GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {

        //playerTransform.position.x += baseSpeed * Time.deltaTime;
        playerTransform.Translate(baseSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
	}
}
