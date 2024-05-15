using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instant;
    public Dictionary<string, int> storagesBought = new Dictionary<string, int>();
    private void Awake()
    {
        Instant = this;
    }

    private void Start()
    {
        StorageInfo[] si = ItemsBase.GetStorages();
        foreach (var item in si)
        {
            storagesBought.Add(item.Id, 0);
        }
    }

    public void AddStorages(StorageInfo[] si)
    {
        foreach (var item in si)
        {
            if(storagesBought.ContainsKey(item.Id))
            {
                storagesBought[item.Id]++;
            }
        }
    }

    public static int GetCountOfStorage(string s)
    {
        int o = 0;
        Instant.storagesBought.TryGetValue(s, out o);
        return o;
    }

    public static void ChangeCountOfStorages(string id, int c)
    {
        if(Instant.storagesBought.ContainsKey(id))
        {
            Instant.storagesBought[id] += c;
        }
    }
}
