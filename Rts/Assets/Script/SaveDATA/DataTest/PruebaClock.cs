using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PruebaClock : MonoBehaviour
{
    private TextMeshProUGUI timeDisplay;
    DateTime currentTime;

    private void Start()
    {
        timeDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        currentTime=DateTime.Now;
        timeDisplay.text = currentTime.ToString("T");
    }
}
