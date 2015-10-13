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

    public float lungeChance = 0.005f;
    public DogState currentState = DogState.CHASE;
    public Lane currentLane = Lane.MID;

    private Player player;
    private int lungeTimer = 0;
    private StateManager stateManager;

    // Use this for initialization
    void Start () {
        player = GameObject.FindObjectOfType<Player>();

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

                if (Random.value < lungeChance)
                {
                    currentState = DogState.LUNGE;
                }
                break;

            case DogState.LUNGE:
                lungeTimer++;
                
                // If dog has finished lunging, make it go back to chasing
                if( lungeTimer > MAX_LUNGE_TIMER )
                {
                    lungeTimer = 0;
                    currentState = DogState.CHASE;
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
                float lungePercentage = lungeTimer / (float)MAX_LUNGE_TIMER;

                // Dog is approaching the player (making the lunge)
                if (lungePercentage < ( 1 - FULLY_LUNGED_PERCENTAGE ) * 0.5f )
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

                    float speedIncrease = FOLLOW_DISTANCE / ( FULLY_LUNGED_PERCENTAGE * MAX_LUNGE_TIMER );
                    transform.Translate(1.0f * player.baseSpeed * Time.deltaTime + speedIncrease, 0.0f, 0.0f, Space.World);

                    // Dog is past the player, so snap it to where the player's x-position is
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
                else if (lungePercentage > 1 - ( 1 - FULLY_LUNGED_PERCENTAGE ) * 0.5f )
                {
                    float speedDecrease = FOLLOW_DISTANCE / (FULLY_LUNGED_PERCENTAGE * MAX_LUNGE_TIMER);
                    transform.Translate(1.0f * player.baseSpeed * Time.deltaTime - speedDecrease, 0.0f, 0.0f, Space.World);

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
                    transform.Translate(player.baseSpeed * Time.deltaTime, 0.0f, 0.0f, Space.World);
                }
                break;
        }
    }

    public void Stop()
    {
        currentState = DogState.EATING;
    }
}
