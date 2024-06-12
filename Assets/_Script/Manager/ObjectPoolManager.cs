using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : Singleton<ObjectPoolManager>,IManager
{
    #region internal
    public void Init()
    {
        Debug.Log("ObjectPoolManager Init successfully");
    }

    #endregion
}
