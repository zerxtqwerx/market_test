using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StorageEditUI : MonoBehaviour
{
    public StoragePlacer currentStoragePlacer;
    public TMP_Text fruits;
    public TMP_Text vegetables;
    public StorageInfo si1;
    public StorageInfo si2;
    private int item1Count = 0;
    private int item2Count = 0;
    public void Open(StoragePlacer sp)
    {
        currentStoragePlacer = sp;
        gameObject.SetActive(true);
        UpdateCount();
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void SetFruits()
    {
        if (item1Count > 0)
        {
            currentStoragePlacer.Place(si1);
        }
        UpdateCount();
    }

    public void SetVegitables()
    {
        if (item2Count > 0)
        {
            currentStoragePlacer.Place(si2);
        }
        UpdateCount();
    }

    public void UpdateCount()
    {
        item1Count = Inventory.GetCountOfStorage(si1.Id);
        item2Count = Inventory.GetCountOfStorage(si2.Id);
        fruits.text = item1Count.ToString();
        vegetables.text = item2Count.ToString();
    }
}
