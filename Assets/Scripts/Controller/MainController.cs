using Model.Analytic;
using Profile;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;

public class MainController : BaseController
{
    public MainController(Transform placeForUi, PlayerData profilePlayer,
         List<ItemConfig> itemsConfig, IReadOnlyList<UpgradeItemConfig> upgradeItems,
         IReadOnlyList<AbilityItemConfig> abilityItems)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _itemsConfig = itemsConfig;
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
    }

    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private readonly Transform _placeForUi;
    private readonly PlayerData _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnSubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _shedController = new ShedController(_upgradeItems, _itemsConfig, _profilePlayer.CurrentCar);
                _shedController.Enter();
                _shedController.Exit();
                _gameController?.Dispose();
                _inventoryController?.Dispose();
                break;
            case GameState.Game:
                var inventoryModel = new InventoryModel();
                _inventoryController = new InventoryController(_itemsConfig, inventoryModel);
                _inventoryController.ShowInventory();
                _gameController = new GameController(_profilePlayer, _abilityItems, inventoryModel);
                _mainMenuController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _inventoryController?.Dispose();
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
    }
}
