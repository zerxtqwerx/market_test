using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BuyingItemsPanelGrocery : MonoBehaviour
{
    public Dictionary<StorageInfo, int> itemsToBuy = new Dictionary<StorageInfo, int>();
    public Dictionary<StorageInfo, BIPItemPanelGrocery> BIPpanels = new Dictionary<StorageInfo, BIPItemPanelGrocery>();
    public GameObject panelPrefab;
    public RectTransform panelsTransform;
    public TMP_Text allCost;
    public float moneyToDeliver = 10;
    private float allCost_money = 0;
    private float y = 0;

    private void OnEnable()
    {
        UpdateAllCostMoney(0);
    }

    public void AddItem(StorageInfo item)
    {
        if(itemsToBuy.ContainsKey(item))
        {
            itemsToBuy[item] += 1;
            BIPpanels[item].UpdateCount(itemsToBuy[item]);
            UpdateAllCostMoney(item.Cost);
        }
        else
        {
            itemsToBuy.Add(item, 1);
            CreatePanel(item);
            UpdateAllCostMoney(item.Cost);
        }
    }

    public void RemoveItem(StorageInfo item)
    {
        int i;
        if(itemsToBuy.TryGetValue(item, out i))
        {
            if(i - 1 <= 0)
            {
                itemsToBuy.Remove(item);
                DelitePanel(item);
                UpdateAllCostMoney(-item.Cost);
            }
            else
            {
                itemsToBuy[item] -= 1;
                BIPpanels[item].UpdateCount(i - 1);
                UpdateAllCostMoney(-item.Cost);
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
        List<StorageInfo> ToBuyItems = new List<StorageInfo>();
        foreach (var item in itemsToBuy)
        {
            cost += item.Value * item.Key.Cost;
            for(int i=0;i<item.Value;i++)
                ToBuyItems.Add(item.Key);
        }
        if(MoneyManager.IsCanDecreaseMoney(cost + moneyToDeliver))
        {
            Inventory.Instant.AddStorages(ToBuyItems.ToArray());
            MoneyManager.ChangeMoney(-(cost + moneyToDeliver));
            ResetAllCostMoney();
            PrintError("Спасибо за покупку!");
            ClearAll();
        }
        else
        {
            PrintError("Недостаточно денег для покупки");
        }
    }

    private void CreatePanel(StorageInfo item)
    {
        if(y==0)
            y = panelPrefab.GetComponent<RectTransform>().sizeDelta.y;
        BIPItemPanelGrocery bip = Instantiate(panelPrefab, panelsTransform).GetComponent<BIPItemPanelGrocery>();
        bip.Setup(this, item);
        BIPpanels.Add(item, bip);
        panelsTransform.sizeDelta += new Vector2(0, y);
    }

    private void DelitePanel(StorageInfo item)
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
