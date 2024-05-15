using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CarScript : MonoBehaviour
{
    [SerializeField] private GameObject[] points;//��� ���������� ����� ���� ������ ���� ������ ���������
    [SerializeField] private float minDistance;// ����������� ��������� �� ������ �� ����� 
    private int selected;// ��� ��������� i ����� ���� �����
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
