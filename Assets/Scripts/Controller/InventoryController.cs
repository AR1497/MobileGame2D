using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IItemsRepository _itemsRepository;
    private readonly IInventoryView _inventoryWindowView;
    private List<ItemConfig> itemsConfig;
    private InventoryModel inventoryModel;

    public InventoryController(
    [NotNull] IInventoryModel inventoryModel,
    [NotNull] IItemsRepository itemsRepository,
    [NotNull] IInventoryView inventoryView)
    {
        _inventoryModel
        = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
        _itemsRepository
        = itemsRepository ?? throw new ArgumentNullException(nameof(itemsRepository));
        _inventoryWindowView = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
        _inventoryWindowView.Init(_itemsRepository.Items.Values.ToList());


    }

    public void HideInventory()
    {
        _inventoryWindowView.Display();
    }

    public void ShowInventory(Action callback)
    {
        _inventoryWindowView.Display();
    }
}
