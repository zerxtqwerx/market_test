using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class ShopSaveFile
    {
        public Dictionary<int, StoragePlacerSaveFile> spsfs = new Dictionary<int, StoragePlacerSaveFile>();
        public Dictionary<int, StorageSaveFile> ssfs = new Dictionary<int, StorageSaveFile>();
    }
}