using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager
{
    public static float soundsVolume = 1;
    public static int performancePresset = 1;

    public static void SaveSettings(float volume, int performance)
    {
        soundsVolume = volume;
        performancePresset = performance;
        QualitySettings.SetQualityLevel(performance);
    }
}
