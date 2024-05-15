using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatalogGrocery : MonoBehaviour
{
    public BuyingItemsPanelGrocery bip;
    public GameObject itemPanelPrefab;
    public RectTransform itemPanelParent;
    public List<ItemPanelGrocery> allPanels;
    private float y = 0;

    private void Start()
    {
        y = itemPanelPrefab.GetComponent<RectTransform>().sizeDelta.y;
        AddAllItems("grocery");
    }

    private void AddAllItems()
    {
        StorageInfo[] items = ItemsBase.GetStorages();
        GameObject o;
        ItemPanelGrocery ip;
        float transformY = 0;
        foreach (var item in items)
        {
            o = Instantiate(itemPanelPrefab, itemPanelParent);
            ip = o.GetComponent<ItemPanelGrocery>();
            ip.Setup(this, item);
            allPanels.Add(ip);
            transformY += y;
        }
        itemPanelParent.sizeDelta = new Vector2(itemPanelParent.sizeDelta.x, transformY);
    }

    private void AddAllItems(string defaultCategory)
    {
        StorageInfo[] items = ItemsBase.GetStorages();
        GameObject o;
        ItemPanelGrocery ip;
        float transformY = 0;
        foreach (var item in items)
        {
            o = Instantiate(itemPanelPrefab, itemPanelParent);
            ip = o.GetComponent<ItemPanelGrocery>();
            ip.Setup(this, item);
            allPanels.Add(ip);
            if (item.Category == defaultCategory)
            {
                o.SetActive(true);
                transformY += y;
            }
            else
            {
                o.SetActive(false);
            }
        }
        itemPanelParent.sizeDelta = new Vector2(itemPanelParent.sizeDelta.x, transformY);
    }

    public void SelectItemsByCategory(string category)
    {
        float transformY = 0;
        foreach (var item in allPanels)
        {
            if(item.item.Category == category)
            {
                item.gameObject.SetActive(true);
                transformY += y;
            }
            else
            {
                item.gameObject.SetActive(false);
            }
        }
        itemPanelParent.sizeDelta = new Vector2(itemPanelParent.sizeDelta.x, transformY);
    }

    public void AddToBasket(StorageInfo item)
    {
        bip.AddItem(item);
    }
}
