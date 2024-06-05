using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          ����ű���Ҫ���ڹ���Resrouces�ļ������Դ
//          
//          �������������AssetBundleManager.cs�����ǹ������Դ����һ���Ĳ���
//==========================
public class ResourceSystem : Singleton<ResourceSystem>
{
    //ResourceSystem should have all the reference of managers,so that it can allocate and release resources
    private InputManager inputManager => Singleton<InputManager>.Instance;
    private UnitManager unitManager => Singleton<UnitManager>.Instance;
    private ItemManager itemManager => Singleton<ItemManager>.Instance;
    private UIManager uiManager => Singleton<UIManager>.Instance;



    protected override void Awake()
    {
        base.Awake();

        AssembleResources();
    }

    #region Assemble Resource

    private void AssembleResources()
    {

        Debug.Log("Assemble Resources Finished");
    }
    #endregion

}
