﻿using System;
using UnityEngine;

public class PlayerTriggerCollider : MonoBehaviour
{
    public event Action Parked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Garage"))
        {
            Debug.Log("Parked");
            Parked();
        }
    }
}
