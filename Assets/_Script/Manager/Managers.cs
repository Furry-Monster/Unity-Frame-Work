using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : StaticInstance<Managers>
{
    
    //Connect to managers
    private GameManager gameManager => Singleton<GameManager>.Instance;
    private InputManager inputManager => Singleton<InputManager>.Instance;
    private UnitManager unitManager => Singleton<UnitManager>.Instance;
    private ItemManager itemManager => Singleton<ItemManager>.Instance;
    private UIManager uiManager => Singleton<UIManager>.Instance;

    //flags
    public bool isPause = false;

}
