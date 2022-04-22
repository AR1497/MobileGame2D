using Company.Project.Features;
using System;
using System.Collections.Generic;

public class UpgradeHandlersRepository : IRepository<int, IUpgradeCarHandler>
{
    public IReadOnlyDictionary<int, IUpgradeCarHandler> UpgradeItems => _upgradeItems;

    private Dictionary<int, IUpgradeCarHandler> _upgradeItemsMapById = new Dictionary<int, IUpgradeCarHandler>();

    public UpgradeHandlersRepository(
        IReadOnlyList<UpgradeItemConfig> upgradeItemConfigs)
    {
        PopulateItems(ref _upgradeItemsMapById, upgradeItemConfigs);
    }

    private void PopulateItems(
        ref Dictionary<int, IUpgradeCarHandler> upgradeHandlersMapByType, 
        IReadOnlyList<UpgradeItemConfig> configs)
    {
        foreach (var config in configs)
        {
            if (upgradeHandlersMapByType.ContainsKey(config.Id)) continue;
            upgradeHandlersMapByType.Add(config.Id, CreateHandlerByType(config));
        }
    }

    private IUpgradeCarHandler CreateHandlerByType(UpgradeItemConfig config)
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

    public IReadOnlyDictionary<int, IUpgradeCarHandler> Collection => _upgradeItemsMapById;
}
