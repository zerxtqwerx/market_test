using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EMUUIElement : MonoBehaviour
{
    public Image icon;
    public StorageInfo storageInfo;

    public void OnClick()
    {
        EditModeUpgrade.Instance.Select(storageInfo);
    }
}
