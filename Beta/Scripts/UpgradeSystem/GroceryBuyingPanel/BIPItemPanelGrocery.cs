using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BIPItemPanelGrocery : MonoBehaviour
{
    private BuyingItemsPanelGrocery bip;
    public StorageInfo item;
    public int count;
    public TMP_Text title;
    public TMP_Text cost;
    public TMP_Text countText;
    public void Setup(BuyingItemsPanelGrocery b, StorageInfo item)
    {
        bip = b;
        this.item = item;
        count = 1;
        title.text = item.ItemName;
        cost.text = item.Cost + " $";
        countText.text = count.ToString();
    }

    public void UpdateCount(int c)
    {
        count = c;
        cost.text = item.Cost * c + " $";
        countText.text = count.ToString();
    }

    public void Add()
    {
        bip.AddItem(item);
    }

    public void Remove()
    {
        bip.RemoveItem(item);
    }
}
