using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : Singleton<ResourceSystem>
{
    //Dictionaries and Lists of resources should be assembled here

    protected override void Awake()
    {
        base.Awake();

        AssembleResources();
    }

    private void AssembleResources()
    {
        //TODO:Assemble Dictionaries and Lists of resources
    }
}
