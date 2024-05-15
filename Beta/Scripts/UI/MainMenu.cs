using SaveSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public LoadScene loadScene;
    
    
    public void Continue()
    {
        MainSaveFile msf = SaveManager.GetMainSaveFile();
        if (msf != null)
        {
            loadScene.scene_id = msf.scene_id;
            loadScene.gameObject.SetActive(true);
            loadScene.Load();
        }
    }

    public void NewGame()
    {
        SaveManager.ClearSaves();
        loadScene.scene_id = 1;
        loadScene.gameObject.SetActive(true);
        loadScene.Load();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
