using System;
using UnityEngine;

public class BackgroundController : BaseController
{
    private readonly IReadOnlySubscriptionProperty<float> _movement;
    private TapeBackgroundView _background;

    public BackgroundController(IReadOnlySubscriptionProperty<float> movement)
    {
        var prefab = ResourceLoader.LoadPrefab(new ResourcePath() { PathResource = "Prefabs/background" });
        var go = GameObject.Instantiate(prefab);
        AddGameObject(go);
        _background = go.GetComponentInChildren<TapeBackgroundView>();
        _background.Init(movement);
    }
}
