using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCCashier : MonoBehaviour
{
    private enum NPCState
    {
        None,
        FindCashbox,
        GoToCashbox,
        GoHome
    }
    public static int id = 0;
    public int myId = -1;
    public bool setIdOnAwake = false;
    public NPCSkin skin;

    [SerializeField]
    private NavMeshAgent nma;
    private Transform homePoint;
    private NPCState currentState = NPCState.FindCashbox;
    private Getter getter;
    private int dayCount = 0;
    private void Awake()
    {
        if (setIdOnAwake)
        {
            SetID();
        }
    }
    public void SetID()
    {
        myId = id;
        id++;
    }

    public void SetID(int i)
    {
        myId = i;
        id++;
    }
    private void Start()
    {
        homePoint = GameManager.GetHomePoint();
        GameManager.Instance.npcCashiers.Add(this);
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case NPCState.None:
                break;
            case NPCState.FindCashbox:
                {
                    skin.anim.SetBool("walk", false);
                    if (!DayNightManager.Instant.isDay)
                    {
                        currentState = NPCState.GoHome;
                        break;
                    }
                    getter = GameManager.Instance.GetGetterForCashier(transform.position, this);
                    if (getter != null)
                    {
                        getter.npccashier = this;
                        currentState = NPCState.GoToCashbox;
                    }
                }
                break;
            case NPCState.GoToCashbox:
                {
                    if (getter != null)
                    {
                        GoTo(getter.cashierZone.transform.position);
                        if (Vector3.Distance(getter.cashierZone.transform.position, transform.position) <= 0.5f)
                        {
                            skin.anim.SetBool("walk", false);
                            LoockAt(getter.QueuePositions[0].position);
                        }
                        else
                        {
                            skin.anim.SetBool("walk", true);
                        }
                        if (!DayNightManager.Instant.isDay)
                        {
                            getter.npccashier = null;
                            getter = null;
                            currentState = NPCState.GoHome;
                            break;
                        }
                    }
                    if (!DayNightManager.Instant.isDay)
                    {
                        currentState = NPCState.GoHome;
                        break;
                    }
                }
                break;
            case NPCState.GoHome:
                if(DayNightManager.Instant.isDay)
                {
                    currentState = NPCState.FindCashbox;
                    break;
                }
                GoTo(homePoint.position);
                skin.anim.SetBool("walk", true);
                break;
            default:
                break;
        }
    }
    private Vector3 navigatePos = Vector3.zero;
    public void GoTo(Vector3 p)
    {
        if (navigatePos != p)
        {
            nma.SetDestination(p);
            navigatePos = p;
        }
    }

    public void Save(NPCCashierSaveFile ncsf)
    {
        ncsf.position = transform.position;
        ncsf.eulerRotation = transform.eulerAngles;
        ncsf.currentState = (int)currentState;
        ncsf.dayCount = dayCount;
    }

    public void Load(NPCCashierSaveFile ncsf)
    {
        nma.enabled = false;
        transform.position = ncsf.position;
        transform.eulerAngles = ncsf.eulerRotation;
        NPCState ns = (NPCState)ncsf.currentState;
        switch (ns)
        {
            case NPCState.None:
                currentState = NPCState.None;
                break;
            case NPCState.FindCashbox:
                currentState = NPCState.FindCashbox;
                break;
            case NPCState.GoToCashbox:
                currentState = NPCState.FindCashbox;
                break;
            case NPCState.GoHome:
                currentState = NPCState.GoHome;
                break;
            default:
                break;
        }
        dayCount = ncsf.dayCount;
        nma.enabled = true;
    }

    public void OnDay()
    {
        dayCount++;
        if (dayCount >= 3)
        {
            dayCount = 0;
            MoneyManager.ChangeMoney(-20);
        }
    }

    private void LoockAt(Vector3 p)
    {
        Vector3 direction = (p - transform.position).normalized;
        if (direction.x != 0 && direction.y != 0)
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5);
        }
    }
}
