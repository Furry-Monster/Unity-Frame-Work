using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本管理游戏场景
//          包括场景的加载，后退，卸载等功能
//          采用栈的方式存储场景，实现后退功能
//          采用异步加载的方式加载场景，防止卡顿  
//==========================


public class GameSceneManager : Singleton<GameSceneManager>,ISystem
{
    //stack to store the Scene loaded(not include current scene,I regard currentScene as applied scene)
    //thus my stack should organized like this:
    //|||||||[MainMenu, Home, WorldMap(previous scene)  -->   current_scene   -->  next_scene
    private Stack<string> sceneStack = new Stack<string>();

    private string nextScene;
    private string currentScene;
    private string previousScene;

    private AsyncOperation nextSceneLoadRequest;

    protected override void Awake()
    {
        base.Awake();
    }

    #region internal
    public void Init()
    {
        //register event
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        currentScene = SceneManager.GetActiveScene().name;
        previousScene = currentScene;

        Debug.Log("SceneManager Initialized successfully");
    }

    internal void ChangeToScene(string sceneName)
    {
        nextScene = sceneName;

        StartCoroutine(PreloadNextScene((success) =>
        {
            if (success)
            {
                EnterNextScene();
            }
            else
            {
                Debug.LogError($"Failed to load scene: {sceneName}");
            }
        }));
    }

    internal void GoBack()
    {
        if (sceneStack.Count > 0)
        {
            nextScene = PopScene();
            StartCoroutine(PreloadNextScene((success) =>
            {
                if (success)
                {
                    EnterNextScene();
                }
                else
                {
                    Debug.LogError($"Failed to load scene: {nextScene}");
                }
            }));
        }
        else
        {
            Debug.LogWarning("No previous scene to go back.");
        }
    }

    #endregion

    #region CRUD
    private void PushScene(string sceneName)
    {
        sceneStack.Push(sceneName);
    }

    private string PopScene()
    {
        if (sceneStack.Count > 0)
        {
            return sceneStack.Pop();
        }
        else
        {
            return "Quit";
        }
    }

    #endregion

    #region Reusable
    private IEnumerator PreloadNextScene(Action<bool> callback)
    {
        nextSceneLoadRequest = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);
        nextSceneLoadRequest.allowSceneActivation = false;

        //set a timer to check if time is out
        Singleton<TimerSystem>.Instance.Schedule((timeout) =>
        {
            if (!nextSceneLoadRequest.isDone)
            {
                callback(false);
                StopCoroutine(PreloadNextScene(callback));
            }
        }, 3f);

        //wait for the scene to be loaded
        while (!nextSceneLoadRequest.isDone)
        {
            yield return null;
        }

        //callback
        callback(true);
    }

    private void EnterNextScene()
    {
        //check if is loaded,though it's unnecessary
        if (IsNextSceneLoaded())
        {
            Debug.LogWarning("Next scene is not loaded yet.");
            return;
        }

        //set ref
        previousScene = currentScene;
        currentScene = nextScene;

        PushScene(previousScene);

        //enter the next scene
        nextSceneLoadRequest.allowSceneActivation = true;
    }

    private bool IsNextSceneLoaded()
    {
        return (nextSceneLoadRequest != null && nextSceneLoadRequest.isDone);
    }

    #endregion

    #region Debugger
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded: " + scene.name);
    }
    private void OnSceneUnloaded(Scene scene)
    {
        Debug.Log("Scene unloaded: " + scene.name);
    }
    #endregion
}
