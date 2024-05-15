using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCLoader : MonoBehaviour
{
    private enum NPCState
    {
        None,
        GoToLoaderPoint,
        UnoadCar,
        FoundBox,
        GetBox,
        CheckFor,
        FoundStorage,
        GoToStorage,
        GoToGrocery,
        GoHome
    }
    public static int id = 0;
    public int myId = -1;
    public bool setIdOnAwake = false;
    public NPCSkin npcskin;

    [SerializeField]
    private NavMeshAgent nma;
    [SerializeField]
    private Transform boxPoint;

    private NPCState currentState = NPCState.GoToLoaderPoint;
    private Transform homePoint;
    private Transform loaderPoint;
    private BoxGameObject bgo;
    private Storage st;
    private GroceryRack gr;
    private int dayCount = 0;

    private void Awake()
    {
        if(setIdOnAwake)
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
        loaderPoint = GameManager.GetLoaderPoint();
        homePoint = GameManager.GetHomePoint();
        GameManager.Instance.npcLoaders.Add(this);
    }

    private void FixedUpdate()
    {
        switch (currentState)
        {
            case NPCState.GoToLoaderPoint:
                GoTo(loaderPoint.position);
                if (!DayNightManager.Instant.isDay)
                {
                    currentState = NPCState.GoHome;
                    break;
                }
                if (GameManager.Instance.notUnloadBoxesGameObjects.Count > 0)
                {
                    currentState = NPCState.GetBox;
                    break;
                }
                if (GameManager.Instance.notEmptyZone != null)
                {
                    currentState = NPCState.UnoadCar;
                    break;
                }
                bgo = GameManager.FoundWhatCanBeFill(transform.position);
                if(bgo != null)
                {
                    currentState = NPCState.GetBox;
                    break;
                }
                break;
            case NPCState.UnoadCar:
                {
                    if (GameManager.Instance.notEmptyZone != null)
                    {
                        GoTo(GameManager.Instance.notEmptyZone.transform.position);
                        if (Vector3.Distance(GameManager.Instance.notEmptyZone.transform.position, transform.position) <= 5)
                        {
                            GameManager.Instance.notEmptyZone.dfc.Drop();
                            currentState = NPCState.GetBox;
                            break;
                        }
                    }
                    else
                    {
                        currentState = NPCState.GoToLoaderPoint;
                        break;
                    }
                }
                break;
            case NPCState.FoundBox:
                {
                    bgo = GameManager.GetClosestBox(transform.position);
                    if (bgo != null)
                    {
                        currentState = NPCState.GetBox;
                    }
                    else
                    {
                        currentState = NPCState.GoToLoaderPoint;
                    }
                }
                break;
            case NPCState.GetBox:
                {
                    if (bgo != null)
                    {
                        if (bgo.status == "RedyForLoad" || bgo.status == "Droped" || bgo.status == "Placed")
                        {
                            GoTo(bgo.transform.position);
                            if (Vector3.Distance(transform.position, bgo.transform.position) <= 2.5f)
                            {
                                npcskin.anim.SetBool("box", true);
                                npcskin.anim.SetTrigger("vsat");
                                GetBox(bgo);
                                currentState = NPCState.CheckFor;
                            }
                        }
                        else
                        {
                            currentState = NPCState.FoundBox;
                        }
                    }
                    else
                    {
                        currentState = NPCState.FoundBox;
                    }
                }
                break;
            case NPCState.FoundStorage:
                {
                    st = GameManager.GetStorage(transform.position, bgo);
                    if (st != null)
                    {
                        currentState = NPCState.GoToStorage;
                    }
                }
                break;
            case NPCState.GoToStorage:
                {
                    if (st.isFull())
                    {
                        currentState = NPCState.FoundStorage;
                        break;
                    }
                    GoTo(st.point_to_go.position);
                    if (Vector3.Distance(st.transform.position, transform.position) <= 3)
                    {
                        if (bgo != null)
                        {
                            st.PlaceBox(bgo);
                            npcskin.anim.SetBool("box", false);
                            npcskin.anim.SetTrigger("polozit");
                            bgo = null;
                        }
                        currentState = NPCState.GoToLoaderPoint;
                        break;
                    }
                }
                break;
            case NPCState.GoHome:
                {
                    GoTo(homePoint.position);
                    if (DayNightManager.Instant.isDay)
                    {
                        currentState = NPCState.GoToLoaderPoint;
                    }
                }
                break;
            case NPCState.None:
                break;
            case NPCState.CheckFor:
                {
                    if (bgo != null)
                    {
                        GroceryRack gr = GameManager.GetGrocery(transform.position, bgo);
                        if(gr != null)
                        {
                            this.gr = gr;
                            currentState = NPCState.GoToGrocery;
                            break;
                        }
                        else
                        {
                            currentState = NPCState.FoundStorage;
                            break;
                        }
                    }
                    else
                    {
                        currentState = NPCState.GoToLoaderPoint;
                        break;
                    }
                }
                break;
            case NPCState.GoToGrocery:
                GoTo(gr.transform.position);
                if (Vector3.Distance(gr.transform.position, transform.position) <= 2.5f)
                {
                    if (gr.Fill(bgo))
                    {
                        npcskin.anim.SetBool("box", false);
                        npcskin.anim.SetTrigger("polozit");
                        Destroy(bgo.gameObject);
                        bgo = null;
                        currentState = NPCState.GoToLoaderPoint;
                        break;
                    }
                    else
                    {
                        currentState = NPCState.CheckFor;
                        break;
                    }
                }
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
        if(Vector3.Distance(transform.position, nma.pathEndPosition) <= 0.2f)
        {
            npcskin.anim.SetBool("walk", false);
        }
        else
        {
            npcskin.anim.SetBool("walk", true);
        }
    }

    public void GetBox(BoxGameObject bg)
    {
        bg.OnPickedUpByNpc();
        bg.OnGet();
        bg.transform.SetParent(boxPoint);
        bg.transform.localPosition = Vector3.zero;
        bg.transform.localRotation = Quaternion.identity;
    }

    public void Save(NPCLoaderSaveFile nlsf)
    {
        nlsf.position = transform.position;
        nlsf.eulerRotation = transform.eulerAngles;
        BoxSaveFile bsf = new BoxSaveFile();
        bsf.id = "";
        if(bgo!=null)
        {
            bgo.Save(bsf);
        }
        nlsf.inventoryObj = bsf;
        nlsf.currentState = (int)currentState;
        nlsf.dayCount = dayCount;
    }

    public void Load(NPCLoaderSaveFile nlsf)
    {
        nma.enabled = false;
        transform.position = nlsf.position;
        transform.eulerAngles = nlsf.eulerRotation;
        if(nlsf.inventoryObj.id != "")
        {
            Item i = ItemsBase.GetItem(nlsf.inventoryObj.id);
            if(i != null)
            {
                GameObject o = Instantiate(i.BoxesPrefab);
                o.GetComponent<BoxGameObject>().Load(nlsf.inventoryObj, i);
                bgo = o.GetComponent<BoxGameObject>();
                GetBox(o.GetComponent<BoxGameObject>());
            }
        }
        NPCState ns = (NPCState)nlsf.currentState;
        switch (ns)
        {
            case NPCState.None:
                currentState = NPCState.None;
                break;
            case NPCState.GoToLoaderPoint:
                currentState = NPCState.GoToLoaderPoint;
                break;
            case NPCState.UnoadCar:
                currentState = NPCState.GoToLoaderPoint;
                break;
            case NPCState.FoundBox:
                currentState = NPCState.GoToLoaderPoint;
                break;
            case NPCState.GetBox:
                currentState = NPCState.GoToLoaderPoint;
                break;
            case NPCState.FoundStorage:
                currentState = NPCState.FoundStorage;
                break;
            case NPCState.GoToStorage:
                currentState = NPCState.FoundStorage;
                break;
            case NPCState.GoHome:
                currentState = NPCState.GoHome;
                break;
            case NPCState.CheckFor:
                currentState = NPCState.CheckFor;
                break;
            case NPCState.GoToGrocery:
                currentState = NPCState.CheckFor;
                break;
        }
        dayCount = nlsf.dayCount;
        nma.enabled = true;
    }

    public void OnDay()
    {
        dayCount++;
        if(dayCount >= 3)
        {
            dayCount = 0;
            MoneyManager.ChangeMoney(-25);
        }
    }
}
