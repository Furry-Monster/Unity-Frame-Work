using UnityEngine;

public abstract class BaseUnit : MonoBehaviour
{
    const string PATH = "ScriptableObjects/Unit/";

    //Data of the unit should be set up in the editor,
    //you can check and set it in UnitManager panel
    protected BaseUnitSO unitData { get; private set; }

    protected virtual void Awake()
    {
        if (unitData == null)
        {
            unitData = Resources.Load<BaseUnitSO>(PATH + GetType().Name.Replace("Unit", ""));
        }
    }
}
