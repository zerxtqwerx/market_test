using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UICostChanger : MonoBehaviour
{
    public TMP_Text itemLabel;
    public TMP_Text origCost;
    public TMP_Text currentCost;
    public TMP_Text buyChance;
    public TMP_Text currentProcent;
    public Slider procentSlider;

    private ItemGameObject currentItem;
    private GroceryRack currentPlaceZone;

    [SerializeField] private GameObject CostChangerBody;

    public void Open(GroceryRack pz)
    {
        currentPlaceZone = pz;
        if(currentPlaceZone.itemsIn.Count > 0)
        {
            currentItem = currentPlaceZone.itemsIn[0];
 
            origCost.text = "Цена покупки: " + currentItem.item.BoxCost;
            procentSlider.value = currentItem.item.margin / 25;

            OnSliderChange(procentSlider.value);
            CostChangerBody.SetActive(true);
        }
    }

    public void OnSliderChange(float value)
    {
        /*currentItem = currentPlaceZone.itemsIn[0];

        currentPlaceZone.procent = value * 25;

        currentItem.item.margin = (int)value * 25;
        currentItem.item.changeProductCost();

        currentProcent.text = currentItem.item.margin + "%";

        currentCost.text = "Цена продажи: " + currentItem.item.ItemCost;

        buyChance.text = "Интерес покупателей: " + currentItem.item.Interest + "%";*/

    }
}
