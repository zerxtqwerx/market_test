using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
[System.Serializable]

public class NPC : MonoBehaviour
{
    private enum NPCState
    {
        None,
        FoundItem,
        GetItem,
        InQueue,
        DropItems,
        ToSpawn,
        Despawning
    }

    public NavMeshAgent nma;
    [SerializeField] private List<ItemGameObject> inventory;
    private ItemGameObject currentTarget;
    private Vector3 timedPosition;
    private Getter getterTarget;
    [SerializeField] private Transform inventoryOrig;
    [SerializeField]
    private bool die = false;
    [SerializeField] private float deathTimer = 30;
    private NPCState currentState;
    [SerializeField]
    private List<Item> itemList;
    private float timer = 3;
    private float timer2 = 0;
    private int skin_id = -1;
    private NPCSkin npcskin;

    public void OnNormalSpawn()
    {
        skin_id = Random.Range(0, GameManager.Instance.npcSkins.Count);
        SpawnSkin(skin_id);

        currentState = NPCState.FoundItem;

        CreateShoppingList();
    }

    private void CreateShoppingList()
    {
        /*Dictionary<Item, int> itemCheckList = new Dictionary<Item, int>();
        Item[] items = GameManager.Instance.GetExistItems();
        if (items.Length <= 0)
        {
            Destroy(gameObject);
            return;
        }
        Item it;

        for (int i = 0 ; i < 2; i++) //Random.Range(2, 8)
        {
            it = items[Random.Range(0, items.Length)];
            int NPCMargin = Random.Range(1, 8);

            if (NPCMargin >= it.Margin)
            {
                if (itemCheckList.ContainsKey(it))
                {

                    if (itemCheckList[it] < 4)
                    {
                        itemList.Add(it);
                        itemCheckList[it]++;

                    }
                }

                else
                {
                    itemList.Add(it);
                    itemCheckList.Add(it, 1);
                }
            }
         
        }*/
    }
    public void SpawnSkin(int i)
    {
        if(i >= 0 && i < GameManager.Instance.npcSkins.Count)
        {
            GameObject o = Instantiate(GameManager.Instance.npcSkins[i], transform);
            o.transform.localPosition = Vector3.zero;
            o.transform.localEulerAngles = Vector3.zero;
            npcskin = o.GetComponent<NPCSkin>();
        }
    }

