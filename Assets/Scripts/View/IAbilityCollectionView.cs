using System;
using System.Collections.Generic;

namespace Assets.Scripts.View
{
    public interface IAbilityCollectionView
    {
        event EventHandler<IItem> UseRequested;
        void Display(IReadOnlyList<IItem> abilityItems);
    }
}