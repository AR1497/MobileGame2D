using System.Collections.Generic;
using System.Linq;
using Profile;
using UnityEngine;

public class MainMenuController : BaseController
{
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/mainMenu" };
    private readonly ProfilePlayer _profilePlayer;
    private readonly MainMenuView _view;

    public MainMenuController(
        Transform placeForUi, 
        ProfilePlayer profilePlayer)
    {
        _profilePlayer = profilePlayer; 
        _view = ResourceLoader.LoadAndInstantiateObject<MainMenuView>(new ResourcePath
        { PathResource = "Prefabs/mainMenu" }, placeForUi, false);
        //_view = LoadView(placeForUi);
        AddGameObject(_view.gameObject);
        _view.Init(StartGame, GoToTheShed);

        //var cursorTrailController = ConfigureCursorTrail();
        var shedController = ConfigureShedController(placeForUi, profilePlayer);
    }

    private MainMenuView LoadView(Transform placeForUi)
    {
        var objectView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath), placeForUi, false);
        AddGameObject(objectView);

        return objectView.GetComponent<MainMenuView>();
    }

    //private BaseController ConfigureCursorTrail()
    //{
    //    var cursorTrailController = new CursorTrailController();
    //    AddController(cursorTrailController);
    //    return cursorTrailController;
    //}

    private BaseController ConfigureShedController(
    Transform placeForUi,
    ProfilePlayer profilePlayer,
    IUpgradable upgradable,
    List<UpgradeItemConfig> upgradeItems)
    {
        var upgradeItemsConfigCollection
        = ContentDataSourceLoader.LoadUpgradeItemConfigs(new ResourcePath
        {
            PathResource = "DataSource/Upgrade/UpgradeItemConfigDataSource"
        });
        var upgradeItemsRepository = new UpgradeHandlersRepository(upgradeItemsConfigCollection);
        var itemsRepository = new ItemsRepository(upgradeItemsConfigCollection.Select(value => value._itemConfig).ToList());
        var itemsConfig = new ItemConfig();
        var inventoryModel = new InventoryModel();
        var inventoryViewPath = new ResourcePath { PathResource = $"Prefabs/{nameof(InventoryView)}" };
        var inventoryView = ResourceLoader.LoadAndInstantiateObject<InventoryView>(inventoryViewPath, placeForUi, false);
        AddGameObject(inventoryView.gameObject);
        var inventoryController = new InventoryController(itemsRepository, itemsConfig, inventoryModel, inventoryView);
        AddController(inventoryController);
        var shedController = new ShedController(upgradeItemsRepository, inventoryController, upgradable, upgradeItems,
        profilePlayer.CurrentCar, placeForUi, inventoryModel);
        AddController(shedController);
        return shedController;
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
}
