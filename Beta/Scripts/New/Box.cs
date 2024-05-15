using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    ItemGameObject content;
    int capacity;
    GameObject boxPrefab;

    public GameObject emptyBox;

    public Box()
    {
        content = null;
        capacity = 0;
        boxPrefab = null;
    }

    public Box(ItemGameObject igo)
    {
        content = igo;
        capacity = igo.item.CountInBox;
        boxPrefab = igo.item.BoxesPrefab;
    }

    public ItemGameObject GiveContent()
    {
        if (capacity != 0)
        {
            capacity--;
            return content;
        }
        return null;
    }

    public void GetContent(ItemGameObject igo)
    {
        if (capacity !=  content.item.CountInBox && igo == content)
        {
            capacity++;
        }
    }

    private void SetEmptyBox()
    {
        boxPrefab = emptyBox;
    }

    private void SetFilledBox()
    {
        boxPrefab = content.item.BoxesPrefab;
    }
}
