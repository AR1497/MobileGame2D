using UnityEngine;

public class CarController : BaseController
{
    private readonly CarView _carView;

    public CarController()
    {
        var prefab = ResourceLoader.LoadPrefab(new ResourcePath() { PathResource = "Prefabs/Car" });
        var car = GameObject.Instantiate(prefab);
        AddGameObject(car);
        _carView = car.GetComponent<CarView>();
    }
}
