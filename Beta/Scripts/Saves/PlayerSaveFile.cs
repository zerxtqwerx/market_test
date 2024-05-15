using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class PlayerSaveFile : SaveFile
    {
        public Vector3 position;
        public Vector3 rotationEuler;
        public BoxSaveFile holdItemId;
        public Vector3 localRotationEulerCamera;
    }
}