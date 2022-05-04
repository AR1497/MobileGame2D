using Tools;
using UnityEngine;

internal class InputController : BaseController
{
    public InputController(SubscriptionProperty<float> leftMove, SubscriptionProperty<float> rightMove, Car car)
    {
        _inputView = LoadView();
        _inputView.Init(leftMove, rightMove, car.Speed);
    }

    private BaseInputView _inputView;
    private readonly ResourcePath _viewPath = new ResourcePath { PathResource = "Prefabs/StickControl" };

    private BaseInputView LoadView()
    {
        var objView = Object.Instantiate(ResourceLoader.LoadPrefab(_viewPath));
        AddGameObject(objView);

        return objView.GetComponent<BaseInputView>();
    }
}