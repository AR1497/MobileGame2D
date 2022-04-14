private void OnAbilityUseRequested(object sender, IItem e)
{
    if (_abilityRepository.AbilityMapByItemId.TryGetValue(e.Id, out var ability))
        ability.Apply(_abilityActivator);
}