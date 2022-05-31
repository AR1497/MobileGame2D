using System;
using UnityEngine;

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
}
