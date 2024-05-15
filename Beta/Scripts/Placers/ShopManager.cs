using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public List<StoragePlacer> storagePlacers;
    public List<Storage> storages;
    public List<Getter> getters;
    public void Save(ShopSaveFile ssf)
    {
        foreach (var item in storagePlacers)
        {
            StoragePlacerSaveFile spsf = new StoragePlacerSaveFile();
            item.Save(spsf);
            ssf.spsfs.Add(item.myId, spsf);
        }

        foreach (var item in storages)
        {
            StorageSaveFile stsf = new StorageSaveFile();
            item.Save(stsf);
            ssf.ssfs.Add(item.myId, stsf);
        }
    }

    public void Load(ShopSaveFile ssf)
    {
        foreach(var item in ssf.spsfs)
        {
            var i = storagePlacers.Find(x => x.myId == item.Key);
            if (i != null)
                i.Load(item.Value);
        }

        foreach (var item in ssf.ssfs)
        {
            var i = storages.Find(x => x.myId == item.Key);
            if (i != null)
                i.Load(item.Value);
        }
    }

    public void ToEditMode(bool to)
    {
        storagePlacers.ForEach(x => x?.EditMode(to));
    }
}
