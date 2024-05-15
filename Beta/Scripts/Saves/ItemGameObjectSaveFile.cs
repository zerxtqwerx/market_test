using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class ItemGameObjectSaveFile
    {
        public Vector3 position;
        public Vector3 eulerRotation;
        public string id;
        public float itemCost;
        public string status;
    }
}