using UnityEngine;
using System.Collections;

public class DogActionMarker : MonoBehaviour
{
    public DogState dogState = DogState.LUNGE;

    void OnTriggerEnter2D(Collider2D other)
    {
        // When running into player, run code!
        if (other.tag == "Player")
        {
            Dog dog = FindObjectOfType<Dog>();
            dog.currentState = dogState;
        }
    }
}
