using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingItemsPanel : MonoBehaviour
{
    public Dictionary<Item, int> itemsToBuy = new Dictionary<Item, int>();
    public Dictionary<Item, BIPItemPanel> BIPpanels = new Dictionary<Item, BIPItemPanel>();
    public GameObject panelPrefab;
    public RectTransform panelsTransform;
    public DropZoneManager dropZone;
    public TMP_Text allCost;
    public float moneyToDeliver = 10;
    private float allCost_money = 0;
    private float y = 0;

    private void OnEnable()
    {
        UpdateAllCostMoney(0);
    }

    public void AddItem(Item item)
    {
        if(itemsToBuy.ContainsKey(item))
        {
            itemsToBuy[item] += 1;
            BIPpanels[item].UpdateCount(itemsToBuy[item]);
            UpdateAllCostMoney(item.BoxCost);
        }
        else
        {
            itemsToBuy.Add(item, 1);
            CreatePanel(item);
            UpdateAllCostMoney(item.BoxCost);
        }
    }

    public void RemoveItem(Item item)
    {
        int i;
        if(itemsToBuy.TryGetValue(item, out i))
        {
            if(i - 1 <= 0)
            {
                itemsToBuy.Remove(item);
                DelitePanel(item);
                UpdateAllCostMoney(-item.BoxCost);
            }
            else
            {
                itemsToBuy[item] -= 1;
                BIPpanels[item].UpdateCount(i - 1);
                UpdateAllCostMoney(-item.BoxCost);
            }
        }
    }

    public void ClearAll()
    {
        itemsToBuy.Clear();
        foreach (var item in BIPpanels)
        {
            Destroy(item.Value.gameObject);
        }
        BIPpanels.Clear();
        panelsTransform.sizeDelta = new Vector2(panelsTransform.sizeDelta.x, 0);
    }

    public void BuyAll()
    {
        if (itemsToBuy.Count <= 0)
            return;
        float cost = 0;
        List<Item> ToBuyItems = new List<Item>();
        foreach (var item in itemsToBuy)
        {
            cost += item.Value * item.Key.BoxCost;
            for(int i=0;i<item.Value;i++)
                ToBuyItems.Add(item.Key);
        }
        if(MoneyManager.IsCanDecreaseMoney(cost + moneyToDeliver))
        {
            string s = "";
            if(dropZone.DropByTime(ToBuyItems.ToArray(), 45, out s))
            {
                MoneyManager.ChangeMoney(-(cost + moneyToDeliver));
                ResetAllCostMoney();
                PrintError("Спасибо за покупку!");
                ClearAll();
            }
            else
            {
                PrintError(s);
            }
        }
        else
        {
            PrintError("Недостаточно денег для покупки");
        }
    }

    private void CreatePanel(Item item)
    {
        if(y==0)
            y = panelPrefab.GetComponent<RectTransform>().sizeDelta.y;
        BIPItemPanel bip = Instantiate(panelPrefab, panelsTransform).GetComponent<BIPItemPanel>();
        bip.Setup(this, item);
        BIPpanels.Add(item, bip);
        panelsTransform.sizeDelta += new Vector2(0, y);
    }

    private void DelitePanel(Item item)
    {
        panelsTransform.sizeDelta -= new Vector2(0, y);
        Destroy(BIPpanels[item].gameObject);
        BIPpanels.Remove(item);
    }

    private void UpdateAllCostMoney(float m)
    {
        allCost_money += m;
        allCost.text = allCost_money + " $ (" + moneyToDeliver + " $ за доставку)";
    }

    private void ResetAllCostMoney()
    {
        allCost_money = 0;
    }

    private void PrintError(string err)
    {
        allCost.text = err;
    }
}
