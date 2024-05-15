using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public bool PlayOnAwake = true;
    public int scene_id = 0;
    public Slider loadingSlider;
    private bool alredy = false;

    private void Start()
    {
        if (PlayOnAwake)
        {
            StartCoroutine(LoadSceneAsync(scene_id));
        }
    }

    public void Load()
    {
        if (!alredy)
        {
            alredy = true;
            StartCoroutine(LoadSceneAsync(scene_id));
        }
    }

    IEnumerator LoadSceneAsync(int id)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(id);

        while(!operation.isDone)
        {
            loadingSlider.value = operation.progress;

            yield return null;
        }
    }

}
