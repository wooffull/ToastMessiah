using UnityEngine;
using System.Collections;

public class Butter : MonoBehaviour {
    public Lane lane;
    public float butterAmount = 0.1f;

	// Use this for initialization
	void Start ()
    {
        transform.position = new Vector3
        (
            transform.position.x,
            lane.GetY(),
            transform.position.z
        );
    }
	
	// Update is called once per frame
	void Update () {
	}
}
