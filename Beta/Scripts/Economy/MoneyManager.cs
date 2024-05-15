using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instant;

    public static float Money { get { return money; } set { Instant.SetMoney(value); } }
    private static float money = 100;

    [SerializeField]
    private bool debug = false;

    private void Awake()
    {
        Instant = this;
    }

    public void SetMoney(float c)
    {
        money = c;
        UIManager.SetMoney(c);
    }

    public static bool ChangeMoney(float m)
    {
        if(money + m < 0)
        {
            return false;
        }
        Money += m;
        return true;
    }

    public static bool IsCanDecreaseMoney(float m)
    {
        if (money - m < 0)
        {
            return false;
        }
        return true;
    }

    private void FixedUpdate()
    {
        if(debug)
        {
            //SetMoney(10000);
        }
    }

    public void Save(EconomicSaveFile ecf)
    {
        ecf.money = money;
    }

    public void Load(EconomicSaveFile ecf)
    {
        SetMoney(ecf.money);
    }

}
