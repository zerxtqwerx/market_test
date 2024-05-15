using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public class NPCCashierSaveFile : SaveFile
    {
        public Vector3 position;
        public Vector3 eulerRotation;
        public int currentState;
        public int dayCount;
    }
}
