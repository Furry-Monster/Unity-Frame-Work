using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          ����ű���Ҫ���ڹ���Resrouces�ļ������Դ
//          
//          �������������AssetBundleManager.cs�����ǹ������Դ����һ���Ĳ���
//==========================
public class ResourceManager : Singleton<ResourceManager>
{

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

    #region internal 
    internal void Init()
    {

    }

    #endregion
}
