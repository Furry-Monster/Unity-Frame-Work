using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          ����ű���Ҫ���ڹ���Resrouces�ļ������Դ
//          
//          �������������AssetBundleManager.cs�����ǹ������Դ����һ���Ĳ���
//==========================
public class ResourceManager : Singleton<ResourceManager>,ISystem
{
    #region internal 
    public void Init()
    {
        Debug.Log("Resource Manager initialized successfully");
    }

    #endregion
}
