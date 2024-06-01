using System.Collections.Generic;
using UnityEngine;


public class BaseItem
{
    public ItemSO basicData { get; private set; }

    [Header("Extend Datas")]
    public List<BaseExtendsSO> extendsDatas = new List<BaseExtendsSO>();
}
