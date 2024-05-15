/*using System.Collections.Generic;
using UnityEngine;
using System;

public class AddingProducts : MonoBehaviour
{
    [SerializeField] private List<ItemGameObject> unlockedProducts;

    string lastId;
    string lastTrader;
    float lastPrice;
    GameObject = lastPrefab;

    public Action OnAddingProducts;

    public void AddProducts(string id, string trader, float price, int count)
    {
        foreach(ItemGameObject igo in unlockedProducts)
        {
            Item item = igo.item;
            if(item.Id == id)
            {
                for (int i = 0; i!= count; i++) 
                {
                    if (InstantiateObject(item.ItemPrefab))
                    {
                        try
                        {
                            lastItemGameObject = igo;
                            lastTrader = trader;
                            lastPrice = price;
                            OnAddingProducts?.Invoke();
                        }
                        catch
                        {
                            Debug.LogError("AddingProducts/AddProducts()");
                        }
                    } 
                }
            }
        }
    }

    public void TargetData(string id_, string trader_, float price_, GameObject prefab_)
    {
        id_ = id;
        trader_ = lastTrader;
        price_ = lastPrice;
        prefab_ = lastPrefab;
    }

    private bool InstantiateObject(GameObject object_)
    {
        try
        {
            var obj = Instantiate(object_, position, Quaternion.identity);
            lastPrefab = obj;
            productOnScene.Add(obj);
            return true;
        }
        catch { return false; }
        
    }

    public bool RemoveObject(string id, string trader)
    {

    }
}
*/