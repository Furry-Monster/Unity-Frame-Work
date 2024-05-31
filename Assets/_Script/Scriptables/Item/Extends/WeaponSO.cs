using UnityEngine;

public enum WeaponType
{
    Melee,
    Remote,
    Misc
}
[CreateAssetMenu(fileName = "WeaponExtends", menuName = "ScriptableObjects/ItemSO/Extensions/WeaponSO")]
public class WeaponSO : BaseExtendsSO
{
    public WeaponType type;
    public int damage;
    public float condition;
}
