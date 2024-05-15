using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UISettingsPanel : MonoBehaviour
{
    public Slider musicVolume;
    public TMP_Dropdown dropdown;

    public void Save()
    {
        SettingsManager.SaveSettings(musicVolume.value, dropdown.value);
    }
}
