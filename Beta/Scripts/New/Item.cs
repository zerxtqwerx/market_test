using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Item", menuName = "Items", order = 51)]

public class Item : ScriptableObject
{
    [SerializeField] private string id       = "";
    [SerializeField] private string category = "";
    [SerializeField] private string itemName = "";

    [SerializeField] private int countInBox;
    [SerializeField] private float boxCost;
    [SerializeField] private DateTime expirationDate;

    [SerializeField] private float origCost; //оригинальная цена
    [SerializeField] public float itemCost; //цена с наценкой

    [SerializeField] public  int   margin;     //наценка
    public string trader;
    private float interest;   //интерес

    [SerializeField] private Sprite icon;

    [SerializeField] private GameObject boxesPrefab;
    [SerializeField] private GameObject itemPrefab;


    public string Id        => id;
    public string Category  => category;
    public string ItemName  => itemName;

    public int CountInBox => countInBox;
    public float BoxCost  => boxCost;
    public DateTime ExpirationDate => expirationDate;

    public float OrigCost => origCost;

    public float Interest => interest;  

    public Sprite Icon  => icon;

    public GameObject BoxesPrefab   => boxesPrefab;
    public GameObject ItemPrefab    => itemPrefab;

    private ExpirationDate ed;

    private void Start()
    {
        itemCost = origCost;
        margin = 0;
        calculateInterest();

        ed = FindObjectOfType<ExpirationDate>();
        expirationDate = ed.CalculateExpirationDate(category);
    }

    private void Update()
    {
        Debug.Log(boxCost);
        Debug.Log(itemCost);
    }

    public void changeProductCost()
    {
        calculateInterest();

        double procent = Math.Round(1 + margin * 0.01f, 1);

        itemCost = (float)procent * origCost;

    }

    private void calculateInterest()
    {
        if (margin == -75)
            interest = 7.0f;

        else if (margin == -50)
            interest = 6.0f;

        else if (margin == -25)
            interest = 5.5f;

        else if (margin == 0)
            interest = 5.0f;

        else if (margin == 25)
            interest = 4.0f;

        else if (margin == 50)
            interest = 3.0f;

        else if (margin == 75)
            interest = 1.0f;
    }

    private void calculateExpirationDate(DateTime dt)
    {
        //ExpirationDateByCategory exbc = ExpirationDateByCategory.GetStringValue(id);
    }

    
    /*{
        berries     = 3,
        fruits      = 5,
        vegetables  = 6,
        meat        = 7,
        //dairy       = 2
    }*/
}
