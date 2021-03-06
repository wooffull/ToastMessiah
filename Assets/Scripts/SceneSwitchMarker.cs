﻿using UnityEngine;
using System.Collections;

public class SceneSwitchMarker : MonoBehaviour
{
    public string scene;

    void OnTriggerEnter2D(Collider2D other)
    {
        // When running into player, run code!
        if (other.tag == "Player")
        {
            Application.LoadLevel(scene);
        }
    }
}