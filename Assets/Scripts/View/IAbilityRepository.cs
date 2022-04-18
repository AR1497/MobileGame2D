using System.Collections.Generic;

namespace Assets.Scripts.View
{
    public interface IAbilityRepository
    {
        IReadOnlyDictionary<int, IAbility> AbilitiesMap { get; }
    }
}