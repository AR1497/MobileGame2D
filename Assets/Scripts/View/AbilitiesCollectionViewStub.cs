using System;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesCollectionViewStub : MonoBehaviour, IAbilityCollectionView
{
    [SerializeField] private AbilityItemView _itemView;
    private CanvasGroup _canvasGroup;
    private Transform _layout;
    public List<AbilityItemView> _abilityItemViews = new List<AbilityItemView>();
    public event EventHandler<IItem> UseRequested;

    private void OnEnable()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _layout = gameObject.transform;
    }

    public void Display(IReadOnlyList<IItem> abilityItems)
    {
        foreach (var item in abilityItems)
        {
            var abilityItem = Instantiate<AbilityItemView>(_itemView, _layout);
            abilityItem.Init(item);
            _abilityItemViews.Add(abilityItem);
            abilityItem.OnClick += OnReQuested;
        }
    }

    private void OnReQuested(IItem item)
    {
        UseRequested?.Invoke(this, item);
    }

    public void Show()
    {
        _canvasGroup.alpha = 1;
    }
    public void Hide()
    {
        _canvasGroup.alpha = 0;
    }
}