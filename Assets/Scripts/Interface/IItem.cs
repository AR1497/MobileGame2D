using System.Collections.Generic;

public interface IItem
{
    int Id { get; }
    ItemInfo Info { get; }
}
public struct ItemInfo
{
    public string Title { get; set; }
}
public interface IItemsRepository
{
    IReadOnlyDictionary<int, IItem> Items { get; }
}
