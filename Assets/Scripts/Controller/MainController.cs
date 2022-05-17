using Assets.Scripts;
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
    private DailyRewardController _dailyRewardController;
    private FightWindowController _fightWindowController;
    private CurrencyController _currencyController;
    private StartFightController _startFightController;
    private Car _car;
    private readonly Transform _placeForUi;
    private readonly ProfilePlayer _profilePlayer;
    private readonly PlayerData _player;
    private readonly List<ItemConfig> _itemsConfig;
    private readonly IAdsShower _adsShower;
    private readonly IReadOnlyList<UpgradeItemConfig> _upgradeItems;
    private readonly IReadOnlyList<AbilityItemConfig> _abilityItems;
    private readonly DailyRewardView _dailyRewardView;
    private readonly CurrencyView _currencyView;
    private readonly FightWindowView _fightWindowView;
    private readonly StartFightView _startFightView;

    public MainController(Transform placeForUi, ProfilePlayer profilePlayer,
         List<ItemConfig> itemsConfig, List<UpgradeItemConfig> upgradeItems,
         IReadOnlyList<AbilityItemConfig> abilityItems, DailyRewardView dailyRewardView, CurrencyView currencyView,
         FightWindowView fightWindowView, StartFightView startFightView)
    {
        _profilePlayer = profilePlayer;
        _placeForUi = placeForUi;
        _itemsConfig = itemsConfig;
        _upgradeItems = upgradeItems;
        _abilityItems = abilityItems;

        _dailyRewardView = dailyRewardView;
        _currencyView = currencyView;
        _fightWindowView = fightWindowView;
        _startFightView = startFightView;

        OnChangeGameState(_profilePlayer.CurrentState.Value);
        profilePlayer.CurrentState.SubscribeOnChange(OnChangeGameState);
        _inventoryModel = new InventoryModel();

        if (_shedController == null)
        {
            _shedController = new ShedController(_upgradeItems, profilePlayer.CurrentCar, placeForUi, _inventoryModel);
            _shedController.Exit();
        }
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
                _mainMenuController = new MainMenuController(_placeForUi, _profilePlayer);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_player, _abilityItems, _inventoryModel, _placeForUi);
                var abilityRepository = new AbilityRepository(_abilityItems);
                var abilityView = Object.Instantiate(ResourceLoader.LoadPrefab(new ResourcePath() { PathResource = "Prefabs/AbilityView" }), _placeForUi).GetComponent<IAbilityCollectionView>();
                var carController = new CarController();
                AddController(carController);
                var invModel = new InventoryModel();
                var abilitiesController = new AbilitiesController(carController, invModel, abilityRepository, abilityView, _shedController._upgradeItemsRepository);
                abilitiesController.ShowAbilities();
                AddController(abilitiesController);
                _mainMenuController?.Dispose();
                break;
            case GameState.Shed: if (_shedController == null)
                    _shedController = new ShedController(_upgradeItems, _car, _placeForUi, _inventoryModel);
                else _shedController.Enter();
                break;
            case GameState.DailyReward:
                _dailyRewardController = new DailyRewardController(_placeForUi,
                _dailyRewardView, _currencyView);
                _dailyRewardController.RefreshView();
                break;
            case GameState.Fight:
                _fightWindowController = new FightWindowController(_placeForUi,
                _fightWindowView, _profilePlayer);
                _fightWindowController.RefreshView();
                _mainMenuController?.Dispose();
                _startFightController?.Dispose();
                _gameController?.Dispose();
                break;
            default:
                AllClear();
                break;
        }
    }

    private void AllClear()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _fightWindowController?.Dispose();
        _dailyRewardController?.Dispose();
        _startFightController?.Dispose();
    }
}
