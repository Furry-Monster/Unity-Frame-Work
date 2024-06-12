using UnityEditor.SearchService;
using UnityEngine;

public class Systems : PersistentSingleton<Systems>
{
    [SerializeField] private Managers managers;

    protected override void Awake()
    {
        base.Awake();

        //critical!
        InitSystems();
    }

    

    private void InitSystems()
    {
        // Initialize systems here
        for (int i = 0; i < transform.childCount; i++)
        {
            ISystem system = transform.GetChild(i).GetComponent<ISystem>();

            if (system != null)
            {
                system.Init();
            }
        }
        Debug.Log("All Systems initialized");

        //managers should be initialized after all systems
        managers.gameObject.SetActive(true);
    }
}
