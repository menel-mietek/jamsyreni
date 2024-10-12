using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;  // Reference to the TextMeshPro UI component
    [SerializeField] private float remainingtime = 60f;  // Total time in seconds (60 seconds for example)

    void Update()
    {
        if (remainingtime > 0)
        {
            // Subtract time from the remaining time using Time.deltaTime
            remainingtime -= Time.deltaTime;

            // Ensure remaining time does not go below zero
            if (remainingtime < 0)
            {
                remainingtime = 0;
            }

            // Update the displayed timer
            int minutes = Mathf.FloorToInt(remainingtime / 60);  // Calculate minutes
            int seconds = Mathf.FloorToInt(remainingtime % 60);  // Calculate seconds

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
