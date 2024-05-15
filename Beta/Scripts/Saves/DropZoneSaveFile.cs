using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class DropZoneSaveFile : SaveFile
    {
        public int id = -1;
        public bool isActive = false;
    }
}