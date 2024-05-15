using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BoxGameObject : MonoBehaviour
{
    [SerializeField] private NavMeshObstacle nmo;
    [SerializeField] private Item _item;
    public Item Item => _item;

    public string status = "InCar";
    public bool isEmpty()
    {
        if (transform.childCount > 1)
        {
            return false;
        }

        return true;
    }

    public void OnPickupByPlayer()
    {
        status = "OnPlayer";
        GameManager.Instance.notUnloadBoxesGameObjects.Remove(this);
    }
    
    public void OnGetOutFromCar()
    {
        status = "RedyForLoad";
        GameManager.Instance.notUnloadBoxesGameObjects.Add(this);
    }

    public void OnPickedUpByNpc()
    {
        status = "InNPCHands";
        GameManager.Instance.notUnloadBoxesGameObjects.Remove(this);
    }

    public void OnPlaced()
    {
        status = "Placed";
        GameManager.Instance.boxesInStorage.Add(this);
    }

    public void OnGet()
    {
        GetComponent<Rigidbody>().isKinematic = true;
        nmo.enabled = false;
        GameManager.Instance.boxesInStorage.Remove(this);
    }

    public void OnDrop()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        status = "Droped";
        nmo.enabled = true;
        GameManager.Instance.notUnloadBoxesGameObjects.Add(this);
    }

    public void Save(BoxSaveFile bsf)
    {
        bsf.position = transform.position;
        bsf.rotationEuler = transform.eulerAngles;
        bsf.id = Item.Id;
        bsf.status = status;
        bsf.isEmpty = isEmpty();
    }

    public void Load(BoxSaveFile bsf, Item i)
    {
        transform.position = bsf.position;
        transform.eulerAngles = bsf.rotationEuler;
        _item = i;
        status = bsf.status;
        if(status == "RedyForLoad")
        {
            OnGetOutFromCar();
        }
        if (bsf.isEmpty)
        {
            foreach (ItemObj p in transform.GetComponentsInChildren<ItemObj>())
            {
                Destroy(p.gameObject);
            }
        }
    }

    public void SetLayer(int l)
    {
        gameObject.layer = l;
        for(int i=0;i<transform.childCount;i++)
        {
            transform.GetChild(i).gameObject.layer = l;
        }
    }
}
