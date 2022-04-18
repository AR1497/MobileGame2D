using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRepository : BaseController, IAbilityRepository
{
    public IReadOnlyDictionary<int, IAbility> AbilitiesMap { get => _abilitiesMap; }

    private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();

    public AbilityRepository(IReadOnlyList<AbilityItemConfig> abilities)
    {
        foreach (var config in abilities)
        {
            _abilitiesMap[config.Id] = CreateAbility(config);
        }
    }

    private IAbility CreateAbility(AbilityItemConfig config)
    {
        switch (config.Type)
        {
            case AbilityType.None:
                return AbilityStub.Default;
            case AbilityType.Gun:
                return new GunAbility(config.View, config.value);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class AbilityStub : IAbility
{
    public static AbilityStub Default { get; } = new AbilityStub();

    public void Apply(IAbilityActivator activator)
    {
    }
}