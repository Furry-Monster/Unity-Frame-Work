using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : BaseUnit
{
    [SerializeField] private InputManager input;

    private void Awake()
    {
        input = FindObjectOfType<InputManager>();
    }

    private void OnEnable()
    {
        input.EnableInput(this);
    }

    private void OnDisable()
    {
        input.DisableInput(this);
    }
}
