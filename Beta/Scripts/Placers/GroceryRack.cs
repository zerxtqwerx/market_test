using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroceryRack : PlaceZone
{
    public static int id = 0;
    public float procent = 100;
    public int myId = -1;
    private void Awake()
    {
        /*myId = id;
        id++;*/
    }

    private void Start()
    {
        GameManager.Instance.groceryRacks.Add(this);
    }

    public override void OnStorageUpdate()
    {
        foreach (var item in itemsIn)
        {
            item.itemCost = procent;
        }
        base.OnStorageUpdate();
    }

    public bool Fill(BoxGameObject bgo)
    {
        if (Type != bgo.Item.Category)
            return false;
        Item ii = GetItem();
        if(ii != null)
        {
            if (ii.Id != bgo.Item.Id)
                return false;
        }
        if (emptyCount() < bgo.Item.CountInBox)
            return false;
        int i = 0;
        for (int j = 0; j < transform.childCount && i < bgo.Item.CountInBox; j++)
        {
            if(transform.GetChild(j).GetComponent<Place>()!= null && transform.GetChild(j).GetComponent<Place>().IsEmpty())
            {
                GameObject go = Instantiate(bgo.Item.ItemPrefab);
                go.transform.position = transform.GetChild(j).position;
                go.transform.parent = transform.GetChild(j);
                transform.GetComponent<PlaceZone>().itemsIn.Add(go.GetComponent<ItemGameObject>());
                go.GetComponent<ItemGameObject>().placeZone = this;
                go.GetComponent<ItemGameObject>().OnNormalSpawning();
                transform.GetChild(j).GetComponent<Place>().SetEmpty(false);
                OnStorageUpdate();
                i++;
            }
        }

        foreach (ItemObj p in bgo.transform.GetComponentsInChildren<ItemObj>())
        {
            Destroy(p.gameObject);
        }
        return true;
    }

    public void Save(GroceryRackSaveFile grsf)
    {
        grsf.procent = procent;
        for(int i=0;i<transform.childCount;i++)
        {
            if(transform.GetChild(i).TryGetComponent<Place>(out Place p))
            {
                if(!p.IsEmpty())
                {
                    ItemGameObjectSaveFile igosf = new ItemGameObjectSaveFile();
                    transform.GetChild(i).GetChild(0).GetComponent<ItemGameObject>().Save(igosf);
                    grsf.iosf.Add(i, igosf);
                }
            }
        }
    }

    public void Load(GroceryRackSaveFile grsf)
    {
        procent = grsf.procent;
        for (int j = 0; j < transform.childCount;j++)
        {
            if(transform.GetChild(j).TryGetComponent(out Place p))
            {
                p.Clear();
            }
        }
        foreach (var item in grsf.iosf)
        {
            Item i = ItemsBase.GetItem(item.Value.id);
            if(i != null)
            {
                Transform t = transform.GetChild(item.Key);
                GameObject o = Instantiate(i.ItemPrefab);
                o.GetComponent<ItemGameObject>().Load(item.Value, i);
                itemsIn.Add(o.GetComponent<ItemGameObject>());
                o.GetComponent<ItemGameObject>().placeZone = this;
                o.transform.SetParent(t);
                o.transform.localPosition = Vector3.zero;
                //o.transform.localRotation = Quaternion.identity;
                o.GetComponent<ItemGameObject>().OnNormalSpawning();
            }
        }
    }
}
