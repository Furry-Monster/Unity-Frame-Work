using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要是管理单位（包括角色、怪物、实体）的注册和生成，
//          包括实体的注册（CRUD）、生成、销毁、获取等功能。
//==========================
public class UnitManager : Singleton<UnitManager>
{
    //UnitList(shouldn't be able to visit directly,just for setting in editor)
    [SerializeField] private List<UnitInstance> units = new List<UnitInstance>();
    public List<UnitInstance> Units
    {
        get
        {
            UpdateUnitVisual();
            return units;
        }
    }

    //UnitDict
    private Dictionary<string, UnitInstance> unitDict = new Dictionary<string, UnitInstance>();

    //default spawn parent
    [SerializeField] private Transform defaultParent;

    protected override void Awake()
    {
        base.Awake();

        if (defaultParent == null)
        {
            defaultParent = GameObject.Find("Environment").GetComponent<Transform>().Find("_Units");
        }

    }

    #region CRUD
    public void AddUnit(UnitInstance unit)
    {
        if (HasUnit(unit)) return;

        unitDict.Add(unit.unitData.unitName, unit);
    }

    public void RemoveUnit(string unitID)
    {
        if (HasUnit(unitID))
        {
            unitDict.Remove(unitID);
        }
    }

    public void UpdateUnit(UnitInstance unit)
    {
        if (HasUnit(unit))
        {
            unitDict[unit.unitData.unitName] = unit;
        }
    }

    public bool HasUnit(UnitInstance unit)
    {
        return unitDict.ContainsKey(unit.unitData.unitName);
    }
    public bool HasUnit(string unitID)
    {
        return unitDict.ContainsKey(unitID);
    }
    public UnitInstance GetUnit(string unitID)
    {
        return unitDict[unitID];
    }
    #endregion

    #region internal
    //init
    internal void Init()
    {
        //load unit data from list
        foreach (UnitInstance unit in units)
        {
            unitDict.Add(unit.unitData.unitName, unit);
        }
        Debug.Log($"{unitDict.Count}/{units.Count} units loaded successfully");
    }

    #region Spawn
    internal void SpawnUnit(string unitID,Transform parent,Vector3 pos = default,Quaternion rot = default)
    {
        if (unitDict.ContainsKey(unitID))
        {
            UnitInstance unit = unitDict[unitID];
            var instance = Instantiate(unit.unitData.unitPrefab, parent);

            instance.transform.position = pos;
            instance.transform.rotation = rot;
        }
    }

    internal void SpawnUnit(string unitID)
    {
        SpawnUnit(unitID, defaultParent);
    }
    #endregion

    #endregion

    #region Editor
    private void UpdateUnitVisual()
    {
        units = unitDict.Values.ToList();
    }

    #endregion
}
