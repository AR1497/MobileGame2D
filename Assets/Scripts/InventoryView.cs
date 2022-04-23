using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class InventoryView : MonoBehaviour, IInventoryView
{
    public event Action<IItem> Selected;
    public event Action<IItem> Deselected;

    public void Show()
    {
    }
    public void Hide()
    {
    }

    private List<IItem> _itemInfoCollection;
    private List<GameObject> _itemsObjectsView = new List<GameObject>();
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Item" };
    private InventoryItemView _itemView;

    public void Init(List<IItem> items)
    {
        _itemInfoCollection = new List<IItem>(2);
        _itemInfoCollection = items;
        foreach (var item in _itemInfoCollection)
        {
            _itemsObjectsView.Add(GameObject.Instantiate(ResourceLoader.LoadPrefab(_viewPath), transform, false));
            _itemsObjectsView.LastOrDefault().transform.GetChild(0).GetComponent<Text>().text = item.Info.Title;
            _itemsObjectsView.LastOrDefault().transform.GetChild(1).GetComponent<Image>().sprite = item.Info.Sprite;
            _itemView = _itemsObjectsView.LastOrDefault().GetComponent<InventoryItemView>();
            _itemView.Init(item);
            _itemView.OnClick += OnSelected;

        }
        gameObject.SetActive(false);
    }

    public void Display()
    {
        if (gameObject.activeSelf) gameObject.SetActive(false);
        else gameObject.SetActive(true);
    }

    public void Display(List<IItem> itemInfoCollection)
    {
        _itemInfoCollection = itemInfoCollection;
    }

    protected virtual void OnSelected(IItem e)
    {
        Selected?.Invoke(e);
    }

    protected virtual void OnDeselected(IItem e)
    {
        Deselected?.Invoke(e);
    }
}
