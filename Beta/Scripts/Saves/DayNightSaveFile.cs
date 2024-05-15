using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class DayNightSaveFile : SaveFile
    {
        public float time;
        public bool isDay;
    }
}
