using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SaveSystem;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instant;
    public GameObject npcPrefab;
    public GameObject npcLoaderPrefab;
    public GameObject npcCashierPrefab;
    public bool loadOnAwake = true;
    public bool debugSave = false;
    public bool debugLoad = false;
    private void Awake()
    {
        Instant = this;
    }

    private void Start()
    {
        if (loadOnAwake)
        {
            Load();
        }
    }

    public static MainSaveFile GetMainSaveFile()
    {
        if (File.Exists(GetSaveFileUrl()))
        {
            return JsonUtility.FromJson<MainSaveFile>(File.ReadAllText(GetSaveFileUrl()));
        }
        return null;
    }

    private void FixedUpdate()
    {
        if(debugLoad)
        {
            debugLoad = false;
            Load();
        }
        if(debugSave)
        {
            debugSave = false;
            Save();
        }
    }

    public void Load()
    {
        /*if(File.Exists(GetSaveFileUrl()))
        {
            MainSaveFile msf = JsonConvert.DeserializeObject<MainSaveFile>(File.ReadAllText(GetSaveFileUrl()));
            if (SceneManager.GetActiveScene().buildIndex == msf.scene_id)
            {
                try
                {
                    PlayerScript ps = GameObject.FindObjectOfType<PlayerScript>();
                    if (ps != null)
                    {
                        ps.Load(msf.psf);
                    }
                }
                catch(Exception e)
                {

                }

                try
                {
                    MoneyManager mm = GameObject.FindObjectOfType<MoneyManager>();
                    if (mm != null)
                    {
                        mm.Load(msf.esf);
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    DayNightManager dnm = GameObject.FindObjectOfType<DayNightManager>();
                    if (dnm != null)
                    {
                        dnm.Load(msf.dnsf);
                    }
                }
                catch (Exception e)
                {

                }


                try
                {
                    DropZoneManager dzm = GameObject.FindObjectOfType<DropZoneManager>();
                    if (dzm != null)
                    {
                        dzm.Load(msf.dzmsf);
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    foreach (var item in msf.boxSaveFiles)
                    {
                        Item i = ItemsBase.GetItem(item.id);
                        if (i != null)
                        {
                            GameObject o = Instantiate(i.BoxesPrefab);
                            o.GetComponent<BoxGameObject>().Load(item, i);
                        }
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    foreach (var item in msf.npcSaveFiles)
                    {
                        GameObject o = Instantiate(npcPrefab);
                        o.GetComponent<NPC>().Load(item);
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    foreach (var item in msf.dropedItemsSaveFiles)
                    {
                        Item i = ItemsBase.GetItem(item.id);
                        if(i != null)
                        {
                            GameObject o = Instantiate(i.ItemPrefab);
                            o.GetComponent<ItemGameObject>().OnDrop();
                        }
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    GroceryRack[] grs = (GroceryRack[])UnityEngine.Object.FindObjectsOfType(Type.GetType("GroceryRack"));
                    foreach (var item in grs)
                    {
                        item.Load(msf.groceryRackFiles[item.myId]);
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    Storage[] s = (Storage[])UnityEngine.Object.FindObjectsOfType(Type.GetType("Storage"));
                    foreach (var item in s)
                    {
                        item.Load(msf.storeageSaveFiles[item.myId]);
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    NPCLoader[] nl = (NPCLoader[])UnityEngine.Object.FindObjectsOfType(Type.GetType("NPCLoader"));
                    List<NPCLoader> nls = new List<NPCLoader>(nl);
                    foreach (var item in msf.npcLoaderSaveFile)
                    {
                        NPCLoader n = nls.Find(x => x.myId == item.Key);
                        if(n == null)
                        {
                            GameObject o = Instantiate(npcLoaderPrefab);
                            n = o.GetComponent<NPCLoader>();
                            n.SetID(item.Key);
                        }
                        n.Load(item.Value);
                    
                    }
                }
                catch (Exception e)
                {

                }

                try
                {
                    NPCCashier[] nc = (NPCCashier[])UnityEngine.Object.FindObjectsOfType(Type.GetType("NPCCashier"));
                    List<NPCCashier> ncs = new List<NPCCashier>(nc);
                    foreach (var item in msf.npcCashierSaveFile)
                    {
                        NPCCashier n = ncs.Find(x => x.myId == item.Key);
                        if (n == null)
                        {
                            GameObject o = Instantiate(npcCashierPrefab);
                            n = o.GetComponent<NPCCashier>();
                            n.SetID(item.Key);
                        }
                        n.Load(item.Value);
                    }
                }
                catch (Exception e)
                {

                }
            }
        }*/
    }

    public void Save()
    {
        /*MainSaveFile msf = new MainSaveFile();
        msf.scene_id = SceneManager.GetActiveScene().buildIndex;
        try
        {
            PlayerScript ps = GameObject.FindObjectOfType<PlayerScript>();
            PlayerSaveFile psf = null;
            if(ps != null)
            {
                psf = new PlayerSaveFile();
                ps.Save(psf);
            }
            msf.psf = psf;
        }
        catch (Exception e)
        {

        }

        try
        {
            MoneyManager mm = GameObject.FindObjectOfType<MoneyManager>();
            EconomicSaveFile esf = null;
            if(mm != null)
            {
                esf = new EconomicSaveFile();
                mm.Save(esf);
            }
            msf.esf = esf;
        }
        catch (Exception e)
        {

        }

        try
        {
            DayNightManager dnm = GameObject.FindObjectOfType<DayNightManager>();
            DayNightSaveFile dnsf = null;
            if(dnm != null)
            {
                dnsf = new DayNightSaveFile();
                dnm.Save(dnsf);
            }
            msf.dnsf = dnsf;
        }
        catch (Exception e)
        {

        }

        try
        {
            DropZoneManager dzm = GameObject.FindObjectOfType<DropZoneManager>();
            DropZoneManagerSaveFile dzms = null;
            if (dzm != null)
            {
                dzms = new DropZoneManagerSaveFile();
                dzm.Save(dzms);
            }
            msf.dzmsf = dzms;
        }
        catch (Exception e)
        {

        }

        try
        {
            BoxGameObject[] bgos = (BoxGameObject[])UnityEngine.Object.FindObjectsOfType(Type.GetType("BoxGameObject"));
            foreach (var item in bgos)
            {
                if (item.status != "None" && item.status != "OnPlayer" && item.status != "InNPCHands")
                {
                    BoxSaveFile bsf = new BoxSaveFile();
                    item.Save(bsf);
                    msf.boxSaveFiles.Add(bsf);
                }
            }
        }
        catch (Exception e)
        {

        }

        try
        {
            NPC[] npc = (NPC[])UnityEngine.Object.FindObjectsOfType(Type.GetType("NPC"));
            foreach (var item in npc)
            {
                NPCSaveFile npcsf = new NPCSaveFile();
                item.Save(npcsf);
                msf.npcSaveFiles.Add(npcsf);
            }
        }
        catch (Exception e)
        {

        }

        try
        {
            ItemGameObject[] igos = (ItemGameObject[])UnityEngine.Object.FindObjectsOfType(Type.GetType("ItemGameObject"));
            foreach (var item in igos)
            {
                if (item.status == "Droping")
                {
                    ItemGameObjectSaveFile igosf = new ItemGameObjectSaveFile();
                    item.Save(igosf);
                    msf.dropedItemsSaveFiles.Add(igosf);
                }
            }
        }
        catch (Exception e)
        {

        }

        try
        {
            GroceryRack[] grs = (GroceryRack[])UnityEngine.Object.FindObjectsOfType(Type.GetType("GroceryRack"));
            foreach (var item in grs)
            {
                GroceryRackSaveFile grsf = new GroceryRackSaveFile();
                item.Save(grsf);
                msf.groceryRackFiles.Add(item.myId, grsf);
            }
        }
        catch (Exception e)
        {

        }

        try
        {
            Storage[] s = (Storage[])UnityEngine.Object.FindObjectsOfType(Type.GetType("Storage"));
            foreach (var item in s)
            {
                StorageSaveFile ssf = new StorageSaveFile();
                item.Save(ssf);
                msf.storeageSaveFiles.Add(item.myId, ssf);
            }
        }
        catch (Exception e)
        {

        }

        try
        {
            NPCLoader[] nl = (NPCLoader[])UnityEngine.Object.FindObjectsOfType(Type.GetType("NPCLoader"));
            foreach (var item in nl)
            {
                NPCLoaderSaveFile nlsf = new NPCLoaderSaveFile();
                item.Save(nlsf);
                msf.npcLoaderSaveFile.Add(item.myId, nlsf);
            }
        }
        catch (Exception e)
        {

        }

        try
        {
            NPCCashier[] nc = (NPCCashier[])UnityEngine.Object.FindObjectsOfType(Type.GetType("NPCCashier"));
            foreach (var item in nc)
            {
                NPCCashierSaveFile ncsf = new NPCCashierSaveFile();
                item.Save(ncsf);
                msf.npcCashierSaveFile.Add(item.myId, ncsf);
            }
        }
        catch (Exception e)
        {

        }

        File.WriteAllText(GetSaveFileUrl(), JsonConvert.SerializeObject(msf, Formatting.Indented, new JsonSerializerSettings
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        }));*/
    }

    public static void ClearSaves()
    {
        if (File.Exists(GetSaveFileUrl()))
        {
            File.Delete(GetSaveFileUrl());
        }
    }

    public static string GetSaveFileUrl()
    {
        return Application.persistentDataPath + Path.DirectorySeparatorChar + "save.json";
    }
}
