using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    void Apply(IAbilityActivator activator);
}
public interface IAbilityActivator
{
    GameObject GetViewObject();
}
public interface IAbilityRepository
{
    IReadOnlyDictionary<int, IAbility> AbilityMapByItemId { get; }
}