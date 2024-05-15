using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoragePlacer : MonoBehaviour
{
    public int myId;
    public BoxCollider bc;
    public GameObject visibleBox;
    public PlaceZone placeZoneIn;

    public void EditMode(bool t)
    {
        bc.enabled = t;
        visibleBox.SetActive(t);
    }

    public void Place(StorageInfo si, bool saveLoad = false)
    {
        if (placeZoneIn != null)
        {
            if(!saveLoad)
                Inventory.ChangeCountOfStorages(placeZoneIn.storageInfo.Id, 1);
            Destroy(placeZoneIn.gameObject);
        }
        GameObject o = Instantiate(si.Prefab, transform);
        o.transform.localPosition = Vector3.zero;
        o.transform.localEulerAngles = Vector3.zero;
        placeZoneIn = o.GetComponent<PlaceZone>();
        if (!saveLoad)
            Inventory.ChangeCountOfStorages(placeZoneIn.storageInfo.Id, -1);
    }

    public void Save(StoragePlacerSaveFile spsf)
    {
        spsf.storageId = myId;
        if(placeZoneIn != null)
        {
            spsf.groceryId = placeZoneIn.storageInfo.Id;
            if(placeZoneIn is GroceryRack)
            {
                GroceryRackSaveFile grsf = new GroceryRackSaveFile();
                ((GroceryRack)placeZoneIn).Save(grsf);
                spsf.grsf = grsf;
            }
        }
    }

    public void Load(StoragePlacerSaveFile spsf)
    {
        if(spsf.groceryId != "")
        {
            Place(ItemsBase.GetStorage(spsf.groceryId), true);
            if(placeZoneIn != null && placeZoneIn is GroceryRack)
            {
                ((GroceryRack)placeZoneIn).Load(spsf.grsf);
            }
        }
    }
}
