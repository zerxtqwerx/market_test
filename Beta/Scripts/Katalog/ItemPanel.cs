using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemPanel : MonoBehaviour
{
    public Item item;
    public Image icon;
    public TMP_Text title;
    public TMP_Text cost;
    private Katalog katalog;

    public void Setup(Katalog k,Item item)
    {
        katalog = k;
        this.item = item;
        icon.sprite = item.Icon;
        title.text = item.ItemName;
        cost.text = item.BoxCost.ToString();
    }

    public void AddToBasket()
    {
        katalog.AddToBasket(item);
    }
}
