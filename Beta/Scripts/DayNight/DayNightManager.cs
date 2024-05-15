using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class DayNightManager : MonoBehaviour
{
    public static DayNightManager Instant;
    public Transform sun;
    public Light DirectSunLight;
    public Light MoonLight;

    public float time = 420;   
    //public Vector2 dayTime = new Vector2(420, 1140); 
    //public Vector2 nightTime = new Vector2(1140, 1380);
    private float x = 0;
    private float y = 0;

    public bool isDay = true;

    public AnimationCurve lightStreightDay;

    public UnityEvent onDay;
    public UnityEvent onNight;
    public UnityEvent onSleep;

    private DateTime dateTime; 
    public DateTime DateTime_ => dateTime; 

    private void Awake()
    {
        Instant = this;
        dateTime = new DateTime(2000, 01, 01, 07, 00, 00);
    }

    /*private void Update()
    {
        time += Time.deltaTime;
        *//*if (isDay)
        {
            DirectSunLight.enabled = true;
            MoonLight.enabled = false;

            y = lightStreightDay.Evaluate(time / dayTime.y);
            DirectSunLight.intensity = y;
            RenderSettings.ambientIntensity = y;
            RenderSettings.reflectionIntensity = y;

            if (time > dayTime.y)
            {
                isDay = false;
                onNight.Invoke();
            }
        }
        else
        {
            DirectSunLight.intensity = 0;

            DirectSunLight.enabled = false;
            MoonLight.enabled = true;

            y = lightStreightDay.Evaluate((time - nightTime.x) / (nightTime.y - nightTime.x));
            MoonLight.intensity = y * 0.2f;
            RenderSettings.ambientIntensity = 0;
            RenderSettings.reflectionIntensity = 0;

            if (time > nightTime.y)
            {
                onSleep.Invoke();
            }

            if (time > 1390)    
            {
                time = dayTime.x;
                onDay.Invoke();
                isDay = true;
                Day += 1;
                SaveManager.Instant.Save();
                Debug.Log("Saved");
            }
        }
        x = time / 1440 * 360;  
        sun.eulerAngles = new Vector3(x, 0, 0);*//*

        //change time
        AmountTime();
        

        *//*time += Time.deltaTime;
        if (isDay)
        {
            DirectSunLight.enabled = true;
            MoonLight.enabled = false;
            y = lightStreightDay.Evaluate(time / dayTime);
            DirectSunLight.intensity = y;
            RenderSettings.ambientIntensity = y;
            RenderSettings.reflectionIntensity = y;
            x = time / dayTime * 180;
            if(time >= dayTime)
            {
                onNight.Invoke();
                x = 180;
                isDay = false;
                time = 0;
            }
        }
        else
        {
            DirectSunLight.intensity = 0;
            DirectSunLight.enabled = false;
            MoonLight.enabled = true;
            y = lightStreightDay.Evaluate(time / nightTime);
            MoonLight.intensity = y * 0.2f;
            x = time / nightTime * 180 + 180;
            RenderSettings.ambientIntensity = 0;
            RenderSettings.reflectionIntensity = 0;
            if (time >= nightTime)
            {
                onDay.Invoke();
                x = 0;
                isDay = true;
                time = 0;
                SaveManager.Instant.Save();
                Debug.Log("Saved");
            }
        }
        sun.eulerAngles = new Vector3(x, 0, 0);*//*


    }

    public void SkipNight()
    {
        time = nightTime.y;
    }
    private void AmountTime()
    {
        *//*Hour =(int)time / 60;
        Min = (int)time % 60;*//*
        dateTime.AddMinutes(time);
    }*/

    public void Load(DayNightSaveFile dnsf)
    {
        time = dnsf.time;
        isDay = dnsf.isDay;
        if (isDay)
        {
            onDay.Invoke();
        }
        else
        {
            onNight.Invoke();
        }
    }
    public void Save(DayNightSaveFile dnsf)
    {
        dnsf.time = time;
        dnsf.isDay = isDay;
    }
}
