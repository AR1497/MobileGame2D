using System.Collections.Generic;

public interface IInventoryModel
{
    IReadOnlyList<IItem> GetEquippedItems();
    void EquipItem(IItem item);
    void UnequipItem(IItem item);
}