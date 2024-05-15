using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance;
    private DateTime dateTime;
    public DateTime GetDateTime() { return dateTime; }

    public bool isDay = true;
    private float time = 0;
    private double min = 0;

    private void Awake()
    {
        Instance = this;
        dateTime = new DateTime(2000, 01, 01, 07, 00, 00);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (time >= 1)
        {
            dateTime = dateTime.AddMinutes(time);
            time = 0;
        }
    }
}
