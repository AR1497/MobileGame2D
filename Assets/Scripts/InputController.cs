using JoostenProductions;
using Tools;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

internal class InputController : BaseController
{
    private BaseInputView _inputView;
    private readonly SubscriptionProperty<float> _movement;

    public InputController(SubscriptionProperty<float> movement)
    {
        _movement = movement;
        var prefab = ResourceLoader.LoadPrefab(new ResourcePath() { PathResource = "Prefabs/StickControl" });
        var go = GameObject.Instantiate(prefab);
        AddGameObject(go);
        _inputView = go.GetComponent<BaseInputView>();

        UpdateManager.SubscribeToUpdate(OnUpdate);
    }

    private void OnUpdate()
    {
        _movement.Value = CrossPlatformInputManager.GetAxis("Horizontal");
    }

    protected override void OnDispose()
    {
        UpdateManager.UnsubscribeFromUpdate(OnUpdate);
        base.OnDispose();
    }
}