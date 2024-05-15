using SaveSystem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemGameObject : MonoBehaviour
{
    public Rigidbody rig;
    public Item item;
    public PlaceZone placeZone;

    public string Id;
    public string Trader;
    public float Price;

    public int id = 0;
    public float itemCost = 100;
    public string status = "InStorage";

    private bool drag = false;
    private int identifier;

    void Start()
    {
        try 
        {
            identifier = FindObjectOfType<LevelProductsList>().AddProduct(gameObject);
        }
        catch
        {
            Debug.LogError("igo/start");
        }
    }

    public void OnDestroy()
    {
        bool n = FindObjectOfType<LevelProductsList>().RemoveProduct(identifier);
    }

    public void OnNormalSpawning()
    {
        GameManager.Instance.getObjects.Add(this);
    }

    public void OnGet()
    {
        GameManager.Instance.getObjects.Remove(this);
        //placeZone.itemsIn.Remove(this);
        status = "InNPCHands";
    }

    public void OnDrop()
    {
        rig.isKinematic = false;
        status = "Droping";
        Invoke("Destr", 2);
    }

    public void OnDropToDrag()
    {
        rig.isKinematic = false;
        status = "Dragable";
    }

    public void Destr()
    {
        //MoneyManager.Money += item.ItemCost * (itemCost / 100);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "CashRegisterDestroyer")
        {
            Destr();
        }
    }

    public void Save(ItemGameObjectSaveFile igosf)
    {
        igosf.position = transform.position;
        igosf.eulerRotation = transform.eulerAngles;
        igosf.id = item.Id;
        //igosf.itemCost = itemCost;
        igosf.status = status;
    }

    public void Load(ItemGameObjectSaveFile igosf, Item i)
    {
        transform.position = igosf.position;
        transform.eulerAngles = igosf.eulerRotation;
        item = i;
        itemCost = igosf.itemCost;
        status = igosf.status;
        if(status == "Droping")
        {
            OnDrop();
        }
    }

    private void Update()
    {
        /*if (CharacterControl.Instance.InGetter != null && drag)
        {
            var v3 = Input.mousePosition;
            v3.z = 1.0f;
            v3 = CharacterControl.Instance.InGetter.getterCamera.GetComponent<Camera>().ScreenToWorldPoint(v3);
            transform.position = v3;
        }*/
    }

    private void OnMouseDown()
    {
        /*if (CharacterControl.Instance.InGetter != null)
        {
            rig.isKinematic = true;
            drag = true;
        }*/
    }

    private void OnMouseUp()
    {
        /*if (CharacterControl.Instance.InGetter != null)
        {
            rig.isKinematic = false;
            drag = false;
        }*/
    }
}
