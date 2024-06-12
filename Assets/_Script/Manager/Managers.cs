using System;
using System.Collections.Generic;
using UnityEngine;

public class Managers : StaticInstance<Managers>
{

    private void OnEnable()
    {
        ResetManagers();
    }

    private void OnDisable()
    {
        
    }


    private void ResetManagers()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            IManager manager = transform.GetChild(i).GetComponent<IManager>();

            if (manager != null)
            {
                manager.Init();
            }
        }

        Debug.Log("All Managers initialized");
    
        
    }
}
