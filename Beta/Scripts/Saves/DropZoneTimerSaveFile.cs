using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class DropZoneTimerSaveFile : SaveFile
    {
        public float time = -1;
        public int dropZoneId = -1;
        public List<string> itemsId = new List<string>();
    }
}
