using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetter : MonoBehaviour
{
    public static PlayerGetter Instant;
    public Transform playerCamera;
    public Transform hand;
    public LayerMask lm;
    public float maxDist = 10;
    private BoxGameObject bgoPickable;
    private BoxGameObject inHandBox;
    public BoxGameObject InHandBox => inHandBox;
    private GroceryRack grocery;
    private Storage storage;
    private DropFromCar dropFromCar;
    private GroceryRack groceryToChangePrice;
    private StoragePlacer placeZoneToEdit;
    private Getter getter;

    private void Awake()
    {
        Instant = this;
    }
    private void FixedUpdate()
    {
        int i;
        RaycastHit[] rh = Physics.RaycastAll(playerCamera.transform.position, playerCamera.transform.forward, maxDist, lm);
        UIManager.ClearButtons();
        bgoPickable = null;
        grocery = null;
        storage = null;
        groceryToChangePrice = null;
        dropFromCar = null;
        placeZoneToEdit = null;
        getter = null;
        if (inHandBox != null)
        {
            UIManager.SetDropButton(true);
            if (inHandBox.isEmpty())
            {
                UIManager.SetDeliteButton(true);
            }
        }
        /*if(CharacterControl.Instance.InGetter != null)
        {
            UIManager.SetExitCashRegisterButton(true);
        }*/
        if (rh.Length > 0)
        {
            rh = SortRaycastHits(rh);
            for (i = 0; i < rh.Length; i++)
            {
                if (rh[i].collider.tag == "Untagged")
                    break;
                if (rh[i].collider.tag == "Pickable" && inHandBox == null && rh[i].collider.GetComponent<BoxGameObject>().status != "InNPCHands" && rh[i].collider.GetComponent<BoxGameObject>().status != "InCar")
                {
                    if (bgoPickable == null)
                    {
                        bgoPickable = rh[i].collider.GetComponent<BoxGameObject>();
                        UIManager.SetPickButton(true);
                    }
                }
                if(rh[i].collider.tag == "Car" && rh[i].collider.GetComponent<DropFromCar>().dz.IsNotEmpty())
                {
                    if(dropFromCar == null)
                    {
                        dropFromCar = rh[i].collider.GetComponent<DropFromCar>();
                        UIManager.SetCarDropButton(true);
                    }
                }
                if(rh[i].collider.tag == "Zone")
                {
                    if(groceryToChangePrice == null)
                    {
                        groceryToChangePrice = rh[i].collider.GetComponent<GroceryRack>();
                        if (!groceryToChangePrice.isFullEmpty())
                        {
                            UIManager.SetPriceChangeButton(true);
                        }
                    }
                }
                if (inHandBox != null)
                {
                    if(!inHandBox.isEmpty())
                    {
                        if (rh[i].collider.tag == "Zone")
                        {
                            if (grocery == null)
                            {
                                grocery = rh[i].collider.GetComponent<GroceryRack>();
                                UIManager.SetLoadButton(true);
                            }
                        }
                        if (rh[i].collider.tag == "Storage")
                        {
                            if (storage == null)
                            {
                                storage = rh[i].collider.GetComponent<Storage>();
                                UIManager.SetLoadButton(true);
                            }
                        }
                    }
                }
                if (rh[i].collider.tag == "CashRegister")
                {
                    /*if(getter == null && CharacterControl.Instance.InGetter == null)
                    {
                        getter = rh[i].collider.GetComponent<Getter>();
                        UIManager.SetEnterCashRegisterButton(true);
                    }*/
                }
            }
        }
    }

    private RaycastHit[] SortRaycastHits(RaycastHit[] rh)
    {
        List<RaycastHit> rhl = new List<RaycastHit>(rh);
        rhl.Sort((a, b) => a.distance.CompareTo(b.distance));
        return rhl.ToArray();
    }

    public void PickSelectedItem()
    {
        PickItem(bgoPickable);
    }

    public void PickItem(BoxGameObject bgo)
    {
        if(inHandBox == null && bgo != null)
        {
            inHandBox = bgo;
            inHandBox.GetComponent<Rigidbody>().isKinematic = true;
            inHandBox.GetComponent<Collider>().isTrigger = true;
            inHandBox.transform.parent = hand.transform;
            inHandBox.transform.rotation = hand.rotation;
            inHandBox.transform.localPosition = new Vector3(0, -0.6f, 0);
            inHandBox.OnPickupByPlayer();
            inHandBox.gameObject.layer = 11;
            inHandBox.SetLayer(11);
        }
    }

    public void LoadSelected()
    {
        if(grocery != null && inHandBox != null)
        {
            LoadToGrocery(inHandBox, grocery);
        }
        else
        if(storage != null && inHandBox != null)
        {
            if(LoadToStorage(inHandBox, storage))
            {
                inHandBox = null;
            }
        }
    }

    public bool LoadToGrocery(BoxGameObject bgo, GroceryRack gr)
    {
        return gr.Fill(bgo);
    }

    public bool LoadToStorage(BoxGameObject bgo, Storage st)
    {
        if(st.PlaceBox(bgo))
        {
            bgo.SetLayer(6);
            return true;
        }
        return false;
    }

    public void Drop()
    {
        if (inHandBox != null)
        {
            inHandBox.transform.SetParent(null);
            inHandBox.GetComponent<Rigidbody>().isKinematic = false;
            inHandBox.GetComponent<BoxCollider>().isTrigger = false;
            inHandBox.OnDrop();
            inHandBox.SetLayer(6);
            inHandBox.transform.position = transform.position;
            inHandBox.transform.rotation = transform.rotation;
            inHandBox = null;
        }
    }

    public void Delite()
    {
        if (inHandBox != null)
        {
            Destroy(inHandBox.gameObject);
            inHandBox = null;
        }
    }

    public void DropFromCarSelected()
    {
        if(dropFromCar != null)
        {
            dropFromCar.Drop();
        }
    }

    public void OpenPriceChanger()
    {
        if(groceryToChangePrice != null)
        {
            UIManager.Instant.uiCostChanger.Open(groceryToChangePrice);
        }
    }

    public void OpenEditModeSelected()
    {
        if(placeZoneToEdit != null)
        {
            UIManager.Instant.storageEditUI.Open(placeZoneToEdit);
        }
    }

    public void OpenCashRegister()
    {
        /*if(getter != null)
        {
            CharacterControl.Instance.DisableControll();
            getter.ControllByPlayer();
        }*/
    }

    public void CloseCashRegister()
    {
        /*if(CharacterControl.Instance.InGetter != null)
        {
            CharacterControl.Instance.EnableControll();
            CharacterControl.Instance.InGetter.UnControllByPlayer();
        }*/
    }
}
