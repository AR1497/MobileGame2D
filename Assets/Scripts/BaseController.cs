using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

public abstract class BaseController : IDisposible
{
    private List<GameObject> _gameObjects = new List<GameObject>();
    private List<BaseController> _controllers = new List<BaseController>();

    private bool _isDisposed = false;

    protected void AddGameObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }

    protected void AddController(BaseController controller)
    {
        _controllers.Add(controller);
    }

    protected virtual void OnDispose()
    {

    }

    public void Dispose()
    {
        if (_isDisposed)
            return;
        OnDispose();

        foreach (var go in _gameObjects)
        {
            if (go != null)
            {
                GameObject.Destroy(go);
            }
        }
        _gameObjects.Clear();

        foreach (var controller in _controllers)
        {
            controller?.Dispose();
        }
        _controllers.Clear();
    }
}