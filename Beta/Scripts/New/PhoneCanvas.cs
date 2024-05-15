using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneCanvas : MonoBehaviour
{
    public void OnCloseButton(GameObject objct)
    {
        objct.SetActive(false);
    }

    public void OnOpenButton(GameObject objct)
    {
        objct.SetActive(true);
    }
}
