using UnityEngine;

public class BaseUnitSO : ScriptableObject
{
    [Header("text info")]
    public string unitName;
    [TextArea]
    public string unitDescription;

    [Header("2d info")]
    public Sprite unitIcon;

    [Header("3d info")]
    public GameObject unitPrefab;

}
