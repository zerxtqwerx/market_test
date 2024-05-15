using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropZone : MonoBehaviour
{
    public int id = -1;
    public DropFromCar dfc;
    public GameObject mainGameObject;
    [SerializeField]
    private BoundsInt dropBounds;
    [SerializeField]
    private Bounds boundsBounds;
    [SerializeField]
    private float dist = 1;
    [SerializeField]
    private Transform orig;
    [SerializeField]
    private int max_items = 25;
    [SerializeField]
    private LayerMask itemsLayer;
    [SerializeField]
    List<Transform> dropPositions = new List<Transform>();
    private bool onDeliver = false;
    public int MaxItems => max_items;
    public bool OnDeliver { get { return onDeliver; } set { onDeliver = value; } }

    /*public void DropByTime(Item[] items, float time)
    {
        StartCoroutine(Drop(items, time));
    }

    public IEnumerator Drop(Item[] items, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        int k = 0;
        while (k < items.Length)
        {
            for (int i = dropBounds.min.x; i < (dropBounds.max.x - 1) && k < items.Length; i++)
            {
                for (int j = dropBounds.min.z; j < (dropBounds.max.z - 1) && k < items.Length; j++)
                {
                    Instantiate(items[k].Prefab, transform.position + Vector3.forward * i * dist + Vector3.right * j * dist, Quaternion.identity);
                    k++;
                }
            }
        }
        onDeliver = false;
    }*/

    public void Drop(Item[] items)
    {
        int k = 0;
        while (k < items.Length)
        {
            for(int i = 0;i<dropPositions.Count;i++)
            {
                Instantiate(items[k].BoxesPrefab, dropPositions[i].position, transform.rotation);
                k++;
                if (k >= items.Length)
                {
                    onDeliver = false;
                    return;
                }
            }
        }
            /*while (k < items.Length)
            {
                for (int i = dropBounds.min.x; i < dropBounds.max.x - 1; i++)
                {
                    for (int j = dropBounds.min.z; j < dropBounds.max.z - 1; j++)
                    {
                        Instantiate(items[k].Prefab, transform.position + Vector3.forward * j * dist + Vector3.right * i * dist, Quaternion.identity);
                        k++;
                        if (k >= items.Length)
                        {
                            onDeliver = false;
                            return;
                        }
                    }
                }
            }*/
        onDeliver = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        /*for(int i = dropBounds.min.x; i < dropBounds.max.x - 1; i++)
        {
            for(int j = dropBounds.min.z; j < dropBounds.max.z - 1; j++)
            {
                Gizmos.DrawCube(transform.position + Vector3.forward * j * dist + Vector3.right * i * dist, Vector3.one / 4);
            }
        }*/
        Gizmos.DrawWireCube(transform.position + boundsBounds.center, boundsBounds.size);
    }

    public bool CheckZoneForItems()
    {
        return !Physics.CheckBox(transform.position + boundsBounds.center, boundsBounds.size / 2, transform.rotation, itemsLayer) && !onDeliver;
    }

    public bool IsNotEmpty()
    {
        return Physics.CheckBox(transform.position + boundsBounds.center, boundsBounds.size / 2, transform.rotation, itemsLayer);
    }
}
