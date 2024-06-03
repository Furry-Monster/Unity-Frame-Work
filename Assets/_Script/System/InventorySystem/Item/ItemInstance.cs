using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemInstance
{
    public ItemSO basicData;

    [Header("Extend Datas")]
    public List<BaseExtendsSO> extendsDatas = new List<BaseExtendsSO>();
}
