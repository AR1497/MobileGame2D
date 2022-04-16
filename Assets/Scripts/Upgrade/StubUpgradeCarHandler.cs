public class StubUpgradeCarHandler : IUpgradeCarHandler
{
    public static readonly IUpgradeCarHandler Default = new StubUpgradeCarHandler();
    public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
    {
        return upgradableCar;
    }
}