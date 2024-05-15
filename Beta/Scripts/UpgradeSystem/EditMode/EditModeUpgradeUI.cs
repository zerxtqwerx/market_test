using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EditModeUpgradeUI : MonoBehaviour
{
    public GameObject thisPanel;
    public Transform content;
    public GameObject element;
    private int invenoryCount;
    private void Start()
    {
        invenoryCount = Inventory.Instant.storagesBought.Count;
    }
    public void Open()
    {
        UpdateElements();
        thisPanel.SetActive(true);
        EditModeUpgrade.Instance.UpdateEdit(true);
    }

    public void Exit()
    {
        thisPanel.SetActive(false);
        EditModeUpgrade.Instance.UpdateEdit(false);
    }

    public void Rotate()
    {
        EditModeUpgrade.Instance.Rotate();
    }

    public void Place()
    {
        EditModeUpgrade.Instance.Place();
    }

    public void UpdateElements()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(0, content.GetComponent<RectTransform>().sizeDelta.y);
        foreach (var item in Inventory.Instant.storagesBought)
        {
            if (item.Value > 0)
            {
                EMUUIElement el = Instantiate(element, content).GetComponent<EMUUIElement>();
                el.storageInfo = ItemsBase.GetStorage(item.Key);
                el.icon.sprite = el.storageInfo.Icon;
                content.GetComponent<RectTransform>().sizeDelta += new Vector2(el.GetComponent<RectTransform>().sizeDelta.x, 0);
            }
        }
    }

    private void Update()
    {
        if(invenoryCount != Inventory.Instant.storagesBought.Count)
        {
            UpdateElements();
            invenoryCount = Inventory.Instant.storagesBought.Count;
        }
    }
}
