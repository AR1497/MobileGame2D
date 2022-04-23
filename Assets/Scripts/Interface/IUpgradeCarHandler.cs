public interface IUpgradeCarHandler
{
    IUpgradableCar Upgrade(IUpgradableCar upgradableCar);
}
public interface IUpgradableCar
{
    float Speed { get; set; }
    void Restore();
}