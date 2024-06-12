using UnityEngine;

//==========================
// - Created: FurryMonster
// - LastModifiedTime: 2024-6-5 14:21:33
// - Description:
//          这个脚本主要用于管理Resrouces文件里的资源
//          
//          请区分这个它和AssetBundleManager.cs，他们管理的资源存在一定的差异
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
