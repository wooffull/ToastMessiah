using UnityEngine;
using System.Collections;

public enum DogState
{
    CHASE,
    LUNGE,
    REST,
    EATING
}

public class Dog : MonoBehaviour {
    public const float FOLLOW_DISTANCE = 7;
    
    private const int MAX_LUNGE_TIMER = 150;
    private const float FULLY_LUNGED_PERCENTAGE = 0.35f; // Percentage of time while in LUNGE state that the dog is fully lunged (at the player's x-coordinate)
    private const float LUNGE_DELAY = 0.75f; // Time that the lunge warning shows before dog starts lunge
    
    public DogState currentState = DogState.CHASE;
    public Lane currentLane = Lane.MID;

    private Player player;
    private GameObject warningSprite;
    private int lungeTimer = 0;
    private float delayTimer = 0.0f;
    private StateManager stateManager;

    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<Player>();
        warningSprite = gameObject.transform.GetChild(0).gameObject; // Get gameObject of warning sprite
        warningSprite.SetActive(false);

        transform.position = new Vector3
        (
            player.transform.position.x - FOLLOW_DISTANCE,
            transform.position.y,
            transform.position.z
        );

        stateManager = GameObject.Find("GameController").GetComponent<StateManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (stateManager.GetGameState() == StateManager.GameplayState.PLAYING)
        {
            UpdateState();
            Move();
        }
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case DogState.CHASE:
                currentLane = player.currentLane;
                break;

            case DogState.LUNGE:
                // Take care of warning period
                if (delayTimer < LUNGE_DELAY) {
                    delayTimer += Time.deltaTime;
                    warningSprite.SetActive(true);
                }
                else {
                    warningSprite.SetActive(false);
                    lungeTimer++;

                    // If dog has finished lunging, make it go back to chasing
                    if (lungeTimer > MAX_LUNGE_TIMER)
                    {
                        lungeTimer = 0;
                        delayTimer = 0.0f;
                        currentState = DogState.CHASE;
                    }
                }
                break;
        }
    }

    void Move()
    {
        switch (currentState)
        {
            case DogState.CHASE:
                transform.Translate(player.baseSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                transform.position = new Vector3
                (
                    transform.position.x,
                    currentLane.GetY(),
                    transform.position.z
                );
                break;

            case DogState.LUNGE:
                // Lunge when warning period is up, otherwise move as if chasing
                if (delayTimer < LUNGE_DELAY) {
                    transform.Translate(player.baseSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                    transform.position = new Vector3
                    (
                        transform.position.x,
                        currentLane.GetY(),
                        transform.position.z
                    );
                }
                else {
                    float lungePercentage = lungeTimer / (float)MAX_LUNGE_TIMER;

                    // Dog is approaching the player (making the lunge)
                    if (lungePercentage < (1 - FULLY_LUNGED_PERCENTAGE) * 0.5f)
                    {
                        // Dog is behind player and farther than it should be, so snap it to where it should be (FOLLOW_DISTANCE)
                        if (transform.position.x < player.transform.position.x - FOLLOW_DISTANCE)
                        {
                            transform.position = new Vector3
                            (
                                player.transform.position.x - FOLLOW_DISTANCE,
                                transform.position.y,
                                transform.position.z
                            );
                        }

                        float speedIncrease = FOLLOW_DISTANCE / (FULLY_LUNGED_PERCENTAGE * MAX_LUNGE_TIMER);
                        transform.Translate(player.actualSpeed * Time.deltaTime + speedIncrease, 0.0f, 0.0f, Space.World);
                        // Dog is past the player, so snap it to where the player's x-position is
                        // This transition is jittery, looks not great, but idk how to avoid that...
                        if (transform.position.x > player.transform.position.x)
                        {
                            transform.position = new Vector3
                             (
                                 player.transform.position.x,
                                 transform.position.y,
                                 transform.position.z
                             );
                        }
                    }

                    // Dog is coming back from the lunge
                    else if (lungePercentage > 1 - (1 - FULLY_LUNGED_PERCENTAGE) * 0.5f)
                    {
                        float speedDecrease = FOLLOW_DISTANCE / (FULLY_LUNGED_PERCENTAGE * MAX_LUNGE_TIMER);
                        transform.Translate(player.baseSpeed * Time.deltaTime - speedDecrease, 0.0f, 0.0f, Space.World);

                        // Dog is behind player and farther than it should be, so snap it to where it should be (FOLLOW_DISTANCE)
                        if (transform.position.x <= player.transform.position.x - FOLLOW_DISTANCE)
                        {
                            transform.position = new Vector3
                            (
                                player.transform.position.x - FOLLOW_DISTANCE,
                                transform.position.y,
                                transform.position.z
                            );
                        }
                    }

                    // Dog has lunged and is sticking around the player's x-coordinate until the dog tires out
                    else
                    {
                        transform.Translate(player.actualSpeed * 0.95f * Time.deltaTime, 0.0f, 0.0f, Space.World);
                    }
                }
                break;
        }
    }

    public void Stop()
    {
        currentState = DogState.EATING;
    }
}
