using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoading : MonoBehaviour
{
    public Transform player;

    private bool isLoaded;
    private bool shouldLoad;

    // Start is called before the first frame update
    void Start()
    {
        // if scene is loaded do not open second time
        if (SceneManager.sceneCount > 0)
        {
            for (int i = 0; i < SceneManager.sceneCount; ++i)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.name == gameObject.name)
                {
                    isLoaded = true;
                }
            }
        }

        LoadScene();
        
    }

    // Update is called once per frame
    
    /*
    void Update()
    {
        TriggerCheck();
    }
    */

    private void TriggerCheck()
    {
        if (shouldLoad)
        {
            LoadScene();
        }
        else
        {
            UnloadScene();
        }
    }

    private void LoadScene()
    {
        if (!isLoaded)
        {
            // SceneManager.LoadSceneAsync(gameObject.name, LoadSceneMode.Additive);
            SceneManager.LoadScene(gameObject.name, LoadSceneMode.Additive);
            isLoaded = true;
        }
    }

    private void UnloadScene()
    {
        if (isLoaded)
        {
            SceneManager.UnloadSceneAsync(gameObject.name);
            isLoaded = false;
        }
    }


    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = true;
        }
    }
    */
    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            shouldLoad = false;
        }
    }
    */
}
