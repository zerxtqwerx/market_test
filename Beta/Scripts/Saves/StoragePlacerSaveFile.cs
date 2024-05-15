using System;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class StoragePlacerSaveFile : SaveFile
    {
        public int storageId;
        public string groceryId = "";
        public GroceryRackSaveFile grsf;
    }
}