using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRunner
{
    private List<IAbility> passiveAbilities = new List<IAbility>();

    private PlayerUnit player;

    public AbilityRunner(PlayerUnit player)
    {
        this.player = player;
    }

    public void AddPassiveAbility(IAbility ability)
    {
        passiveAbilities.Add(ability);
    }

    public void RemovePassiveAbility(IAbility ability)
    {
        passiveAbilities.Remove(ability);
    }

    
}
