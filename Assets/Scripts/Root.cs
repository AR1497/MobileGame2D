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

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    private ProfilePlayer _playerProfile;

    private void Awake()
    {
        _playerProfile = new ProfilePlayer(15f, _ads, _analyticsTools);
        _analyticsTools = new UnityAnalyticTools();
        _playerProfile.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_uiRoot, _playerProfile, _items, _upgradeSource.ItemConfigs.ToList(), _abilityItems.AsReadOnly());
    }


    private void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
