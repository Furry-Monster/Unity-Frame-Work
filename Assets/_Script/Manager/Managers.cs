using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : StaticInstance<Managers>
{
    //list of managers(for visual in editor)
    [SerializeField] private List<IManager> managers = new List<IManager>();

    //queue for storing manager need initialing
    private Queue<IManager> managerInitQueue;

    protected override void Awake()
    {
        base.Awake();

        // Initialize managers here
        ReloadAllManagersRequest();

        // Start initialing managers
        InstantiateAllManagers();
    }

    #region reusable
    private void ReloadAllManagersRequest()
    {
        //reset queue
        managerInitQueue = new Queue<IManager>(managers);
    }

    private void InstantiateAllManagers()
    {
        while (managerInitQueue.Count > 0)
        {
            //Instantiate manager
            IManager manager = managerInitQueue.Peek();
            InstantiateManagerExecution(manager, (success) =>
            {
                if (success)
                {
                    managerInitQueue.Dequeue();
                }
                else
                {
                    throw new Exception("Failed to create " + manager.GetType().Name + " execution");
                }
            });
        }
    }

    private void InstantiateManagerExecution(IManager manager, Action<bool> callback)
    {
        GameObject executionEntity = null;

        //Create execution entity
        if (GameObject.Find(manager.GetType().Name))
        {
            Debug.LogWarning("Manager " + manager.GetType().Name + " has already been initialized.");
            callback(false);
            return;
        }
        executionEntity = new GameObject(manager.GetType().Name);
        executionEntity.transform.SetParent(this.transform);


        //Add manager script to entity
        executionEntity.AddComponent(manager.GetType());
        callback(true);
    }
    #endregion
}
