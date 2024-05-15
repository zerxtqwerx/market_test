using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingList : MonoBehaviour
{
    [Header("Максимальное количество продуктов в списке")]
    [SerializeField] private int maxProductsNumber;

    [Header("Шанс покупки")]
    [SerializeField] private float chance;

    [Header("Уменьшение спроса товара при покупке")]
    [SerializeField] private int reducingDemand;

    private int productsNumber;
    List<string> shoppingList;
    List<Vector3> productsCoord;
    private LevelProductsList lpl;

    void Start()
    {
        try
        {
            lpl = FindObjectOfType<LevelProductsList>();
            productsNumber = Random.Range(1, maxProductsNumber);
            //CreateList();
        }
        catch 
        {
            Debug.LogError("shoppinglist/start");
        }
    }

    void CreateList()
    {
        List<string> products = lpl.FindAllProducts();
        Debug.Log(products.Count);
        if (products.Count < productsNumber)
        {
            productsNumber = products.Count;
        }


        for (int i = 0; i < productsNumber; i++)
        {
            int index = Random.Range(0, products.Count);
            shoppingList.Add(products[index]);
            //products.RemoveAt(index);
        }

        Debug.Log(shoppingList);
    }

    public List<string> GetList()
    {
        return shoppingList;
    }
}
