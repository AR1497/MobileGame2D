using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShedController : BaseController
{
    private readonly Car _car;
    private readonly UpgradeHandlersRepository _upgradeHandlersRepository;
    private readonly ItemsRepository _upgradeItemsRepository;
    private readonly InventoryModel _inventoryModel;
    private readonly InventoryView _inventoryView;
    private readonly InventoryController _inventoryController;
    #region Life cycle
    public ShedController(
    [NotNull] List<UpgradeItemConfig> upgradeItemConfigs,
    [NotNull] Car car)
    {
        if (upgradeItemConfigs == null) throw new
        ArgumentNullException(nameof(upgradeItemConfigs));
        _car = car ?? throw new ArgumentNullException(nameof(car));
        _upgradeHandlersRepository = new UpgradeHandlersRepository(upgradeItemConfigs);
        AddController(_upgradeHandlersRepository);
        _upgradeItemsRepository = new ItemsRepository(upgradeItemConfigs.Select(value =>
        value.itemConfig).ToList());
        AddController(_upgradeItemsRepository);
        _inventoryModel = new InventoryModel();
        _inventoryView = new InventoryView();
        _inventoryController = new InventoryController(_inventoryModel, _upgradeItemsRepository, _inventoryView);
        AddController(_inventoryController);
    }

    #region IShedController
    public void Enter()
    {
        _inventoryController.ShowInventory(Exit);
        Debug.Log($"Enter: car has speed : {_car.Speed}");
    }
    public void Exit()
    {
        UpgradeCarWithEquippedItems(
        _car, _inventoryModel.GetEquippedItems(), _upgradeHandlersRepository.UpgradeItems);
        Debug.Log($"Exit: car has speed : {_car.Speed}");
    }
    private void UpgradeCarWithEquippedItems(
    IUpgradableCar upgradableCar,
    IReadOnlyList<IItem> equippedItems,
    IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
        foreach (var equippedItem in equippedItems)
        {
            if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
            {
                handler.Upgrade(upgradableCar);
            }
        }
    }
    #endregion
}
#endregion