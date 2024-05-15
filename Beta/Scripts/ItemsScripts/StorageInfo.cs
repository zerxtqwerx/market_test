using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StorageInfo", menuName = "Storageinfo", order = 52)]
public class StorageInfo : ScriptableObject
{
    [SerializeField]
    private string id = "";
    [SerializeField]
    private string category = "";
    [SerializeField]
    private string itemName = "";
    [SerializeField]
    private float cost = 0;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private GameObject prefab;

    public string Id => id;
    public string Category => category;
    public string ItemName => itemName;
    public float Cost => cost;
    public Sprite Icon => icon;
    public GameObject Prefab => prefab;
}
