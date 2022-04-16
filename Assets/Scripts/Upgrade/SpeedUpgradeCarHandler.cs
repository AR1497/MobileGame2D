public class SpeedUpgradeCarHandler : IUpgradeCarHandler
{
    #region Fields
    private readonly float _speed;
    #endregion
    #region Life cycle
    public SpeedUpgradeCarHandler(float speed)
    {
        _speed = speed;
    }
    #endregion
    #region IUpgradeHandler
    public IUpgradableCar Upgrade(IUpgradableCar upgradableCar)
    {
        upgradableCar.Speed = _speed;
        return upgradableCar;
    }
    #endregion
}
