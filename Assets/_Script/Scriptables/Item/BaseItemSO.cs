using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New BaseItem", menuName = "ScriptableObjects/BaseItem")]
public class BaseItemSO : ScriptableObject
{
    [Header("text info")]
    public string itemName;
    public int itemID;
    [TextArea]
    public string itemDescription;

    [Header("2d info")]
    public Sprite itemIcon;

    [Header("3d info")]
    public GameObject itemPrefab;

    [Header("data info")]
    public int itemValue;
    public float itemWeight;

    [Header("Extends info")]
    public List<BaseExtendsSO> decoratorSO = new List<BaseExtendsSO>();
}
