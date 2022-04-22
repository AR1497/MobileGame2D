using System;
using System.Collections.Generic;

public interface IAbilityCollectionView : IView
{
    event EventHandler<IItem> UseRequested;
    void Display(IReadOnlyList<IItem> abilityItems);
}

public interface IAbilitiesController
{
    void ShowAbilities();
}