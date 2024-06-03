using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Player,
    Enemy,
    NPC,
    Entity,//for example, a bucket or a chest
}

[System.Serializable]
public struct DictionaryUnitElement
{
    public UnitType type;
    public BaseUnitSO unitSO;
}

public class UnitManager : Singleton<UnitManager>
{
    //UnitDict
    public Dictionary<UnitType, BaseUnitSO> unitDict = new Dictionary<UnitType, BaseUnitSO>();
    [SerializeField] private List<DictionaryUnitElement> unitDictVisual = new List<DictionaryUnitElement>();
    //this list is used to make dictionary visible in editor
    //and it must be serialized to save data,and send data to the real dictionary so that it can be used in game
    [Space]

    [SerializeField] private Transform defaultParent;

    protected override void Awake()
    {
        base.Awake();

        if (defaultParent == null)
        {
            defaultParent = GameObject.Find("Environment").GetComponent<Transform>().Find("Units");
        }

        //load unitDict from visual list
        foreach (DictionaryUnitElement unitElement in unitDictVisual)
        {
            unitDict.Add(unitElement.type, unitElement.unitSO);
        }

        Debug.Log($"{unitDict.Count} units preloaded successfully");
    }

    
    //CRUD(Create,Read,Update,Delete)
    

    //Internal
    #region SpawnUnit
    //spawn units under a parent
    //You're suggested to pick a parent under folder "Environment/Units"
    internal void SpawnUnitAtTransform(BaseUnitSO unit, Transform parent, Vector3 pos, Quaternion rot)
    {
        GameObject spawnedUnit;
        spawnedUnit = Instantiate(unit.unitPrefab, parent);

        spawnedUnit.transform.position = pos;
        spawnedUnit.transform.rotation = rot;

        Debug.Log($"spawn a {unit.unitName} under {parent.name}");
    }
    internal void SpawnUnitAtTransform(BaseUnitSO unit, Transform parent, Vector3 pos)
    {
        SpawnUnitAtTransform(unit, parent, pos, Quaternion.identity);
    }
    internal void SpawnUnitAtTransform(BaseUnitSO unit, Transform parent)
    {
        SpawnUnitAtTransform(unit, parent, Vector3.zero, Quaternion.identity);
    }

    //spawn units under default parent
    //It's better to use these methods
    internal void SpawnUnit(BaseUnitSO unit)
    {
        SpawnUnitAtTransform(unit, defaultParent);
    }
    internal void SpawnUnit(BaseUnitSO unit, Vector3 pos, Quaternion rot)
    {
        SpawnUnitAtTransform(unit, defaultParent, pos, rot);
    }
    internal void SpawnUnit(BaseUnitSO unit, Vector3 pos)
    {
        SpawnUnitAtTransform(unit, defaultParent, pos);
    }

    //spawn units at worldspace
    //Better not use these methods
    internal void SpawnUnitAtWorldspace(BaseUnitSO unit, Vector3 pos, Quaternion rot)
    {
        Instantiate(unit.unitPrefab, pos, rot);
        Debug.Log($"spawn a {unit.unitName} at {pos}");
    }
    internal void SpawnUnitAtWorldspace(BaseUnitSO unit, Vector3 pos)
    {
        SpawnUnitAtWorldspace(unit, pos, Quaternion.identity);
    }
    internal void SpawnUnitAtWorldspace(BaseUnitSO unit)
    {
        SpawnUnitAtWorldspace(unit, Vector3.zero, Quaternion.identity);
    }

    #endregion
    #region ClearUnit

    internal void ClearUnitsInType<T>() where T : BaseUnit
    {
        T[] units = FindObjectsOfType<T>();

        foreach (T unitInstance in units)
        {
            Destroy(unitInstance.gameObject);
        }
    }

    /// <summary>
    /// Be careful when using this function, it will destroy all units in the scene.
    /// </summary>
    internal void ClearAllUnitsInScene()
    {
        BaseUnit[] units = FindObjectsOfType<BaseUnit>();

        foreach (BaseUnit unitInstance in units)
        {
            Destroy(unitInstance.gameObject);
        }
    }
    #endregion

    //Utilities
    #region UnitCount
    public int CountUnitInType<T>() where T : BaseUnit
    {
        T[] units = FindObjectsOfType<T>();

        return units.Length;
    }

    public int CountUnitInScene()
    {
        BaseUnit[] units = FindObjectsOfType<BaseUnit>();

        return units.Length;
    }
    #endregion
}
