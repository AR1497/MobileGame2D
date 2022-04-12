using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;

public class Root : MonoBehaviour
{
    [SerializeField]
    private Transform _uiRoot;

    private MainController _mainController;
    private IAnalyticTools _analyticsTools;

    [SerializeField]
    private UnityAdsTools _ads;

    private void Awake()
    {
        var model = new PlayerData(15f);
        _analyticsTools = new UnityAnalyticTools();
        model.CurrentState.Value = GameState.Start;
        _mainController = new MainController(model, _uiRoot, _analyticsTools, _ads);
    }


    private void OnDestroy()
    {
        _mainController?.Dispose();
    }
}
