using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour {
    public float baseSpeed = 1;
    public float actualSpeed;
    public Lane currentLane = Lane.MID;
    //public ButterMeter butterMeter;
    public Slider butterMeterSlider;
    public GameObject referenceLine;

    private bool eaten;
    private UIButterMeter butterMeter;
    private Transform playerTransform;
    private StateManager stateManager;

    // Use this for initialization
    void Start () {
        currentLane = Lane.MID;
        eaten = false;
        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
        butterMeter = butterMeterSlider.GetComponent<UIButterMeter>();
        actualSpeed = baseSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        if (!eaten){
            float butterPercentage = butterMeter.percentage;

            if (butterPercentage > 0 && (stateManager.GetGameState() == StateManager.GameplayState.PLAYING))
            {
                if (butterPercentage > 0)
                {
                    // Reduce speed based on how much butter is remaining
                    actualSpeed = baseSpeed - (butterPercentage * 0.4f);
                    transform.Translate(actualSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);

                    // Get input for lane changing
                    if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
                    {
                        ShiftLaneDown();
                    }
                    if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
                    {
                        ShiftLaneUp();
                    }
                }
            }
        }
	}

    void LateUpdate()
    {
        if(!eaten)
        {
            referenceLine.transform.position = new Vector3
            (
                3 + transform.position.x - currentLane.GetOffset().x,
                referenceLine.transform.position.y,
                referenceLine.transform.position.z
            );
            butterMeter.UpdatePercentage();
        }
    }

    void ShiftLaneUp() {
        Debug.Log("Lane Up");

        Vector3 prevLaneOffset = currentLane.GetOffset();

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
            currentLane.GetY() + 0.1f,
            transform.position.z
        ) - prevLaneOffset + currentLane.GetOffset();
    }

    void ShiftLaneDown() {
        Debug.Log("Lane Down");

        Vector3 prevLaneOffset = currentLane.GetOffset();

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
            currentLane.GetY() + 0.1f,
            transform.position.z
        ) - prevLaneOffset + currentLane.GetOffset();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Obstacles and Butter behave the same way, except Obstacles add negative Butter
        if (other.tag == "Pickup")
        {
            Pickup p = other.GetComponent<Pickup>();

            if (p.lane == currentLane)
            {
                butterMeter.AddButter(p);

                // Add to score if the pickup has a positive value
                if (p.butterAmount > 0)
                {
                    ScoreTracker s = FindObjectOfType<ScoreTracker>();
                    s.score += (int)(p.butterAmount * 1000);
                }

                Destroy(other.gameObject);
            }
        }

        // Dog caught up to the player, Game Over!
        else if (other.tag == "Dog")
        {
            Dog d = other.GetComponent<Dog>();

            if( d.currentState != DogState.EATING )
            {
                d.Stop();
                eaten = true;
                stateManager.SetGameState(StateManager.GameplayState.END);
            }
        }
    }
}
