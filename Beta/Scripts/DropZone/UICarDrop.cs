using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICarDrop : MonoBehaviour
{
    public GameObject car;

    public void Drop()
    {
        car?.GetComponent<DropFromCar>()?.Drop();
    }
}
