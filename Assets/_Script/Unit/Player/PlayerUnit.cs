using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : BaseUnit
{
    private InputManager input;

    private void Awake()
    {
        Debug.Log("PlayerUnit spawned");
        input = Singleton<InputManager>.Instance;
    }

    private void OnEnable()
    {
        input.EnableInput(this);
    }

    private void OnDisable()
    {
        input.DisableInput(this);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
}
