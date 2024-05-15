using System.Collections.Generic;
using UnityEngine;

public class LevelProductsList : MonoBehaviour
{
    private class Product
    {
        public GameObject prefab;
        public int id;

        public Product(GameObject go_, int id_)
        {
            prefab = go_;
            id = id_;
        }
    }
    int n;
    List<Product> levelProductsList;
    void Start()
    {
        levelProductsList = new List<Product>();
        //Debug.Log(levelProductsList.Count);
        n = 0;
    }

    public int AddProduct(GameObject go)
    {
        try
        {
            Product product = new Product(go, n);
            levelProductsList.Add(product);
            n++;
            if (levelProductsList != null)
            {
                Debug.Log("lpl " + levelProductsList.Count);
            }

            return n;
        }
        catch
        {
            Debug.LogError("LevelProductList/AddProduct");
            return -1;
        }
    }

    public bool RemoveProduct(int id_)
    {
        if (levelProductsList != null)
        {
            try
            {
                foreach (Product product in levelProductsList)
                {
                    if (product.id == id_)
                    {
                        levelProductsList.Remove(product);
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                Debug.LogError("LevelProductList/RemoveProduct");
                return false;
            }
        }
        return false;
    }
    public string FindTraderWithLowestPrice(string id)
    {
        string trader = null;
        int index = FindCheapestProduct(id);
        var igo = levelProductsList[index].prefab.GetComponent<ItemGameObject>();
        return igo.Trader;
    }

    public List<string> FindAllTraders()
    {
        if (levelProductsList != null)
        {
            List<string> tradersList = new List<string>();
            foreach (Product product in levelProductsList)
            {
                var igo = product.prefab.GetComponent<ItemGameObject>();
                if (!tradersList.Contains(igo.Trader))
                {
                    tradersList.Add(igo.Trader);
                }
            }
            return tradersList;
        }
        return null;
    }

    public List<string> FindAllProducts()
    {
        if (levelProductsList != null)
        {
            List<string> productsList = new List<string>();
            foreach (Product product in levelProductsList)
            {
                var igo = product.prefab.GetComponent<ItemGameObject>();
                if (!productsList.Contains(igo.item.Id))
                {
                    productsList.Add(igo.Id);
                }
            }
            Debug.Log("lpl " +  levelProductsList.Count);
            return productsList;
        }
        return null;
    }

    public List<string> FindProductsByTrader(string trader)
    {
        if (levelProductsList != null)
        {
            List<string> productsList = new List<string>();
            foreach (Product product in levelProductsList)
            {
                var igo = product.prefab.GetComponent<ItemGameObject>();
                if (igo.Trader == trader)
                {
                    productsList.Add(igo.item.Id);
                }
            }
            return productsList;
        }
        return null;
    }

    public Vector3 FindCoordCheapestProduct(string product)
    {
        int index = FindCheapestProduct(product);
        return levelProductsList[index].prefab.transform.position;

    }

    private int FindCheapestProduct(string id)
    {
        float lowestPrice = 1000;
        int index = -1;
        foreach (Product product in levelProductsList)
        {
            var igo = product.prefab.GetComponent<ItemGameObject>();
            if (igo.item.Id == id && igo.Price < lowestPrice)
            {
                index = levelProductsList.IndexOf(product);
            }
        }
        return index;
    }
}