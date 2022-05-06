using Model.Analytic;
using Profile;
using System.Collections.Generic;
using System.Linq;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField] private Transform _uiRoot;

    [SerializeField] private UnityAdsTools _ads;
    [SerializeField] private List<ItemConfig> _items;
    [SerializeField] private UpgradeItemConfigDataSource _upgradeSource;
    [SerializeField] private List<AbilityItemConfig> _abilityItems;
    [SerializeField]
    private Transform _placeForUi;
    [SerializeField]
    private DailyRewardView _dailyRewardView;
    [SerializeField]
    private CurrencyView _currencyView;
    [SerializeField]
    private FightWindowView _fightWindowView;
    [SerializeField]
    private StartFightView _startFightView;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    private ProfilePlayer _playerProfile;
    private PlayerData _playerData;

    private void Awake()
    {
        _analyticsTools = new UnityAnalyticTools();
        ProfilePlayer profilePlayer = new ProfilePlayer(15f, new UnityAdsTools(), new UnityAnalyticTools());
        profilePlayer.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_placeForUi, _playerProfile, _items, _upgradeSource.ItemConfigs.ToList(), _abilityItems, _dailyRewardView, _currencyView, _fightWindowView, _startFightView);
    }

    private void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
