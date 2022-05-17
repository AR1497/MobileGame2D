using System;
using System.Collections.Generic;

public interface IAbilityCollectionView
{
    event EventHandler<IItem> UseRequested;
    void Display(IReadOnlyList<IItem> abilityItems);

    void Show();
    void Hide();
}