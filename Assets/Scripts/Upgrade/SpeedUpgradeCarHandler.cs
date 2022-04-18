public class SpeedUpgradeCarHandler : IUpgradeCarHandler
{
    #region Fields
    private readonly UpgradeItemConfig _config;
    #endregion
    #region Life cycle
    public SpeedUpgradeCarHandler(float config)
    {
        _config = config;
    }
    #endregion
    #region IUpgradeHandler
    public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
    {
        upgradableCar.Speed = _config.ValueUpgrade;
        return upgradableCar;
    }
    #endregion
}
