using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemView : MonoBehaviour
{
    [SerializeField] private Button button;

    public event Action<IItem> OnClick;
    private IItem _item;

    public void Init(IItem item)
    {
        _item = item;
    }

    private void Awake()
    {
        button.onClick.AddListener(Click);
    }

    private void Click() => OnClick?.Invoke(_item);

    private void OnDestroy()
    {
        OnClick = null;
        button.onClick.RemoveAllListeners();
    }
}
