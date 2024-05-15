using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceZone : MonoBehaviour
{
    public StorageInfo storageInfo;
    public List<ItemGameObject> itemsIn = new List<ItemGameObject>();
    [SerializeField] private string _type;

    private int _count;
    public bool isfull;
    public string Type => _type;

    private void Awake()
    {
        _count = transform.childCount;
    }

    public Item GetItem()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Place place))
            {
                if (transform.GetChild(i).childCount > 0)
                {
                    if(transform.GetChild(i).GetChild(0).TryGetComponent(out BoxGameObject bgo))
                    {
                        return bgo.Item;
                    }
                    if (transform.GetChild(i).GetChild(0).TryGetComponent(out ItemGameObject igo))
                    {
                        return igo.item;
                    }
                }
            }
        }
        return null;
    }

    public bool isFull()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent<Place>(out Place place))
            {
                if (transform.GetChild(i).GetComponent<Place>().IsEmpty())
                {
                    return false;
                }
                    
            }
        }

        return true;
    }

    public bool isFullEmpty()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Place place))
            {
                if (transform.GetChild(i).childCount > 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public int emptyCount()
    {
        int emty = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).TryGetComponent(out Place place))
            {
                if (transform.GetChild(i).childCount == 0)
                {
                    emty++;
                }
            }
        }
        return emty;
    }

    public virtual void OnStorageUpdate()
    {

    }
}
