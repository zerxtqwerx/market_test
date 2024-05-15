using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLvl : MonoBehaviour
{
    public float changeCost = 1000;
    public LoadScene loadScene;
    public GameObject loadingScreen;
    public void TryChangeLvl()
    {
        if(MoneyManager.Money >= changeCost)
        {
            loadingScreen.SetActive(true);
            loadScene.Load();
        }
    }
}
