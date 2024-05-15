using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHirePanel : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    [SerializeField]
    private GameObject loaderPrefab;
    [SerializeField]
    private GameObject cashierPrefab;
    public void SpawnNPCLoader()
    {
        if (GameManager.Instance.npcLoaders.Count < 2)
        {
            GameObject o = Instantiate(loaderPrefab, spawnPoint.position, spawnPoint.rotation);
            o.GetComponent<NPCLoader>().SetID();
        }
    }

    public void SpawnNPCCashier()
    {
        if (GameManager.Instance.npcCashiers.Count < 2)
        {
            GameObject o = Instantiate(cashierPrefab, spawnPoint.position, spawnPoint.rotation);
            o.GetComponent<NPCCashier>().SetID();
        }
    }
}
