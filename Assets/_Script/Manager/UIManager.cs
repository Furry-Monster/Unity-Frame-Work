using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    PauseMenu,
    InventoryMenu,
    HealthBar
}

[System.Serializable]
public struct DictionaryUIElements
{
    public UIType type;
    public GameObject instance;
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIType, GameObject> uiDict = new Dictionary<UIType, GameObject>();
    [SerializeField] List<DictionaryUIElements> uiDictVisual = new List<DictionaryUIElements>();

    #region internal
    //Init
    internal void Init()
    {
        foreach (DictionaryUIElements ui in uiDictVisual)
        {
            uiDict.Add(ui.type, ui.instance);
        }

        Debug.Log($"{uiDict.Count}/{uiDictVisual.Count} UI elements loaded successfully");
    }
    //Enable
    internal void Enable(UIType type)
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
    //Disable
    internal void Disable(UIType type)
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
