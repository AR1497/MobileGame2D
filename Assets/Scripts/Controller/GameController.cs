using Profile;
using System.Collections.Generic;
using Tools;
using UnityEngine;

public class GameController : BaseController
{
    public GameController(ProfilePlayer profilePlayer, IReadOnlyList<AbilityItemConfig> configs, InventoryModel inventoryModel, Transform root)
    {
        var leftMoveDiff = new SubscriptionProperty<float>();
        var rightMoveDiff = new SubscriptionProperty<float>();

        var tapeBackgroundController = new TapeBackgroundController(leftMoveDiff, rightMoveDiff);
        AddController(tapeBackgroundController);

        var inputGameController = new InputGameController(leftMoveDiff, rightMoveDiff, profilePlayer.CurrentCar);
        AddController(inputGameController);

        var carController = new CarController();
        AddController(carController);

        var abilityRepository = new AbilityRepository((List<AbilityItemConfig>)configs);
        var abilitiesController = new AbilitiesController(carController, inventoryModel, abilityRepository,
            new AbilitiesCollectionView());
        AddController(abilitiesController);

    }

    private IAbilitiesController ConfigureAbilityController(
    Transform placeForUi,
    IAbilityActivator abilityActivator)
    {
        var abilityItemsConfigCollection
        = ContentDataSourceLoader.LoadAbilityItemConfigs(new ResourcePath
        {
            PathResource = "DataSource/Ability/AbilityItemConfigDataSource"
        });
        var abilityRepository = new AbilityRepository(abilityItemsConfigCollection);
        var abilityCollectionViewPath = new ResourcePath { PathResource = $"Prefabs/{nameof(AbilityCollectionView)}" };
        var abilityCollectionView = ResourceLoader.LoadAndInstantiateObject<AbilityCollectionView>(abilityCollectionViewPath,
        placeForUi, false);
        AddGameObject(abilityCollectionView.gameObject);

        var inventoryModel = new InventoryModel();
        var abilitiesController = new AbilitiesController(abilityRepository, inventoryModel,
        abilityCollectionView, abilityActivator);
        AddController(abilitiesController);
        return abilitiesController;
    }
}
