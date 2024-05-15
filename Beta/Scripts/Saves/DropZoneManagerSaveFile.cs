using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class DropZoneManagerSaveFile : SaveFile
    {
        public List<DropZoneSaveFile> dzsfs = new List<DropZoneSaveFile>();
        public List<DropZoneTimerSaveFile> dztsfs = new List<DropZoneTimerSaveFile>();
    }
}