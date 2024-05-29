using UnityEngine;

public class UnitManager : Singleton<UnitManager>
{


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


    #region SpawnUnit

    public void SpawnUnit(BaseUnitSO unit, Vector3 pos, Quaternion rot)
    {
        Instantiate(unit.unitPrefab, pos, rot);
        Debug.Log($"spawn a {unit.unitName} at {pos}");
    }
    public void SpawnUnit(BaseUnitSO unit, Vector3 pos)
    {
        SpawnUnit(unit, pos, Quaternion.identity);
    }
    public void SpawnUnit(BaseUnitSO unit)
    {
        SpawnUnit(unit, Vector3.zero, Quaternion.identity);
    }


    public void SpawnUnitAtTransform(BaseUnitSO unit, Transform parent, Vector3 pos, Quaternion rot)
    {
        GameObject spawnedUnit;
        spawnedUnit = Instantiate(unit.unitPrefab, parent);

        spawnedUnit.transform.position = pos;
        spawnedUnit.transform.rotation = rot;

        Debug.Log($"spawn a {unit.unitName} under {parent.name}");
    }
    public void SpawnUnitAtTransform(BaseUnitSO unit, Transform parent, Vector3 pos)
    {
        SpawnUnitAtTransform(unit, parent, pos, Quaternion.identity);
    }
    public void SpawnUnitAtTransform(BaseUnitSO unit, Transform parent)
    {
        SpawnUnitAtTransform(unit, parent, Vector3.zero, Quaternion.identity);
    }
    #endregion


    #region ClearUnit

    public void ClearUnitsInType<T>() where T : BaseUnit
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
    public void ClearAllUnitsInScene()
    {
        BaseUnit[] units = FindObjectsOfType<BaseUnit>();

        foreach (BaseUnit unitInstance in units)
        {
            Destroy(unitInstance.gameObject);
        }
    }
    #endregion
}
