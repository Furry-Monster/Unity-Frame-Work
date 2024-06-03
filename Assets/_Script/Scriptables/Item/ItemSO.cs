using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/ItemSO/BaseItem")]
public class ItemSO : ScriptableObject
{
    [Header("text info")]
    public string itemName;
    [TextArea]
    public string itemDescription;

    [Header("2d info")]
    public Sprite itemIcon;

    [Header("3d info")]
    public GameObject itemPrefab;

    [Header("data info")]
    public int itemValue;
    public float itemWeight;
}
