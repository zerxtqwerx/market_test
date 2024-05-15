using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class NPCLoaderSaveFile : SaveFile
    {
        public Vector3 position;
        public Vector3 eulerRotation;
        public BoxSaveFile inventoryObj;
        public int currentState;
        public int dayCount;
    }
}