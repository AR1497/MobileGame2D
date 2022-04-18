using JetBrains.Annotations;
using System;
using System.Collections.Generic;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IItemsRepository _itemsRepository;
    private readonly IInventoryView _inventoryWindowView;
    public InventoryController(List<ItemConfig> itemConfigs, InventoryModel inventoryModel)
    {
        _inventoryModel = inventoryModel;
        _inventoryWindowView = new InventoryView();
        _itemsRepository = new ItemsRepository(itemConfigs);
    }

    public void HideInventory()
    {
        throw new NotImplementedException();
    }

    public void ShowInventory(Action callback)
    {
        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryWindowView.Display(equippedItems);
    }

    public void ShowInventory()
    {
        foreach (var item in _itemsRepository.Items.Values)
            _inventoryModel.EquipItem(item);

        var equippedItems = _inventoryModel.GetEquippedItems();
        _inventoryWindowView.Display(equippedItems);
    }
}
