using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour
{
    [SerializeField] private List<Item> itemsList;

    public Item GetItem(string id)
    {
        foreach (Item item in itemsList)
        {
            if(item.Id  == id)
            {
                return item;
            }
        }
        return null;
    }
}
