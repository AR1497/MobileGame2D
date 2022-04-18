using System.Collections.Generic;

public class InventoryModel : IInventoryModel
{
    private readonly List<IItem> _items = new List<IItem>();
    #region Methods
    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _items;
    }
    public void EquipItem(IItem item)
    {
        if (_items.Contains(item)) return;
        _items.Add(item);
    }
    public void UnequipItem(IItem item)
    {
        _items.Remove(item);
    }
    #endregion
}
