using Company.Project.Features;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;

public class InventoryController : BaseController, IInventoryController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IRepository<int, IItem> _itemsRepository;
    private readonly IInventoryView _inventoryWindowView;
    private List<ItemConfig> itemsConfig;
    private InventoryModel inventoryModel;
    private Action _hideAction;
    private ItemsRepository itemsRepository;
    private ItemConfig _itemsConfig;
    private InventoryView inventoryView;
    private ItemConfig itemsConfig1;

    public InventoryController([NotNull] IRepository<int, IItem> itemsRepository, List<ItemConfig> itemsConfig, IInventoryModel inventoryModel, IInventoryView inventoryView)
    {
        _itemsConfig = itemsConfig;
        _itemsRepository = itemsRepository ?? throw new ArgumentNullException(nameof(itemsRepository));
        _inventoryModel = inventoryModel ?? throw new ArgumentNullException(nameof(inventoryModel));
        _inventoryWindowView = inventoryView ?? throw new ArgumentNullException(nameof(inventoryView));
        SetupView(_inventoryWindowView);
    }

    public InventoryController(ItemsRepository itemsRepository, ItemConfig itemsConfig1, InventoryModel inventoryModel, InventoryView inventoryView)
    {
        this.itemsRepository = itemsRepository;
        this.itemsConfig1 = itemsConfig1;
        this.inventoryModel = inventoryModel;
        this.inventoryView = inventoryView;
    }

    protected override void OnDispose()
    {
        CleanupView();
        base.OnDispose();
    }

    public IReadOnlyList<IItem> GetEquippedItems()
    {
        return _inventoryModel.GetEquippedItems();
    }

    public void HideInventory()
    {
        _inventoryWindowView.Hide();
        _hideAction?.Invoke();
    }

    public void ShowInventory(Action callback)
    {
        _hideAction = callback;
        _inventoryWindowView.Show();
        _inventoryWindowView.Display(_itemsRepository.Collection.Values.ToList());
    }

    private void SetupView(IInventoryView inventoryView)
    {
        inventoryView.Selected += OnItemSelected;
        inventoryView.Deselected += OnItemDeselected;
    }
    private void CleanupView()
    {
        _inventoryWindowView.Selected -= OnItemSelected;
        _inventoryWindowView.Deselected -= OnItemDeselected;
    }
    private void OnItemSelected(IItem item)
    {
        _inventoryModel.EquipItem(item);
    }
    private void OnItemDeselected(IItem item)
    {
        _inventoryModel.UnequipItem(item);
    }

}
