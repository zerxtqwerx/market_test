using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPlayerGetter : MonoBehaviour
{
    public Getter getter;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Cashier")
        {
            getter.CanDrop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Cashier")
        {
            getter.CanDrop = false;
        }
    }
}
