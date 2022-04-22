public interface IUpgradeCarHandler
{
    IUpgradableCar Upgrade(IUpgradableCar upgradableCar);
    void Upgrade(IUpgradable upgradable);
}
public interface IUpgradableCar
{
    float Speed { get; set; }
    void Restore();
}