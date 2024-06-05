using System.Collections.Generic;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          ����ű���Ҫ�ǹ���UI����ʾ�����أ��Լ�UI��ʵ����
//          �����UITypeö��������Ҫ���������ֲ�ͬ��UI������PauseMenu��InventoryMenu��HealthBar�ȵȡ�
//          �����UIManager�ű���Ҫ�ǹ���UI����ʾ�����أ��Լ�UI��ʵ������UI��ʵ������ͨ��һ��Dictionary�����й���ģ�����Dictionary��Key��UIType��Value��GameObject��
//          
//          Notice: ����������һ��trick��List�洢�ֵ�Ԫ�أ�
//                  ʹ���ڱ༭���п��Կ������е�UIType�Ͷ�Ӧ��GameObject��
//                  �����Ϳ����ڱ༭���з���ؽ���UI��ѡ��͹���
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

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIType, GameObject> uiDict = new Dictionary<UIType, GameObject>();
    [SerializeField] List<UIInstance> uiDictVisual = new List<UIInstance>();

    #region internal
    //Init
    internal void Init()
    {
        foreach (UIInstance ui in uiDictVisual)
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
