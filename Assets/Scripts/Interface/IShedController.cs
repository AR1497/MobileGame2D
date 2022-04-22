public interface IShedController
{
    void Enter();
    void Exit();
}

public interface IUpgradeHandler
{
    IUpgradable Upgrade(IUpgradable upgradable);
}

public interface IUpgradable
{
    void Restore();
    float Speed { get; set; }
}
