using Model.Analytic;
using Profile;
using System.Collections.Generic;
using System.Linq;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Transform _uiRoot;
    private PlayerData _playerProfile;
    [SerializeField] private List<ItemConfig> _items;
    [SerializeField] private UpgradeItemConfigDataSource _upgradeSource;
    [SerializeField] private List<AbilityItemConfig> _abilityItems;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    [SerializeField]
    private UnityAdsTools _ads;

    private void Awake()
    {
        var model = new PlayerData(15f, _ads, _analyticsTools);
        _analyticsTools = new UnityAnalyticTools();
        model.CurrentState.Value = GameState.Start;
        _mainController = new MainController(_uiRoot, _playerProfile, _items, _upgradeSource.ItemConfigs.ToList(), _abilityItems.AsReadOnly());
    }


    private void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
