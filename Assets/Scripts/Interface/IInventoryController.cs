using System;
using System.Collections.Generic;

public interface IInventoryController
{
    IReadOnlyList<IItem> GetEquippedItems();
    void ShowInventory(Action callback);
    void HideInventory();
}
