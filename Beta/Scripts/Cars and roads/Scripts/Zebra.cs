using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zebra : MonoBehaviour
{
    [SerializeField] private bool carPassing = false, botPassing = false; //Пересекает ли зебру машина/нпс
    [SerializeField] private GameObject stoppedObj = null;//Обьект который был  остоновлен 

    private void OnTriggerEnter(Collider other)
    {
        if (carPassing == false && botPassing == false)
        {
            if (other.CompareTag("Car"))
            {
                carPassing = true;
            }
            else if (other.CompareTag("NPC"))
            {
                botPassing = true;
            }
        }
        else if (carPassing)
        {
            if (other.CompareTag("NPC"))
            {
                stoppedObj = other.gameObject;
                other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
        else if (botPassing)
        {
            if (other.CompareTag("Car"))
            {
                stoppedObj = other.gameObject;
                other.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (carPassing == true || botPassing == true)
        {
            if (stoppedObj != null)
            {
                stoppedObj.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
                stoppedObj = null;
                if (other.CompareTag("Car"))
                {
                    carPassing = false;
                }
                else if (other.CompareTag("NPC"))
                {
                    botPassing = false;
                }
            }
            else
            {
                if (other.CompareTag("Car"))
                {
                    carPassing = false;
                }
                else if (other.CompareTag("NPC"))
                {
                    botPassing = false;
                }
            }
        }
    }
}
