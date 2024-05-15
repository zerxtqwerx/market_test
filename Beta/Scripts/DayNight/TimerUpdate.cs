using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TimerUpdate : MonoBehaviour
{
    public Text Timer;
    public Sprite imageDay;
    public Sprite imageNight;

    private Image image;
    private TimeManager tm;
    private DateTime dt;

    void Start()
    {
        tm = FindObjectOfType<TimeManager>();
        image = gameObject.GetComponentInChildren<Image>();

        if (tm.isDay)
        {
            Timer.color = Color.white;
        }
        else 
        { 
            Timer.color = Color.black; 
        }

        ChangeTextTimer();
        ChangeImage();
    }

    void Update()
    {
        ChangeTextTimer();
        ChangeImage();
    }

    void ChangeTextTimer()
    {
        dt = tm.GetDateTime();
        Timer.text = "" + dt.Day + " " + dt.ToString("MMM") + "\n" + dt.ToShortTimeString();
    }

    void ChangeImage()
    {
        if (dt.Hour == 20 || dt.Hour < 7)
        {
            if(image.sprite != imageNight)
            {
                image.sprite = imageNight;
            }
        }

        else if (dt.Hour == 7)
        {
            if (image.sprite != imageDay)
            {
                image.sprite = imageDay;
            }
        }
    }

}