    List<GameObject> dropedItems = new List<GameObject>();
    private void FixedUpdate()
    {
        if(die)
        {
            nma.isStopped = true;
            deathTimer -= Time.fixedDeltaTime;
            if(deathTimer <= 0)
            {
                Destroy(gameObject);
            }
            return;
        }
        switch(currentState)
        {
            case NPCState.FoundItem:
                currentTarget = GetTarget();
                if(currentTarget == null)
                {
                    if (inventory.Count > 0)
                    {
                        currentState = NPCState.InQueue;
                        timer = 30;
                        FoundGetterAndAddToQueue();
                    }
                    else
                    {
                        timedPosition = GameManager.Instance.spawner.GetSpawnCoordinate();
                        currentState = NPCState.ToSpawn;
                    }
                    return;
                }
                else
                {
                    currentState = NPCState.GetItem;
                }
                break;
            case NPCState.GetItem:
                if(currentTarget == null || !currentTarget.gameObject.activeSelf)
                {
                    currentState = NPCState.FoundItem;
                    return;
                }
                GoTo(currentTarget.transform.position);
                npcskin.anim.SetBool("walk", true);

                if (Vector3.Distance(currentTarget.transform.position, transform.position) <= 2.5f)
                {
                    npcskin.anim.SetTrigger("pokup");
                    if(CheckCostBuy(currentTarget))
                        AddToInventory(currentTarget);
                    else
                        itemList.Remove(currentTarget.item);
                    if (IsItemsListIsEmpty())
                    {
                        currentState = NPCState.InQueue;
                        timer = 30;
                        FoundGetterAndAddToQueue();
                    }
                    else
                    {
                        currentState = NPCState.FoundItem;
                    }
                }
                break;
            case NPCState.InQueue:
                GoTo(timedPosition);
                if(Vector3.Distance(timedPosition, transform.position) <= 0.1f)
                {
                    npcskin.anim.SetBool("walk", false);
                }
                else
                {
                    npcskin.anim.SetBool("walk", true);
                }
                timer -= Time.fixedDeltaTime;
                if(timer <= 0)
                {
                    SellAllInstant();
                    getterTarget.RemoveFromQueue(this);
                    timedPosition = GameManager.Instance.spawner.GetSpawnCoordinate();
                    currentState = NPCState.ToSpawn;
                }
                break;
            case NPCState.DropItems:
                dropedItems.RemoveAll(x => x == null);
                if(inventory.Count > 0 || dropedItems.Count > 0)
                {
                    GoTo(timedPosition);
                    if(Vector3.Distance(timedPosition, transform.position) <= 1.5f)
                    {
                        npcskin.anim.SetBool("walk", false);
                        LoockAt(getterTarget.cashierZone.position);
                        timer2 -= Time.fixedDeltaTime;
                        if (getterTarget.CanDrop)
                        {
                            if(currentTarget != null)
                            {
                                currentTarget.OnDrop();
                                currentTarget = null;
                            }
                            timer -= Time.fixedDeltaTime;
                            timer2 = inventory.Count * 10;
                            if (timer <= 0)
                            {
                                getterTarget.OnSell();
                                DropItem(timedPosition);
                                GetPathToItemDrop();
                                timer = 3;
                            }
                        }
                        if (getterTarget.OnPlayerService)
                        {
                            if(currentTarget == null)
                            {
                                DropItemToPlayer(timedPosition);
                            }
                        }
                        if(timer2 <= -30)
                        {
                            SellAllInstant();
                            getterTarget.RemoveFromQueue(this);
                            timedPosition = GameManager.Instance.spawner.GetSpawnCoordinate();
                            currentState = NPCState.ToSpawn;
                        }
                    }
                }
                else
                {
                    getterTarget.RemoveFromQueue(this);
                    timedPosition = GameManager.Instance.spawner.GetSpawnCoordinate();
                    currentState = NPCState.ToSpawn;
                }
                break;
            case NPCState.ToSpawn:
                GoTo(timedPosition);
                npcskin.anim.SetBool("walk", true);
                if (Vector3.Distance(timedPosition, transform.position) <= 1)
                {
                    npcskin.anim.SetBool("walk", false);
                    die = true;
                    currentState = NPCState.Despawning;
                }
                break;
        }
    }

    private ItemGameObject GetTarget()
    {
        return GameManager.Instance.GetClosestObject(transform.position, itemList);
    }

    private bool GetPathToItemDrop()
    {
        return getterTarget.GetEmptyCoordinate(out timedPosition);
    }

    private void FoundGetterAndAddToQueue()
    {
        getterTarget = GameManager.Instance.GetGetter(this);
        getterTarget.AddToQueue(this);
    }

    private void AddToInventory(ItemGameObject otg, bool on_save = false)
    {
        inventory.Add(otg);
        otg.gameObject.SetActive(false);
        otg.transform.parent = inventoryOrig;
        otg.transform.localPosition = Vector3.zero;
        if (on_save)
        {
            otg.status = "InNPCHands";
        }
        else
        {
            otg.OnGet();
            itemList.Remove(otg.item);
        }
    }

    private void DropItem(Vector3 p)
    {
        while (inventory.Count > 0)
        {
            ItemGameObject otg = null;
            if (inventory.Count > 0)
            {
                otg = inventory[0];
            }
            if (otg != null)
            {
                otg.gameObject.SetActive(true);
                otg.transform.parent = null;
                otg.transform.position = p;
                otg.transform.rotation = Quaternion.identity;
                otg.OnDrop();
                inventory.Remove(otg);
            }
        }
    }

    private void DropItemToPlayer(Vector3 p)
    {
        while (inventory.Count > 0)
        {
            ItemGameObject otg = null;
            if (inventory.Count > 0)
            {
                otg = inventory[0];
            }
            if (otg != null)
            {
                otg.gameObject.SetActive(true);
                otg.transform.parent = null;
                otg.transform.position = p;
                otg.transform.rotation = Quaternion.identity;
                otg.OnDropToDrag();
                dropedItems.Add(otg.gameObject);

                /*if (FindObjectOfType<MoneyManager>().ChangeMoney(-otg.Price))
                {
                    inventory.Remove(otg);
                }*/
            }
        }
    }
    
