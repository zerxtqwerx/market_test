using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class StorageSaveFile : SaveFile
    {
        public Dictionary<int, BoxSaveFile> bsfs = new Dictionary<int, BoxSaveFile>();
    }
}