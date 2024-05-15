using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class GroceryRackSaveFile : SaveFile
    {
        public Dictionary<int, ItemGameObjectSaveFile> iosf = new Dictionary<int, ItemGameObjectSaveFile>();
        public float procent;
    }
}