using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instant;
    public UICostChanger uiCostChanger;
    public StorageEditUI storageEditUI;
    public TMP_Text moneyText;
    public Button pickButton;
    public Button loadButton;
    public Button dropButton;
    public Button deliteButton;
    public Button carDropButton;
    public Button changePriceButton;
    public Button enterCashRegister;
    public Button exitCashRegister;

    public GameObject allControlls;
    public GameObject allEconomics;
    public GameObject editModePanels;
    public GameObject upgradesLoading;

    private void Awake()
    {
        Instant = this;
    }

    public static void SetMoney(float c)
    {
        Instant.moneyText.text = Math.Round(c, 2) + " $";
    }

    public static void ClearButtons()
    {
        Instant.pickButton.gameObject.SetActive(false);
        Instant.loadButton.gameObject.SetActive(false);
        Instant.dropButton.gameObject.SetActive(false);
        Instant.deliteButton.gameObject.SetActive(false);
        Instant.carDropButton.gameObject.SetActive(false);
        Instant.changePriceButton.gameObject.SetActive(false);
        Instant.enterCashRegister.gameObject.SetActive(false);
        Instant.exitCashRegister.gameObject.SetActive(false);
    }

    public static void SetPickButton(bool show)
    {
        Instant.pickButton.gameObject.SetActive(show);
    }

    public static void SetLoadButton(bool show)
    {
        Instant.loadButton.gameObject.SetActive(show);
    }

    public static void SetDropButton(bool show)
    {
        Instant.dropButton.gameObject.SetActive(show);
    }

    public static void SetDeliteButton(bool show)
    {
        Instant.deliteButton.gameObject.SetActive(show);
    }

    public static void SetCarDropButton(bool show)
    {
        Instant.carDropButton.gameObject.SetActive(show);
    }

    public static void SetPriceChangeButton(bool show)
    {
        Instant.changePriceButton.gameObject.SetActive(show);
    }

    public static void ToEditMode(bool edit)
    {
        Instant.allControlls.SetActive(!edit);
        Instant.allEconomics.SetActive(!edit);
        Instant.editModePanels.SetActive(edit);
    }

    public static void SetEnterCashRegisterButton(bool show)
    {
        Instant.enterCashRegister.gameObject.SetActive(show);
    }

    public static void SetExitCashRegisterButton(bool show)
    {
        Instant.exitCashRegister.gameObject.SetActive(show);
    }

    public void EditButtonPress()
    {
        PlayerGetter.Instant.OpenEditModeSelected();
    }
}
