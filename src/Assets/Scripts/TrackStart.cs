﻿using UnityEngine;

public class TrackStart : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerCanvas.instance.IncrementLapCount();
    }
}