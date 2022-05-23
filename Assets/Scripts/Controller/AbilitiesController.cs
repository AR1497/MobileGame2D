using JetBrains.Annotations;
using System;

public class AbilitiesController : BaseController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IAbilityRepository _abilityRepository;
    private readonly IAbilityCollectionView _abilityCollectionView;
    private readonly IAbilityActivator _abilityActivator;
    public ItemsRepository _itemsRepository;

    public AbilitiesController(
    [NotNull] IAbilityActivator abilityActivator,
    [NotNull] IInventoryModel inventoryModel,
    [NotNull] IAbilityRepository abilityRepository,
    [NotNull] IAbilityCollectionView abilityCollectionView,
    ItemsRepository itemsRepository)
    {
        _itemsRepository = itemsRepository;
        _abilityActivator = abilityActivator ?? throw new
        ArgumentNullException(nameof(abilityActivator));
        _inventoryModel = inventoryModel ?? throw new
        ArgumentNullException(nameof(inventoryModel));
        _abilityRepository = abilityRepository ?? throw new
        ArgumentNullException(nameof(abilityRepository));
        _abilityCollectionView = abilityCollectionView ?? throw new
        ArgumentNullException(nameof(abilityCollectionView));
        _abilityCollectionView.UseRequested += OnAbilityUseRequested;
        _abilityCollectionView.Display(_inventoryModel.GetEquippedItems());
        SetupView(_abilityCollectionView);
    }

    private void SetupView(IAbilityCollectionView abilityCollectionView)
    {
        abilityCollectionView.UseRequested += OnAbilityUseRequested;
    }

    private void CleanUp(IAbilityCollectionView abilityCollectionView)
    {
        abilityCollectionView.UseRequested -= OnAbilityUseRequested;
    }

    private void OnAbilityUseRequested(object sender, IItem e)
    {
        if (_abilityRepository.AbilityMapByItemId.TryGetValue(e.Id, out var ability))
            ability.Apply(_abilityActivator);
    }
    public void ShowAbilities()
    {
        foreach (var item in _itemsRepository.Items.Values)
        {
            _inventoryModel.EquipItem(item);
        }
        _abilityCollectionView.Display(_inventoryModel.GetEquippedItems());
        _abilityCollectionView.Show();
    }
}