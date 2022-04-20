using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryView : MonoBehaviour, IInventoryView
{
    public event EventHandler<IItem> Selected;
    public event EventHandler<IItem> Deselected;

    public void Display(IReadOnlyList<IItem> items)
    {
        foreach (var item in items)
            Debug.Log($"Id item: {item.Id}. Title item: {item.Info.Title}");
    }
}
