using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZoneManager : MonoBehaviour
{
    public List<DropZone> dropZones = new List<DropZone>();

    private List<DropZoneTimer> timers = new List<DropZoneTimer>();

    private void Awake()
    {
        for(int i=0;i<dropZones.Count;i++)
        {
            dropZones[i].id = i;
        }
    }
    public bool DropByTime(Item[] items, float time, out string error)
    {
        DropZone dz = null;
        foreach (var item in dropZones)
        {
            if (item.CheckZoneForItems())
            {
                dz = item;
                break;
            }
        }
        if(dz != null)
        {
            if(items.Length > dz.MaxItems)
            {
                error = "Слишком много предметов заказано. Максимум: "+dz.MaxItems;
                return false;
            }
            else
            {
                SetTimer(dz, items, time);
                error = "";
                return true;
            }
        }
        else
        {
            error = "Все зоны доставки заняты";
            return false;
        }
    }

    private void FixedUpdate()
    {
        foreach (var item in timers)
        {
            item.t -= Time.fixedDeltaTime;
            if(item.t <= 0)
            {
                Drop(item.dropZone, item.items);
            }
        }
        timers.RemoveAll(x => x.t <= 0);
    }

    public void SetTimer(DropZone dz, Item[] i, float t)
    {
        dz.mainGameObject.SetActive(false);
        dz.OnDeliver = true;
        timers.Add(new DropZoneTimer(t, dz, i));
    }
    
    public void SetTimersZero()
    {
        foreach (var item in timers)
        {
            item.t = 0;
        }
    }

    public void Drop(DropZone dz, Item[] items)
    {
        dz.mainGameObject.SetActive(true);
        dz.Drop(items);
    }

    public void Save(DropZoneManagerSaveFile dzmsf)
    {
        foreach (var item in dropZones)
        {
            DropZoneSaveFile dzsf = new DropZoneSaveFile();
            dzsf.id = item.id;
            dzsf.isActive = item.dfc.gameObject.activeSelf;
            dzmsf.dzsfs.Add(dzsf);
        }
        foreach (var item in timers)
        {
            DropZoneTimerSaveFile dtsf = new DropZoneTimerSaveFile();
            dtsf.dropZoneId = item.dropZone.id;
            dtsf.time = item.t;
            foreach (var item2 in item.items)
            {
                dtsf.itemsId.Add(item2.Id);
            }
            dzmsf.dztsfs.Add(dtsf);
        }
    }

    public void Load(DropZoneManagerSaveFile dzmsf)
    {
        foreach (var item in dzmsf.dzsfs)
        {
            if(item.id >= 0)
            {
                dropZones[item.id].dfc.gameObject.SetActive(item.isActive);
            }
        }
        foreach (var item in dzmsf.dztsfs)
        {
            if(item.dropZoneId >= 0)
            {
                List<Item> items = new List<Item>();
                foreach (var item2 in item.itemsId)
                {
                    Item i = ItemsBase.GetItem(item2);
                    if (i!=null)
                    {
                        items.Add(i);
                    }
                }
                SetTimer(dropZones[item.dropZoneId], items.ToArray(), item.time);
            }
        }
    }
}

public class DropZoneTimer
{
    public DropZoneTimer(float timer, DropZone dz, Item[] i)
    {
        t = timer;
        dropZone = dz;
        items = i;
    }
    public float t;
    public DropZone dropZone;
    public Item[] items;
}