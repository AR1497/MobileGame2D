public class UpgradeHandelrStub : IUpgradeCarHandler
{
    public static IUpgradeCarHandler Default { get; } = new UpgradeHandelrStub();

    public IUpgradableCar Upgrade(IUpgradableCar car)
    {
        return car;
    }
}