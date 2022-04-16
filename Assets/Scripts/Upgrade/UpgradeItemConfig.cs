using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade item", menuName = "Upgrade item", order = 0)]
public class UpgradeItemConfig : ScriptableObject
{
    public ItemConfig itemConfig;
    public UpgradeType type;
    public float value;
    public int Id => itemConfig.id;
}
public enum UpgradeType
{
    None,
    Speed,
}
