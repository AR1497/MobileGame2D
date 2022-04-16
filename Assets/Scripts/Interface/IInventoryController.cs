using System;

public interface IInventoryController
{
    void ShowInventory(Action callback);
    void HideInventory();
}
