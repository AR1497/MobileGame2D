using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item", order = 0)]
public class ItemConfig : ScriptableObject
{
    [SerializeField]
    public int id;

    [SerializeField]
    public string _title;

    public int Id => id;

    public string Title => _title;

    public static implicit operator ItemConfig(List<ItemConfig> v)
    {
        throw new NotImplementedException();
    }
}
