using Model.Analytic;
using Profile;
using Tools.Ads;
using UnityEngine;

public class MainController : BaseController
{
    private readonly PlayerData _model;
    private readonly Transform _uiRoot;
    private readonly IAnalyticTools _analyticsTools;
    private readonly IAdsShower _ads;

    private BaseController _currentController;
    private MainMenuController _mainMenuController;
    private GameController _gameController;

    public MainController(PlayerData model, Transform uiRoot,IAnalyticTools analyticTools, IAdsShower ads)
    {
        _model = model;
        _uiRoot = uiRoot;
        _analyticsTools = analyticTools;
        _ads = ads;

        OnGameModelChanged(_model.CurrentState.Value);
        _model.CurrentState.SubscribeOnChange(OnGameModelChanged);
    }

    private void OnGameModelChanged(GameState state)
    {
        switch (state)
        {
            case GameState.Start:
                _mainMenuController = new MainMenuController(_model, _uiRoot, _analyticsTools, _ads);
                _gameController?.Dispose();
                break;
            case GameState.Game:
                _gameController = new GameController(_model);
                _mainMenuController?.Dispose();
                break;
            default:
                _mainMenuController?.Dispose();
                _gameController?.Dispose();
                break;
        }
    }

    protected override void OnDispose()
    {
        _mainMenuController?.Dispose();
        _gameController?.Dispose();
        _model.CurrentState.UnsubscriptionOnChange(OnGameModelChanged);
        base.OnDispose();
    }
}
