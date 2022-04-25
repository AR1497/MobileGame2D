using Company.Project.Features;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShedController : BaseController, IShedController
{
    private readonly IUpgradable _upgradable;
    private readonly IRepository<int, IUpgradeCarHandler> _upgradeHandlersRepository;
    private readonly IInventoryController _inventoryController;

    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Item" };
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly Car _car;
    private readonly UpgradeHandlersRepository _upgradeRepository;
    private readonly ItemsRepository _upgradeItemsRepository;
    private readonly InventoryModel _inventoryModel;
    private readonly IInventoryView _inventoryView;
    private IReadOnlyList<UpgradeItemConfig> upgradeItems;
    private List<ItemConfig> itemsConfig;
    private Car currentCar;

    public ShedController(
        [NotNull] IRepository<int, IUpgradeCarHandler> upgradeHandlersRepository,
        [NotNull] IInventoryController inventoryController,
        [NotNull] IUpgradable upgradable,
        [NotNull] IReadOnlyList<UpgradeItemConfig> upgradeItems, 
        [NotNull] Car car, 
        [NotNull] Transform placeForUi, 
        InventoryModel inventoryModel)
    {
        _inventoryView = LoadView(placeForUi);
        if (upgradeItems == null) throw new ArgumentNullException(nameof(upgradeItems));
        _car = car ?? throw new ArgumentNullException(nameof(car));
        _upgradeRepository = new UpgradeHandlersRepository(upgradeItems);
        AddController(_upgradeRepository);
        _upgradeItemsRepository = new ItemsRepository(upgradeItems.Select(value => value._itemConfig).ToList());
        AddController(_upgradeItemsRepository);
        _inventoryModel = inventoryModel;
        Enter();

        _upgradeHandlersRepository = upgradeHandlersRepository ?? throw new
        ArgumentNullException(nameof(upgradeHandlersRepository));
        _inventoryController = inventoryController ?? throw new
        ArgumentNullException(nameof(inventoryController)); ;
        _upgradable = upgradable ?? throw new ArgumentNullException(nameof(upgradable));
    }

    private void AddController(UpgradeHandlersRepository upgradeRepository)
    {
        throw new NotImplementedException();
    }

    #region IShedController
    public void Enter()
    {
        _inventoryController.ShowInventory(Exit);
    }

    public void Exit()
    {
        var model = _upgradeItemsRepository.Items.Values.ToList();
        UpgradeCarWithEquipedItems(_upgradable, _inventoryController.GetEquippedItems(),
        _car, model, _upgradeRepository.UpgradeItems);
        Debug.Log($"Exit, car speed = {_car.Speed}");
        _inventoryController.HideInventory();
    }

    private void UpgradeCarWithEquipedItems(
        IUpgradable upgradable,
        IReadOnlyList<IItem> equippedItems,
        IUpgradable car,
        IReadOnlyList<IItem> equiped,
        IReadOnlyDictionary<int, IUpgradeCarHandler> upgradeHandlers)
    {
        foreach (var equippedItem in equippedItems)
        {
            if (upgradeHandlers.TryGetValue(equippedItem.Id, out var handler))
            {
                handler.Upgrade((IUpgradableCar)upgradable);
            }
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