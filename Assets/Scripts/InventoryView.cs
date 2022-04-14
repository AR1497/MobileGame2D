using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour, IInventoryView
{
    public event EventHandler<IItem> Selected;
    public event EventHandler<IItem> Deselected;
    private List<IItem> _itemInfoCollection;
    public void Display(List<IItem> itemInfoCollection)
    {
        _itemInfoCollection = itemInfoCollection;
    }
    protected virtual void OnSelected(IItem e)
    {
        Selected?.Invoke(this, e);
    }
    protected virtual void OnDeselected(IItem e)
    {
        Deselected?.Invoke(this, e);
    }
}
