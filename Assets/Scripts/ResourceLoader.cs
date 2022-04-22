using System;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ResourceLoader
{
    public static GameObject LoadPrefab(ResourcePath path)
    {
        return Resources.Load<GameObject>(path.PathResource);
    }

    internal static T LoadObject<T>(ResourcePath resourcePath)
    {
        throw new NotImplementedException();
    }

    public static T InstantiateObject<T>(T prefab, Transform parent, bool worldPositionStays) where
    T : Object
    {
        return Object.Instantiate(prefab, parent, worldPositionStays);
    }

    public static T LoadAndInstantiateObject<T>(ResourcePath path, Transform parent, bool
    worldPositionStays) where T : Object
    {
        var prefab = LoadObject<T>(path);
        return InstantiateObject(prefab, parent, worldPositionStays);
    }

}
