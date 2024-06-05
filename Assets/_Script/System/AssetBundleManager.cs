using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要是为热重载服务，
//          管理了AssetBundle的加载和卸载，以及Asset的加载和卸载。
//==========================

public class AssetBundleManager : Singleton<AssetBundleManager>
{
    private AssetBundle mainBundle;
    private AssetBundleManifest manifest;
    /// <summary>
    /// path of assetbundles
    /// </summary>
    private string assetBundlePathUrl
    {
        get
        {
            return Application.streamingAssetsPath + "/";
        }
    }

    private Dictionary<string, AssetBundle> loadedBundlesDict = new Dictionary<string, AssetBundle>();


    #region internal
    internal void Init()
    {
        //load main bundle
        if (mainBundle == null)
        {
            mainBundle = AssetBundle.LoadFromFile(assetBundlePathUrl + "main");
            manifest = mainBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        }
    }

    //Bundle Operations
    #region LoadBundle
    internal void LoadBundle(string bundleName)
    {
        //get all dependencies
        string[] dependenciesPath = manifest.GetDirectDependencies(bundleName);
        //load dependencies if not loaded
        foreach (string dependencyPath in dependenciesPath)
        {
            if (!loadedBundlesDict.ContainsKey(dependencyPath))
            {
                AssetBundle loadedBundle = AssetBundle.LoadFromFile(assetBundlePathUrl + dependencyPath);
                loadedBundlesDict.Add(dependencyPath, loadedBundle);
            }
        }

        //loaded the required bundle if not loaded
        if (!loadedBundlesDict.ContainsKey(bundleName))
        {
            AssetBundle bundle = AssetBundle.LoadFromFile(assetBundlePathUrl + bundleName);
        }
    }
    internal void LoadBundleAsync(string bundleName, System.Action<bool> isSuccessCallback)
    {
        //get all dependencies
        string[] dependenciesPath = manifest.GetDirectDependencies(bundleName);
        //load dependencies if not loaded
        foreach (string dependencyPath in dependenciesPath)
        {
            if (!loadedBundlesDict.ContainsKey(dependencyPath))
            {
                AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetBundlePathUrl + dependencyPath);
                request.completed += (obj) =>
                {
                    loadedBundlesDict.Add(dependencyPath, request.assetBundle);
                    isSuccessCallback(true);
                };
            }
        }

        //loaded the required bundle if not loaded
        if (!loadedBundlesDict.ContainsKey(bundleName))
        {
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(assetBundlePathUrl + bundleName);
            request.completed += (obj) =>
            {
                loadedBundlesDict.Add(bundleName, request.assetBundle);
                isSuccessCallback(true);
            };
        }
    }


    #endregion
    #region UnloadBundle
    internal void UnloadBundle(string bundleName)
    {
        if (loadedBundlesDict.ContainsKey(bundleName))
        {
            //param set to false to prevent unload all assets in bundle
            loadedBundlesDict[bundleName].Unload(false);
            loadedBundlesDict.Remove(bundleName);
        }
    }
    internal void ClearBundles()
    {
        foreach (string bundleName in loadedBundlesDict.Keys)
        {
            loadedBundlesDict[bundleName].Unload(false);
        }
        loadedBundlesDict.Clear();
    }

    #endregion

    //Asset Operations
    #region LoadAsset

    //-----------Load Asset-------------


    /// <summary>
    /// load asset from bundle
    /// </summary>
    /// <param name="bundleName">from which bundle to load</param>
    /// <param name="assetName">from which asset to load</param>
    /// <returns>loaded asset</returns>
    internal Object LoadAsset(string bundleName, string assetName)
    {
        //load bundle first
        LoadBundle(bundleName);

        //then load asset
        return loadedBundlesDict[bundleName].LoadAsset(assetName);
    }
    /// <summary>
    /// load asset from bundle and cast to T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <returns>loaded asset</returns>
    internal T LoadAsset<T>(string bundleName, string assetName) where T : Object
    {
        return LoadAsset(bundleName, assetName) as T;
    }
    /// <summary>
    /// load asset from bundle and cast to T
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    internal Object LoadAsset(string bundleName, string assetName, System.Type type)
    {
        //load bundle first
        LoadBundle(bundleName);

        //then load asset
        return loadedBundlesDict[bundleName].LoadAsset(assetName, type);
    }



    //-----------Async Load Asset-------------


    /// <summary>
    /// load asset from bundle async
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <param name="assetCallback"></param>
    internal void LoadAssetAsync(string bundleName, string assetName, System.Action<Object> assetCallback)
    {
        //load bundle async first
        LoadBundleAsync(bundleName, (success) =>
        {
            //then load asset
            Object asset = loadedBundlesDict[bundleName].LoadAsset(assetName);
            assetCallback(asset);
        });
    }
    /// <summary>
    /// load asset from bundle async and cast to T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <param name="assetCallback"></param>
    internal void LoadAssetAsync<T>(string bundleName, string assetName, System.Action<T> assetCallback) where T : Object
    {
        LoadAssetAsync(bundleName, assetName, (asset) =>
        {
            assetCallback(asset as T);
        });
    }
    /// <summary>
    /// load asset from bundle async and cast to T
    /// </summary>
    /// <param name="bundleName"></param>
    /// <param name="assetName"></param>
    /// <param name="type"></param>
    /// <param name="assetCallback"></param>
    internal void LoadAssetAsync(string bundleName, string assetName, System.Type type, System.Action<Object> assetCallback)
    {
        //load bundle first
        LoadBundleAsync(bundleName, (success) =>
        {
            //then load asset
            Object asset = loadedBundlesDict[bundleName].LoadAsset(assetName, type);
            assetCallback(asset);
        });
    }

    //-----------Async Load Asset By Coroutine-------------
    //---->enumerators
    private IEnumerator LoadAssetCoroutine(string bundleName, string assetName, System.Action<Object> assetCallback)
    {
        //load bundle first(not async)
        LoadBundle(bundleName);

        //then load asset
        AssetBundleRequest request = loadedBundlesDict[bundleName].LoadAssetAsync(assetName);
        yield return request;

        //callback
        assetCallback(request.asset);
    }
    private IEnumerator LoadAssetCoroutine<T>(string bundleName, string assetName, System.Action<T> assetCallback) where T : Object
    {
        yield return LoadAssetCoroutine(bundleName, assetName, (asset) =>
        {
            assetCallback(asset as T);
        });
    }
    private IEnumerator LoadAssetCoroutine(string bundleName, string assetName, System.Type type, System.Action<Object> assetCallback)
    {
        //load bundle first(not async)
        LoadBundle(bundleName);

        //then load asset
        AssetBundleRequest request = loadedBundlesDict[bundleName].LoadAssetAsync(assetName, type);
        yield return request;

        //callback
        assetCallback(request.asset);
    }

    //----->starters
    internal void LoadAssetByCoroutine(string bundleName, string assetName, System.Action<Object> assetCallback)
    {
        StartCoroutine(LoadAssetCoroutine(bundleName, assetName, assetCallback));
    }
    internal void LoadAssetByCoroutine<T>(string bundleName, string assetName, System.Action<T> assetCallback) where T : Object
    {
        StartCoroutine(LoadAssetCoroutine<T>(bundleName, assetName, assetCallback));
    }
    internal void LoadAssetByCoroutine(string bundleName, string assetName, System.Type type, System.Action<Object> assetCallback)
    {
        StartCoroutine(LoadAssetCoroutine(bundleName, assetName, type, assetCallback));
    }


    #endregion
    
    #endregion
}
