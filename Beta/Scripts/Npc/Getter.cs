using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Getter : MonoBehaviour
{
    public Bounds getterZone;
    public LayerMask lm;
    public List<NPC> npcs = new List<NPC>();
    public Transform dropPoint;
    public Transform cashierZone;
    public NPCCashier npccashier;
    public List<Transform> QueuePositions;
    public GameObject getterCamera;
    public bool CanDrop = false;
    public bool OnPlayerService = false;

    public bool GetEmptyCoordinate(out Vector3 p)
    {
        p = dropPoint.position;
        return true;
    }

    public void AddToQueue(NPC npc)
    {
        npcs.Add(npc);
        if(npcs.Count == 1)
        {
            npc.GetterQueueNext();
        }
        UpdateAllQueuePositions();
    }

    public void RemoveFromQueue(NPC npc)
    {
        npcs.Remove(npc);
        if(npcs.Count > 0)
        {
            npcs[0].GetterQueueNext();
        }
        UpdateAllQueuePositions();
    }

    public void UpdateAllQueuePositions()
    {
        for(int i=1;i<npcs.Count;i++)
        {
            if (i < QueuePositions.Count)
            {
                npcs[i].UpdateQueqePosition(i, QueuePositions[i].position);
            }
            else
            {
                npcs[i].UpdateQueqePosition(-1, Vector3.zero);
            }
        }
    }

    public void OnSell()
    {
        if(npccashier != null)
        {
            npccashier.skin.anim.SetTrigger("prodovat");
        }
    }

    public void ControllByPlayer()
    {
        /*CharacterControl.Instance.InGetter = this;
        CharacterControl.Instance.PlayerCamera.enabled = false;
        getterCamera.SetActive(true);
        OnPlayerService = true;*/
    }

    public void UnControllByPlayer()
    {
        /*CharacterControl.Instance.InGetter = null;
        CharacterControl.Instance.PlayerCamera.enabled = true;
        getterCamera.SetActive(false);
        OnPlayerService = false;*/
    }
}
