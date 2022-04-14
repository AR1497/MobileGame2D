using System.Collections.Generic;

public class InventoryModel : IInventoryModel
{
    private static readonly List<IItem> _emptyCollection = new List<IItem>();
    private readonly List<IItem> _items = new List<IItem>();
    #region Methods
    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _items ?? _emptyCollection;
    }
    public void EquipItem(IItem item)
    {
        if (_items.Contains(item)) return;
        _items.Add(item);
    }
    public void UnequipItem(IItem item)
    {
        if (!_items.Contains(item)) return;
        _items.Remove(item);
    }
    #endregion
}
