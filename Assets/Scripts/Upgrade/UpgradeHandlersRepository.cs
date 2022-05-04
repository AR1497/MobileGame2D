using System;
using System.Collections.Generic;

public class UpgradeHandlersRepository : BaseController
{
    public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItems;

    private Dictionary<int, IUpgradeCarHandler> _upgradeItems = new Dictionary<int, IUpgradeCarHandler>();

    public UpgradeHandlersRepository(IReadOnlyList<UpgradeItemConfig> configs)
    {
        PopulateItems(ref _upgradeItems, configs);
    }

    private void PopulateItems(ref Dictionary<int, IUpgradeCarHandler> upgradeItems, IReadOnlyList<UpgradeItemConfig> configs)
    {
        foreach (var config in configs)
        {
            upgradeItems[config.Id] = CreateHandler(config);
        }
    }

    private IUpgradeCarHandler CreateHandler(UpgradeItemConfig config)
    {
        switch (config.UpgradeType)
        {
            case UpgradeType.None:
                return UpgradeHandelrStub.Default;
                break;
            case UpgradeType.Speed:
                return new SpeedUpgradeCarHandler(config);
                break;
            case UpgradeType.Control:
                return UpgradeHandelrStub.Default;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

}
