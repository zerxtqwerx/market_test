using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PutInBox : MonoBehaviour
{
    /*private class Point
    {
        private ItemGameObject product;
        private Transform point;

        public Transform Point_ => point;
        public ItemGameObject Product_ => product;


        *//*public Point(Transform p)
        {
            product = null; 
            point = p;
        }*//*
    };*/
    public int capacity;
    public GameObject FullBox;
    public GameObject EmptyBox;
    private Button putInBoxButton;
    private Text buttonText;

    static List<ItemGameObject> points;

    void Start()
    {
        putInBoxButton = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<Button>();
        putInBoxButton.gameObject.SetActive(false);

        buttonText = putInBoxButton.GetComponentInChildren<Text>();
        //points = new List<Point>();

        /*for (int i = 0; i < capacity; i++)
        {
            Point p = new Point(gameObject.transform.GetChild(i));
            points.Add(p);
            Debug.Log("P " + points);
        }*/
    }

    public bool putInBox(ItemGameObject[] igo)
    {
        if(igo == null) return false;
        if(BoxIsFull() == true) return false;
        int n = 0;
        while(!BoxIsFull()) 
        {
            points.Add(igo[n++]);
        }
        return true;
    }

    public ItemGameObject TakeOutOfBox()
    {
        if (!BoxIsEmpty())
        {
            ItemGameObject igo = points[-1];
            points.RemoveAt(-1);
            return igo;

        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            putInBoxButton.gameObject.SetActive(true);
        }
    }

    private void TextTrigger()
    {
        if (BoxIsFull())
        {
            buttonText.text = "Нет места";
            putInBoxButton.interactable = false;
        }
        else
        {
            buttonText.text = "Положить";
            putInBoxButton.interactable = true;
        }
    }

    private bool BoxIsEmpty()
    {
        return points.Count == 0;
    }

    private bool BoxIsFull()
    {
        return points.Count == 10;
    }

    /*public void PutProducts(List<ItemGameObject> list)
    {
        if (point != null)
        {
            if(points[0].item.Id == list.item.Id)
            {
                int n = 0;
                list<int> emptyPoints = FindEmptyPoints();
                if(emptyPoints != null)
                {
                    foreach (int n2 in emptyPoints)
                    {
                        //points[n2] = 
                    }
                }
            }
        }
    }

    private List<int> FindEmptyPoints()
    {
        List<int> list;
        for(int n = 0; n != points.Length; n++)
        {
            if (points[n] == null)
            {
                list.Add(n);
            }
        }
        return list;
    }*/
}
