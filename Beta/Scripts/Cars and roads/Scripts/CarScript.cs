using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarScript : MonoBehaviour
{
    [SerializeField] private GameObject[] points;//Тут указывайте точки куда хотите чтоб машина двигалась
    [SerializeField] private float minDistance;// Минимальная дистанция от машины до точки 
    private int selected;// Это выбранная i среди всех точек
    private NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        selected = 0;
    }

    private void Update()
    {
        if (Vector3.Distance(points[selected].transform.position, transform.position) > minDistance)
        {
            agent.SetDestination(points[selected].transform.position);
        }
        else
        {
            NextPoint();
            agent.SetDestination(points[selected].transform.position);
        }
    }

    void NextPoint()
    {
        if(selected == points.Length - 1)
        {
            selected = 0;
        }
        else
        {
            selected++;
        }
    }
}
