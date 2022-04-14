using UnityEngine;
using System;

public class CarController : BaseController, IAbilityActivator
{   
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/Car" };
    private readonly CarView _carView;

    public CarController()
    {
        _carView = LoadView();
    }

    private CarView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObject(objView);

        return objView.GetComponent<CarView>();
    }

    public GameObject GetViewObject()
    {
        return _carView.gameObject;
    }

    GameObject IAbilityActivator.GetViewObject()
    {
        throw new System.NotImplementedException();
    }
}
