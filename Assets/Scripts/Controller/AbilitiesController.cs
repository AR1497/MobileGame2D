using Company.Project.Features;
using JetBrains.Annotations;
using System;

public class AbilitiesController : BaseController, IAbilitiesController
{
    private readonly IInventoryModel _inventoryModel;
    private readonly IRepository<int, IAbility> _abilityRepository;
    private readonly IAbilityCollectionView _abilityCollectionView;
    private readonly IAbilityActivator _abilityActivator;
    public AbilitiesController(
    [NotNull] IAbilityActivator abilityActivator,
    [NotNull] IInventoryModel inventoryModel,
    [NotNull] IRepository<int, IAbility> abilityRepository,
    [NotNull] IAbilityCollectionView abilityCollectionView)
    {
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

    protected override void OnDispose()
    {
        CleanupView(_abilityCollectionView);
        base.OnDispose();
    }

    private void SetupView(IAbilityCollectionView view)
    {
        view.UseRequested += OnAbilityUseRequested;
    }
    private void CleanupView(IAbilityCollectionView view)
    {
        view.UseRequested -= OnAbilityUseRequested;
    }

    private void OnAbilityUseRequested(object sender, IItem e)
    {
        if (_abilityRepository.Collection.TryGetValue(e.Id, out var ability))
        {
            ability.Apply(_abilityActivator);
        }
    }

    public void ShowAbilities()
    {
        _abilityCollectionView.Show();
        _abilityCollectionView.Display(_inventoryModel.GetEquippedItems());
    }
}