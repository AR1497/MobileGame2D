using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameController : BaseController
{
    public GameController(PlayerData profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryModel inventoryModel, Transform root)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);

        //var carController = new CarController();
        //AddController(carController);

        //var abilityRepository = new AbilityRepository(configs);
        //var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
        //    new AbilitiesCollectionViewStub());
        //AddController(abilitiesController);
    }
}
