using Profile;
using UnityEngine;

public class MainController : BaseController
{
    private readonly PlayerData _model;
    private readonly Transform _uiRoot;

    private BaseController _currentController;

    public MainController(PlayerData model, Transform uiRoot)
    {
        _model = model;
        _uiRoot = uiRoot;

        _model.GameState.SubscribeOnChange(OnGameModelChanged);
        _model.GameState.Value = GameState.Menu;
    }

    private void OnGameModelChanged(GameState state)
    {
        switch (state)
        {
            case GameState.None:
                break;
            case GameState.Menu:
                ReplaceController(ref _currentController, CreateMenuController());
                break;
            case GameState.Game:
                ReplaceController(ref _currentController, CreateGameController());
                break;
        }
    }

    private void ReplaceController(ref BaseController controller, BaseController newController)
    {
        controller?.Dispose();
        controller = newController;
        AddController(controller);
    }

    private MainMenuController CreateMenuController()
    {
        return new MainMenuController(_model, _uiRoot);
    }

    private GameController CreateGameController()
    {
        return new GameController(_model);
    }

    protected override void OnDispose()
    {
        _model.GameState.UnsubscriptionOnChange(OnGameModelChanged);
        base.OnDispose();
    }
}
