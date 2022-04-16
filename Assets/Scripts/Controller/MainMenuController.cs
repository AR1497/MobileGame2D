using Model.Analytic;
using UnityEngine;
using System.Collections.Generic;
using Tools.Ads;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly PlayerData _model;
    private readonly IAnalyticTools _analytics;
    private readonly IAdsShower _ads;
    private readonly MainMenuView _view;

    public MainMenuController(PlayerData model, Transform uiRoot, IAnalyticTools analytics, IAdsShower ads)
    {
        _model = model;
        _analytics = analytics;
        _ads = ads;
        _view = LoadView(uiRoot);
        _view.Init(StartGame);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<MainMenuView>();
    }

    private void StartGame()
    {
        _analytics.SendMessage("Start", new Dictionary<string, object>());
        _ads.ShowInterstitial();
        _model.CurrentState.Value = GameState.Game;
    }
}
