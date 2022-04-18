using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade item", menuName = "Upgrade item", order = 0)]
public class UpgradeItemConfig : ScriptableObject
{
    [SerializeField]
    public ItemConfig _itemConfig;

    [SerializeField]
    public UpgradeType _upgradeType;

    [SerializeField]
    public float _valueUpgrade;

    public int Id => _itemConfig.id;
    public UpgradeType UpgradeType => _upgradeType;

    public int ValueUpgrade => _valueUpgrade;
}
public enum UpgradeType
{
    None,
    Speed,
    Control,
}
