using System.Collections.Generic;
using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{
    //UnitList
    [SerializeField] private List<UnitInstance> units = new List<UnitInstance>();

    //UnitDict
    public Dictionary<int, UnitInstance> unitDict = new Dictionary<int, UnitInstance>();

    //default spawn parent
    [SerializeField] private Transform defaultParent;

    protected override void Awake()
    {
        base.Awake();

        if (defaultParent == null)
        {
            defaultParent = GameObject.Find("Environment").GetComponent<Transform>().Find("_Units");
            //better set previously
        }

    }

    #region CRUD
    public void AddUnit(UnitInstance unit)
    {
        if (HasUnit(unit)) return;

        units.Add(unit);
        unitDict.Add(unit.unitData.unitName.GetHashCode(), unit);
    }

    public void RemoveUnit(UnitInstance unit)
    {
        if (HasUnit(unit))
        {
            units.Remove(unit);
            unitDict.Remove(unit.unitData.unitName.GetHashCode());
        }
    }
    public void RemoveUnitById(int id)
    {
        if (HasUnit(id))
        {
            UnitInstance unit = GetUnitById(id);
            units.Remove(unit);
            unitDict.Remove(id);
        }
    }

    public void UpdateUnit(UnitInstance unit)
    {
        if (HasUnit(unit))
        {
            unitDict[unit.unitData.unitName.GetHashCode()] = unit;
        }
    }

    public bool HasUnit(UnitInstance unit)
    {
        return unitDict.ContainsKey(unit.unitData.unitName.GetHashCode());
    }
    public bool HasUnit(int id)
    {
        return unitDict.ContainsKey(id);
    }
    public UnitInstance GetUnitById(int id)
    {
        return unitDict[id];
    }
    public List<UnitInstance> GetAllUnits()
    {
        return units;
    }
    #endregion

    #region internal
    //init
    internal void Init()
    {
        //load unit data from list
        foreach (UnitInstance unit in units)
        {
            unitDict.Add(unit.unitData.unitName.GetHashCode(), unit);
        }
        Debug.Log($"{unitDict.Count}/{units.Count} units loaded successfully");
    }

    #region SpawnUnit
    //spawn units under a parent
    //You're suggested to pick a parent under folder "Environment/Units"
    internal void SpawnUnitAtTransform(UnitInstance unit, Transform parent, Vector3 pos, Quaternion rot)
    {
        GameObject spawnedUnit;
        spawnedUnit = Instantiate(unit.unitData.unitPrefab, parent);

        spawnedUnit.transform.position = pos;
        spawnedUnit.transform.rotation = rot;

        Debug.Log($"spawn a {unit.unitData.unitName} under {parent.name}");
    }
    internal void SpawnUnitAtTransform(UnitInstance unit, Transform parent, Vector3 pos)
    {
        SpawnUnitAtTransform(unit, parent, pos, Quaternion.identity);
    }
    internal void SpawnUnitAtTransform(UnitInstance unit, Transform parent)
    {
        SpawnUnitAtTransform(unit, parent, Vector3.zero, Quaternion.identity);
    }

    //spawn units under default parent
    //It's better to use these methods
    internal void SpawnUnit(UnitInstance unit)
    {
        SpawnUnitAtTransform(unit, defaultParent);
    }
    internal void SpawnUnit(UnitInstance unit, Vector3 pos, Quaternion rot)
    {
        SpawnUnitAtTransform(unit, defaultParent, pos, rot);
    }
    internal void SpawnUnit(UnitInstance unit, Vector3 pos)
    {
        SpawnUnitAtTransform(unit, defaultParent, pos);
    }

    //spawn units at worldspace
    //Better not use these methods
    internal void SpawnUnitAtWorldspace(UnitInstance unit, Vector3 pos, Quaternion rot)
    {
        Instantiate(unit.unitData.unitPrefab, pos, rot);
        Debug.Log($"spawn a {unit.unitData.unitName} at {pos}");
    }
    internal void SpawnUnitAtWorldspace(UnitInstance unit, Vector3 pos)
    {
        SpawnUnitAtWorldspace(unit, pos, Quaternion.identity);
    }
    internal void SpawnUnitAtWorldspace(UnitInstance unit)
    {
        SpawnUnitAtWorldspace(unit, Vector3.zero, Quaternion.identity);
    }

    #endregion

    #endregion

}
