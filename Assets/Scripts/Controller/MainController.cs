using Model.Analytic;
using Profile;
using System.Collections.Generic;
using Tools.Ads;
using UnityEngine;

public class MainController : BaseController
{
    private MainMenuController _mainMenuController;
    private ShedController _shedController;
    private GameController _gameController;
    private InventoryController _inventoryController;
    private InventoryModel _inventoryModel;
    private Car _car;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IAdsShower _adsShower;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;
    private readonly UpgradeHandlersRepository _upgradeHandlersRepository;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
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
        _inventoryModel = new InventoryModel();
    }

    protected override void OnDispose()
    {
        AllClear();

        _profilePlayer.CurrentState.UnsubscriptionOnChange(OnChangeGameState);
        base.OnDispose();
    }

    private void OnChangeGameState(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer, _upgradeItems, _car, _itemsConfig);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                var inventoryModel = new InventoryModel();
                _gameController = new GameController(_profilePlayer, _abilityItems, inventoryModel, _placeForUi);
                _mainMenuController?.Dispose();
                break;
            case GameState.Shed: if (_shedController == null)
                    _shedController = new ShedController(_upgradeHandlersRepository, _inventoryController, (IUpgradable)_itemsConfig, (List<UpgradeItemConfig>)_upgradeItems, _car, _placeForUi, _inventoryModel);
                else _shedController.Enter();
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
