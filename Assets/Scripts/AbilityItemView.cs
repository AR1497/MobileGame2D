using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityItemView : MonoBehaviour
{
    [SerializeField] private Button _button;

    public event Action<IItem> OnClick;

    private IItem _item;

    public void Init(IItem item)
    {
        _item = item;
    }

    public void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        _button.onClick.AddListener(Click);
    }

    private void Click()
    {
        OnClick?.Invoke(_item);
    }
}
