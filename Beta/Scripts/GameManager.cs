using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<ItemGameObject> getObjects;
    public List<Transform> npcLoadersPoints;
    public List<Transform> npcHomePoints;
    public List<BoxGameObject> notUnloadBoxesGameObjects;
    public List<BoxGameObject> boxesInStorage;
    public List<DropZone> dropZones;
    public List<Storage> storages;
    public List<GroceryRack> groceryRacks;
    public MainShopManager mainShopManager;
    public List<NPCLoader> npcLoaders;
    public List<NPCCashier> npcCashiers;
    public List<GameObject> npcSkins;
    public DropZone notEmptyZone;
    public Spawner spawner;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        bool trig = true;
        foreach (var item in dropZones)
        {
            if(!item.OnDeliver && item.IsNotEmpty())
            {
                trig = false;
                notEmptyZone = item;
            }
        }
        if (trig)
            notEmptyZone = null;
    }

    public ItemGameObject GetClosestObject(Vector3 d)
    {
        ItemGameObject otg = null;
        float s = -1;
        float l = -1;
        foreach (var item in getObjects)
        {
            l = Vector3.Distance(item.transform.position, d);
            if(s<0 || l < s)
            {
                otg = item;
                s = l;
            }
        }

        return otg;
    }

    public ItemGameObject GetClosestObject(Vector3 d, List<Item> itemList)
    {
        ItemGameObject otg = null;
        float s = -1;
        float l = -1;
        foreach (var item in getObjects)
        {
            if (!itemList.Contains(item.item))
                continue;
            l = Vector3.Distance(item.transform.position, d);
            if (s < 0 || l < s)
            {
                otg = item;
                s = l;
            }
        }

        return otg;
    }

    public Getter GetGetter(NPC npc)
    {
        if (mainShopManager.currentShopManager == null)
            return null;
        List<int> getrs = new List<int>();
        int min_count = int.MaxValue;
        foreach (var item in mainShopManager.currentShopManager.getters)
        {
            if(min_count > item.npcs.Count)
            {
                min_count = item.npcs.Count;
            }
            getrs.Add(item.npcs.Count);
        }
        float min_dist = float.MaxValue;
        float t;
        Getter tg;
        Getter g = null;
        for(int i=0;i<getrs.Count;i++)
        {
            if(getrs[i] == min_count)
            {
                tg = mainShopManager.currentShopManager.getters[i];
                t = Vector3.Distance(npc.transform.position, tg.transform.position);
                if(g==null)
                {
                    g = tg;
                    min_dist = t;
                }
                else
                {
                    if (min_dist > t)
                    {
                        g = tg;
                        min_dist = t;
                    }
                }
            }
        }
        return g;
    }

    public Getter GetGetterForCashier(Vector3 pos, NPCCashier npcc)
    {
        Getter g = null;
        float m = float.MaxValue;
        foreach (var item in mainShopManager.currentShopManager.getters)
        {
            if(item.npccashier == null)
            {
                if(Vector3.Distance(pos, item.cashierZone.position) < m)
                {
                    m = Vector3.Distance(pos, item.cashierZone.position);
                    g = item;
                }
            }
        }
        return g;
    }

    public static Transform GetLoaderPoint()
    {
        if(Instance.npcLoadersPoints.Count > 0)
        {
            Transform t = Instance.npcLoadersPoints[Random.Range(0, Instance.npcLoadersPoints.Count)];
            Instance.npcLoadersPoints.Remove(t);
            return t;
        }
        return null;
    }

    public static Transform GetHomePoint()
    {
        if (Instance.npcHomePoints.Count > 0)
        {
            Transform t = Instance.npcHomePoints[Random.Range(0, Instance.npcHomePoints.Count)];
            return t;
        }
        return null;
    }

    public static BoxGameObject GetClosestBox(Vector3 pos)
    {
        BoxGameObject bgo = null;
        float m = float.MaxValue;
        foreach (var item in Instance.notUnloadBoxesGameObjects)
        {
            if(Vector3.Distance(pos, item.transform.position) < m && (item.status == "RedyForLoad" || item.status == "Droped"))
            {
                m = Vector3.Distance(pos, item.transform.position);
                bgo = item;
            }
        }
        return bgo;
    }

    public static Storage GetStorage(Vector3 pos, BoxGameObject bgo)
    {
        Storage st = null;
        float m = float.MaxValue;
        float t;
        foreach (var item in Instance.storages)
        {
            Item i = item.GetItem();
            if(i == bgo.Item && !item.isFull())
            {
                t = Vector3.Distance(pos, item.transform.position);
                if(t < m)
                {
                    m = t;
                    st = item;
                }
            }
        }
        if(st != null)
        {
            return st;
        }
        foreach (var item in Instance.storages)
        {
            Item i = item.GetItem();
            if (i == null && !item.isFull())
            {
                t = Vector3.Distance(pos, item.transform.position);
                if (t < m)
                {
                    m = t;
                    st = item;
                }
            }
        }
        return st;
    }

    public void OnDay()
    {
        foreach (var item in npcLoaders)
        {
            item.OnDay();
        }
        foreach (var item in npcCashiers)
        {
            item.OnDay();
        }
    }

    public static GroceryRack GetGrocery(Vector3 pos, BoxGameObject bgo)
    {
        GroceryRack gr = null;
        List<GroceryRack> grs;
        int priority = -1;
        float mdist = float.MaxValue;
        float t;
        grs = Instance.groceryRacks.FindAll(x => x.Type == bgo.Item.Category);
        foreach (var item in grs)
        {
            if (item.GetItem() != null && item.GetItem().Id == bgo.Item.Id && item.emptyCount() >= bgo.Item.CountInBox)
            {
                if (priority <= 1)
                {
                    priority = 2;
                    mdist = Vector3.Distance(item.transform.position, pos);
                    gr = item;
                }
                if (priority == 2)
                {
                    t = Vector3.Distance(item.transform.position, pos);
                    if (t < mdist)
                    {
                        mdist = t;
                        gr = item;
                    }
                }
            }
            if(item.isFullEmpty())
            {
                t = Vector3.Distance(item.transform.position, pos);
                if(priority < 0)
                {
                    priority = 0;
                    mdist = t;
                    gr = item;
                }
                if(priority == 0)
                {
                    if (t < mdist)
                    {
                        mdist = t;
                        gr = item;
                    }
                }
            }
        }
        return gr;
    }

    public static BoxGameObject FoundWhatCanBeFill(Vector3 pos)
    {
        BoxGameObject bgo = null;
        GroceryRack gr = null;
        foreach (var item in Instance.boxesInStorage)
        {
            gr = GetGrocery(pos, item);
            if (gr != null)
                return item;
        }
        return bgo;
    }

    public void ToEditMode()
    {
        mainShopManager.currentShopManager.ToEditMode(true);
        UIManager.ToEditMode(true);
    }

    public void CloseEditMode()
    {
        mainShopManager.currentShopManager.ToEditMode(false);
        UIManager.ToEditMode(false);
    }

    public Item[] GetExistItems()
    {
        Item itm = null;
        List<Item> items = new List<Item>();
        foreach (var item in mainShopManager.currentShopManager.storagePlacers)
        {
            itm = ((GroceryRack)item.placeZoneIn).GetItem();
            if (itm != null && !items.Contains(itm))
            {
                items.Add(itm);
            }
        }
        return items.ToArray();
    }
}
