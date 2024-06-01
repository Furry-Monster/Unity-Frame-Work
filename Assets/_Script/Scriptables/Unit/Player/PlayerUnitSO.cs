using UnityEngine;

[CreateAssetMenu(fileName = "New Player Unit", menuName = "ScriptableObjects/UnitSO/PlayerUnitSO")]
public class PlayerUnitSO : BaseUnitSO
{

    [Header("Player Health")]
    public int maxHealth;

    [Header("Player Locomotion")]
    public float walkSpeed;
    public float runSpeed;
    public float acceleration;
    public float decceleration;
    [Space]
    public float turnSpeed;

    [Header("Player Interaction")]
    public float interactionDistance;
    public LayerMask interactLayer;
}

