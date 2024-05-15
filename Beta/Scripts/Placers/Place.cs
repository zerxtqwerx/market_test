using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Place : MonoBehaviour
{
    private bool isEmpty = true;

    public bool IsEmpty() { 
        if (gameObject.transform.childCount > 0) { 
            isEmpty = false; 
        } else
        {
            isEmpty = true;
        }

        return isEmpty;
    }

    public void Clear()
    {
        for(int i=0;i<transform.childCount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    public void SetEmpty(bool c) { isEmpty = c; }
    
}
