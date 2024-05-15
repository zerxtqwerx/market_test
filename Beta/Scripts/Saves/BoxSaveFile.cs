using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class BoxSaveFile : SaveFile
    {
        public Vector3 position;
        public Vector3 rotationEuler;
        public string id;
        public string status;
        public bool isEmpty;
    }
}
