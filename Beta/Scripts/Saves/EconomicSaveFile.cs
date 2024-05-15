using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SaveSystem
{
    [Serializable]
    public class EconomicSaveFile : SaveFile
    {
        public float money = -1;
    }
}
