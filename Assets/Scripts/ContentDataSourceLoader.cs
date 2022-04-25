using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ContentDataSourceLoader
{
    public static List<UpgradeItemConfig> LoadUpgradeItemConfigs(ResourcePath resourcePath)
    {
        var config = ResourceLoader.LoadObject<UpgradeItemConfigDataSource>(resourcePath);
        return config == null ? new List<UpgradeItemConfig>() : config._itemConfigs.ToList();
    }
    public static List<AbilityItemConfig> LoadAbilityItemConfigs(ResourcePath resourcePath)
    {
        var config = ResourceLoader.LoadObject<AbilityItemConfigDataSource>(resourcePath);
        return config == null ? new List<AbilityItemConfig>() : config._itemConfigs.ToList();
    }
}
