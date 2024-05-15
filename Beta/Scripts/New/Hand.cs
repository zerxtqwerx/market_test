using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    public Button pickupButton;
    public Button putButton;
    bool takeFlag;

    GameObject inHand;


    void Start()
    {
        pickupButton.gameObject.SetActive(false);
        putButton.gameObject.SetActive(false);
        takeFlag = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickable")
        {
            pickupButton.gameObject.SetActive(true);
            inHand = other.gameObject;
        }
    }

    public void PickUp()
    {
        //не кликается. возможно из за коллайдер триггеров, мешающей области или чего то еще

        Debug.Log("Click");
        if (!takeFlag)
        {
            takeFlag = true;
            inHand.transform.SetParent(gameObject.transform);
            putButton.gameObject.SetActive(false);
        }
    }

    public void Put()
    {
        if (takeFlag)
        {
            takeFlag = false;
            inHand.transform.parent = null;
        }
    }

}
