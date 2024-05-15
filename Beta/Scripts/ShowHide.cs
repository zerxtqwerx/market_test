using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHide : MonoBehaviour
{
    public void Do()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
