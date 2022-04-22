using System.Collections.Generic;

public class InventoryModel : IInventoryModel
{
    private readonly IReadOnlyList<IItem> _stubCollection = new List<IItem>();
    private readonly List<IItem> _equippedItems = new List<IItem>();
    
    #region Methods
    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _equippedItems ?? _stubCollection;
    }
    public void EquipItem(IItem item)
    {
        if (_equippedItems.Contains(item)) return;
        _equippedItems.Add(item);
    }
    public void UnequipItem(IItem item)
    {
        if (!_equippedItems.Contains(item)) return;
        _equippedItems.Remove(item);
    }
    #endregion
}
