using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShedController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Inventory" };
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly Car _car;
    private readonly UpgradeHandlersRepository _upgradeRepository;
    public readonly ItemsRepository _upgradeItemsRepository;
    private readonly InventoryController _inventoryController;
    private readonly InventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private IReadOnlyList<UpgradeItemConfig> upgradeItems;
    private List<ItemConfig> itemsConfig;
    private Car currentCar;

    public ShedController([NotNull] IReadOnlyList<UpgradeItemConfig> upgradeItems, [NotNull] Car car, [NotNull] Transform placeForUi, InventoryModel inventoryModel)
    {
        _inventoryView = LoadView(placeForUi);
        if (upgradeItems == null) throw new ArgumentNullException(nameof(upgradeItems));
        _car = car ?? throw new ArgumentNullException(nameof(car));
        _upgradeRepository = new UpgradeHandlersRepository(upgradeItems);
        AddController(_upgradeRepository);
        _upgradeItemsRepository = new ItemsRepository(upgradeItems.Select(value => value._itemConfig).ToList());
        AddController(_upgradeItemsRepository);
        _inventoryModel = inventoryModel;
        _inventoryController = new InventoryController(_inventoryModel, _upgradeItemsRepository, _inventoryView);
        AddController(_inventoryController);
        Enter();
    }

    #region IShedController
    public void Enter()
    {
        _inventoryController.ShowInventory(Exit);
        Debug.Log($"Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        var model = _upgradeItemsRepository.Items.Values.ToList();
        UpgradeCarWithEquipedItems(_car, model, _upgradeRepository.UpgradeItems);
        Debug.Log($"Exit, car speed = {_car.Speed}");
        _inventoryController.HideInventory();
    }

    private void UpgradeCarWithEquipedItems(IUpgradableCar car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
        foreach (var item in equiped)
        {
            if (upgradeHandlers.TryGetValue(item.Id, out var handler))
                handler.Upgrade(car);
        }
    }
    #endregion

    public IInventoryView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObject(objectView);
        return objectView.GetComponent<InventoryView>();
    }

    protected override void OnDispose()
    {
        base.OnDispose();
    }
}