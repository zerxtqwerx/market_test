using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropFromCar : MonoBehaviour
{
    public DropZone dz;
    public LayerMask lm;
    public List<Transform> carPoints = new List<Transform>();
    public List<Transform> dropPoints = new List<Transform>();

    public void Drop()
    {
        int i = 0;
        foreach (var item in carPoints)
        {
            Collider[] c = Physics.OverlapBox(item.position, Vector3.one / 4, item.rotation, lm);
            GameObject o = null;
            foreach (var item2 in c)
            {
                if(item2.tag == "Pickable")
                {
                    o = item2.gameObject;
                    break;
                }
            }
            if(o!=null)
            {
                for(;i<dropPoints.Count;i++)
                {
                    if (!Physics.CheckBox(dropPoints[i].position, Vector3.one / 4, dropPoints[i].rotation))
                    {
                        o.transform.position = dropPoints[i].position;
                        if(o.TryGetComponent(out BoxGameObject bgo))
                        {
                            bgo.OnGetOutFromCar();
                        }
                        i++;
                        break;
                    }
                }
            }
        }
    }
}
