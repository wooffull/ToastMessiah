using UnityEngine;
using System.Collections;

public class TextNotificationMarker : MonoBehaviour
{
    public GameObject text;
    public bool setActive = true;

    void OnTriggerEnter2D(Collider2D other)
    {
        // When running into player, run code!
        if (other.tag == "Player")
        {
            text.SetActive(setActive);
        }
    }
}
