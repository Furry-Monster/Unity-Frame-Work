using System.Collections.Generic;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要是管理UI的显示和隐藏，以及UI的实例化
//          这里的UIType枚举类型主要是用来区分不同的UI，比如PauseMenu，InventoryMenu，HealthBar等等。
//          这里的UIManager脚本主要是管理UI的显示和隐藏，以及UI的实例化，UI的实例化是通过一个Dictionary来进行管理的，其中Dictionary的Key是UIType，Value是GameObject。
//          
//          Notice: 这里我用了一个trick，List存储字典元素，
//                  使得在编辑器中可以看到所有的UIType和对应的GameObject，
//                  这样就可以在编辑器中方便地进行UI的选择和管理。
//==========================

public enum UIType
{
    PauseMenu,
    InventoryMenu,
    HealthBar
}

[System.Serializable]
public struct UIInstance
{
    public UIType type;
    public GameObject instance;
}

public class UIManager : Singleton<UIManager>,IManager
{
    public Dictionary<UIType, GameObject> uiDict = new Dictionary<UIType, GameObject>();
    [SerializeField] List<UIInstance> uiDictVisual = new List<UIInstance>();

    #region internal
    //Init
    public void Init()
    {
        foreach (UIInstance ui in uiDictVisual)
        {
            uiDict.Add(ui.type, ui.instance);
        }

        Debug.Log($"{uiDict.Count}/{uiDictVisual.Count} UI elements loaded successfully");

        Debug.Log("UIManager initialized successfully");
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
