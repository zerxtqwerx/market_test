using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsBase : MonoBehaviour
{
    public static ItemsBase Instant;
    public List<Item> items;
    public List<StorageInfo> storages;
    private void Awake()
    {
        Instant = this;
    }

    public static Item[] GetItems()
    {
        return Instant.items.ToArray();
    }

    public static Item GetItem(string id)
    {
        return Instant.items.Find(x => x.Id == id);
    }

    public static StorageInfo[] GetStorages()
    {
        return Instant.storages.ToArray();
    }

    public static StorageInfo GetStorage(string id)
    {
        return Instant.storages.Find(x => x.Id == id);
    }
}
