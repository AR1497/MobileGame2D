using System.Collections.Generic;
using Profile;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly PlayerData _profilePlayer;
    private readonly MainMenuView _view;

    public MainMenuController(Transform placeForUi, PlayerData profilePlayer)
    {
        _profilePlayer = profilePlayer;
        _view = LoadView(placeForUi);
        _view.Init(StartGame, GoToTheShed, DailyRewardGame);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _profilePlayer.CurrentState.Value = GameState.Game;

        _profilePlayer.AnalyticTools.SendMessage("start_game");
    }

    private void GoToTheShed()
    {
        _profilePlayer.CurrentState.Value = GameState.Shed;
    }
    private void DailyRewardGame()
    {
        _profilePlayer.CurrentState.Value = GameState.DailyReward;
    }
}
