using UnityEngine;
using System.Collections;

public class ButterMeter : MonoBehaviour {
    public FollowPlayer gameCamera;
    public GameObject filledButterMeter;
    public float percentage = 1.0f;
    public float lossAmount = 0.0075f;

	// Use this for initialization
	void Start () {
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

        percentage -= lossAmount;
    }

    public void AddButter(Pickup pickup)
    {
        percentage += pickup.butterAmount;
    }
}
