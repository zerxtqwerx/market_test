using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class MainSaveFile
    {
        public int scene_id = -1;
        public PlayerSaveFile psf;
        public EconomicSaveFile esf;
        public DayNightSaveFile dnsf;
        public DropZoneManagerSaveFile dzmsf;
        public List<BoxSaveFile> boxSaveFiles = new List<BoxSaveFile>();
        public List<NPCSaveFile> npcSaveFiles = new List<NPCSaveFile>();
        public List<ItemGameObjectSaveFile> dropedItemsSaveFiles = new List<ItemGameObjectSaveFile>();
        public Dictionary<int, GroceryRackSaveFile> groceryRackFiles = new Dictionary<int, GroceryRackSaveFile>();
        public Dictionary<int, StorageSaveFile> storeageSaveFiles = new Dictionary<int, StorageSaveFile>();
        public Dictionary<int, NPCLoaderSaveFile> npcLoaderSaveFile = new Dictionary<int, NPCLoaderSaveFile>();
        public Dictionary<int, NPCCashierSaveFile> npcCashierSaveFile = new Dictionary<int, NPCCashierSaveFile>();
    }
}
