using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ExpirationDate : MonoBehaviour
{
    private static Dictionary<string, float> ExpirationDateByCategory = new Dictionary<string, float>
    {
        { "berries",    3f },
        { "fruits",     5f },
        { "vegetables", 6f },
        { "meat",       7f },
        { "dairy",      2f }
    };

    /*public float FindExpirationDateByCategory(string category)
    {
        return ExpirationDateByCategory[category];
    }*/

    public DateTime CalculateExpirationDate(string category)
    {
        //if(ExpirationDateByCategory.TryGetValue()

        float days = ExpirationDateByCategory[category];

        var dnm = FindObjectOfType<TimeManager>();
        DateTime dt = dnm.GetDateTime();

        dt.AddDays(days);
        return dt;
    }
}
