using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShedController : BaseController
{
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly Car _car;
    private readonly UpgradeHandlersRepository _upgradeRepository;
    private readonly InventoryController _inventoryController;
    private readonly InventoryModel _model;

    public ShedController(IReadOnlyList<UpgradeItemConfig> upgradeItems, List<ItemConfig> items, Car car)
    {
        _upgradeItems = upgradeItems;
        _car = car;
        _upgradeRepository = new UpgradeHandlersRepository(upgradeItems);

        _model = new InventoryModel();
        AddController(_upgradeRepository);
        _inventoryController = new InventoryController(items, _model);
        AddController(_inventoryController);
    }

    #region IShedController
    public void Enter()
    {
        _inventoryController.ShowInventory();
        Debug.Log($"Enter, car speed = {_car.Speed}");
    }

    public void Exit()
    {
        UpgradeCarWithEquipedItems(_car, _model.GetEquippedItems(), _upgradeRepository.UpgradeItems);
        Debug.Log($"Exit, car speed = {_car.Speed}");
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
}