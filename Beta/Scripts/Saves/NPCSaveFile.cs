using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class NPCSaveFile : SaveFile
    {
        public Vector3 position;
        public Vector3 eulerRotation;
        public List<ItemGameObjectSaveFile> inventory = new List<ItemGameObjectSaveFile>();
        public Vector3 timed_position;
        public bool die;
        public float deathTimer;
        public int currentSate;
        public List<string> itemsList = new List<string>();
        public float timer;
        public int skin_id;
    }
}