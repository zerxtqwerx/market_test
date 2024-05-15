using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Bounds getterZone;
    [SerializeField]
    private GameObject npcPrefab;
    private float timer = 0;
    [SerializeField]
    private Vector2 spawnRate;
    public List<Transform> npcPoints;
    public DayNightManager dnm;

    private void FixedUpdate()
    {
        if (dnm.isDay)
        {
            timer -= Time.fixedDeltaTime;
            if (timer <= 0)
            {
                SpawnPlayer(GetSpawnCoordinate());
                timer = Random.Range(spawnRate.x, spawnRate.y);
            }
        }
    }

    public Vector3 GetSpawnCoordinate()
    {
        return npcPoints[Random.Range(0, npcPoints.Count)].position;
    }

    public void SpawnPlayer(Vector3 p)
    {
        GameObject o = Instantiate(npcPrefab, p, Quaternion.identity);
        o.GetComponent<NPC>().OnNormalSpawn();
    }
}