    private bool IsInventoryNotEmpty()
    {
        if(inventory.Count > 0)
        {
            return true;
        }
        return false;
    }

    public void GetterQueueNext()
    {
        timer = 0;
        currentState = NPCState.DropItems;
        currentTarget = null;
        getterTarget?.GetEmptyCoordinate(out timedPosition);
        GetPathToItemDrop();
    }

    public bool IsItemsListIsEmpty()
    {
        if(itemList.Count<=0)
        {
            return true;
        }
        return false;
    }
    private Vector3 navigatePos = Vector3.zero;
    public void GoTo(Vector3 p)
    {
        if(navigatePos != p)
        {
            nma.SetDestination(p);
        }
        navigatePos = p;
    }

    public void UpdateQueqePosition(int t, Vector3 pos)
    {
        if(getterTarget != null)
        {
            if (t == -1)
            {
                timedPosition = transform.position;
            }
            else
            {
                timedPosition = pos;
            }
        }
    }

    public bool CheckCostBuy(ItemGameObject igo)
    {
        if (igo.itemCost > 0)
            return true;
        return false;

        /*if (igo.itemCost <= 100)
            return true;
        float s = 300 - igo.itemCost;
        if (Random.Range(1, 201) <= s)
            return true;
        return false;*/
    }

    public void SellAllInstant()
    {
        foreach (var item in inventory)
        {
            //item.itemCost = 100;
            item.Destr();
        }
        inventory.Clear();
    }

    public void Save(NPCSaveFile nsf)
    {
        nsf.position = transform.position;
        nsf.eulerRotation = transform.eulerAngles;
        foreach (var item in inventory)
        {
            ItemGameObjectSaveFile igos = new ItemGameObjectSaveFile();
            item.Save(igos);
            nsf.inventory.Add(igos);
        }
        nsf.timed_position = timedPosition;
        nsf.die = die;
        nsf.deathTimer = deathTimer;
        nsf.currentSate = (int)currentState;
        foreach (var item in itemList)
        {
            nsf.itemsList.Add(item.Id);
        }
        nsf.timer = timer;
        nsf.skin_id = skin_id;
    }

    public void Load(NPCSaveFile nsf)
    {
        nma.enabled = false;
        transform.position = nsf.position;
        transform.eulerAngles = nsf.eulerRotation;
        foreach (var item in nsf.inventory)
        {
            Item i = ItemsBase.GetItem(item.id);
            if (i != null)
            {
                GameObject o = Instantiate(i.ItemPrefab, inventoryOrig);
                o.GetComponent<ItemGameObject>().Load(item, i);
                AddToInventory(o.GetComponent<ItemGameObject>(), true);
            }
        }
        timedPosition = nsf.timed_position;
        die = nsf.die;
        deathTimer = nsf.deathTimer;
        NPCState npstate = (NPCState)nsf.currentSate;
        switch (npstate)
        {
            case NPCState.None:
                currentState = NPCState.None;
                break;
            case NPCState.FoundItem:
                currentState = NPCState.FoundItem;
                break;
            case NPCState.GetItem:
                currentState = NPCState.FoundItem;
                break;
            case NPCState.InQueue:
                currentState = NPCState.FoundItem;
                break;
            case NPCState.DropItems:
                currentState = NPCState.FoundItem;
                break;
            case NPCState.ToSpawn:
                currentState = NPCState.ToSpawn;
                break;
            case NPCState.Despawning:
                currentState = NPCState.Despawning;
                break;
        }
        foreach (var item in nsf.itemsList)
        {
            Item i = ItemsBase.GetItem(item);
            if(i != null)
            {
                itemList.Add(i);
            }
        }
        timer = nsf.timer;
        skin_id = nsf.skin_id;
        SpawnSkin(skin_id);
        nma.enabled = true;
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
