using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Storage : PlaceZone
{
    public static int id = 0;
    public int myId = -1;
    public Transform point_to_go;
    private void Awake()
    {
        /*myId = id;
        id++;*/
    }
    private void Start()
    {
        GameManager.Instance.storages.Add(this);
    }

    public bool PlaceBox(BoxGameObject bgo)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Place place))
            {
                if(place.IsEmpty())
                {
                    bgo.transform.parent = place.transform;
                    bgo.transform.localPosition = Vector3.zero;
                    bgo.transform.localRotation = Quaternion.identity;
                    bgo.OnPlaced();
                    return true;
                }
            }
        }
        return false;
    }

    public void Save(StorageSaveFile ssf)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<Place>(out Place p))
            {
                if (!p.IsEmpty())
                {
                    BoxSaveFile bsf = new BoxSaveFile();
                    transform.GetChild(i).GetChild(0).GetComponent<BoxGameObject>().Save(bsf);
                    ssf.bsfs.Add(i, bsf);
                }
            }
        }
    }

    public void Load(StorageSaveFile ssf)
    {
        for (int j = 0; j < transform.childCount; j++)
        {
            if (transform.GetChild(j).TryGetComponent(out Place p))
            {
                p.Clear();
            }
        }
        foreach (var item in ssf.bsfs)
        {
            Item i = ItemsBase.GetItem(item.Value.id);
            if (i != null)
            {
                Transform t = transform.GetChild(item.Key);
                GameObject o = Instantiate(i.BoxesPrefab);
                o.GetComponent<BoxGameObject>().Load(item.Value, i);
                o.transform.SetParent(t);
                o.GetComponent<Rigidbody>().isKinematic = true;
                o.GetComponent<BoxCollider>().isTrigger = true;
                o.transform.localPosition = Vector3.zero;
                o.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
