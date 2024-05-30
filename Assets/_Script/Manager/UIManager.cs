using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum UIType
{
    PauseMenu,
    InventoryMenu,
}

[System.Serializable]
public struct DictionaryUIElements
{
    public UIType type;
    public GameObject instance;
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIType,GameObject> uiDict = new Dictionary<UIType, GameObject>();
    [SerializeField] List<DictionaryUIElements> uiDictVisual = new List<DictionaryUIElements>();

    protected override void Awake()
    {
        base.Awake();

        foreach(DictionaryUIElements ui in uiDictVisual)
        {
            uiDict.Add(ui.type, ui.instance);
        }

        Debug.Log($"{uiDict.Count} units preloaded successfully");
    }

    #region Enable/Disable
    public void Enable(UIType type)
    {
        if (uiDict.ContainsKey(type))
        {
            uiDict[type].SetActive(true);
        }
        else
        {
            Debug.Log($"{type} don't exsist in Manager");
        }
    }

    public void Disable(UIType type)
    {
        if (uiDict.ContainsKey(type))
        {
            uiDict[type].SetActive(false);
        }
        else
        {
            Debug.Log($"{type} don't exsist in Manager");
        }
    }

    public bool IsActive(UIType type)
    {
        if (uiDict.ContainsKey(type))
        {
            return uiDict[type].activeSelf;
        }
        else
        {
            Debug.Log($"{type} don't exsist in Manager");
            return false;
        }
    }
    #endregion
}
