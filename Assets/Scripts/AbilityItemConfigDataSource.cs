using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeItemConfigDataSource", menuName = "UpgradeItemConfigDataSource", order = 0)]
internal class AbilityItemConfigDataSource : ScriptableObject
{
    [SerializeField]
    public AbilityItemConfig[] _itemConfigs;

    public AbilityItemConfig[] ItemConfigs => _itemConfigs;
}