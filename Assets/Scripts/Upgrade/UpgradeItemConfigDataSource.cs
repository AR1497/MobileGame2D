using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName =
"UpgradeItemConfigDataSource", order = 0)]
public class UpgradeItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    public UpgradeItemConfig[] _itemConfigs;

    public UpgradeItemConfig[] ItemConfigs => _itemConfigs;
}
