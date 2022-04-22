using Company.Project.Features;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRepository : IRepository<int, IAbility>
{
    public IReadOnlyDictionary<int, IAbility> AbilitiesMap { get => _abilitiesMap; }

    public IReadOnlyDictionary<int, IAbility> AbilityMapByItemId => throw new NotImplementedException();

    private Dictionary<int, IAbility> _abilitiesMap = new Dictionary<int, IAbility>();

    public AbilityRepository(List<AbilityItemConfig> itemConfigs)
    {

        PopulateItems(ref _abilitiesMap, itemConfigs);
    }

    private void PopulateItems(
    ref Dictionary<int, IAbility> upgradeHandlersMapByType,
    List<AbilityItemConfig> configs)
    {
        foreach (var config in configs)
        {
            if (upgradeHandlersMapByType.ContainsKey(config.Id)) continue;
            upgradeHandlersMapByType.Add(config.Id, CreateAbilityByType(config));
        }
    }


    private IAbility CreateAbility(AbilityItemConfig config)
    {
        switch (config.type)
        {
            case AbilityType.None:
                return AbilityStub.Default;
            case AbilityType.Gun:
                return new GunAbility(config.view, config.value);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public IReadOnlyDictionary<int, IAbility> Collection => _abilitiesMap;
}

public class AbilityStub : IAbility
{
    public static AbilityStub Default { get; } = new AbilityStub();

    public void Apply(IAbilityActivator activator)
    {
    }
}