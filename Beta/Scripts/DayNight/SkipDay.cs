using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

//show sleep panel when SkipDayButton press

public class SkipDay : MonoBehaviour
{
    private DateTime dt;
    public Button ChangeDay;
    private bool check = false;

    void Start()
    {
        var tm = FindObjectOfType<TimeManager>();
        dt = tm.GetDateTime();
        WhatsTime();
    }

    void Update()
    {
        WhatsTime();
    }


    void WhatsTime()
    {
        if(dt.Hour == 23 || dt.Hour < 7)
        {
            ChangeDay.gameObject.SetActive(true);
        }
        else
        {
            ChangeDay.gameObject.SetActive(false);
        } 
    }
}
