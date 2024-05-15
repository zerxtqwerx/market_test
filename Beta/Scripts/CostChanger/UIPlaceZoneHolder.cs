using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlaceZoneHolder : MonoBehaviour
{
    public GroceryRack placeZone;
    public UICostChanger costChanger;

    public void OpenUI()
    {
        costChanger.Open(placeZone);
    }
}
