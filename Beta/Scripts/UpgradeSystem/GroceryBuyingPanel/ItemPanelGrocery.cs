using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPanelGrocery : MonoBehaviour
{
    public StorageInfo item;
    public Image icon;
    public TMP_Text title;
    public TMP_Text cost;
    private KatalogGrocery katalog;

    public void Setup(KatalogGrocery k,StorageInfo item)
    {
        katalog = k;
        this.item = item;
        icon.sprite = item.Icon;
        title.text = item.ItemName;
        cost.text = item.Cost.ToString();
    }

    public void AddToBasket()
    {
        katalog.AddToBasket(item);
    }
}
