using System;
using System.Linq;

public static class PlayerExtensions
{
    public static Item GetHighestLevelItem(this Player player)
    {
        Guid dummyGuid = Guid.NewGuid();
        Item retval = new Item { Id = dummyGuid, Level = int.MinValue };
        foreach (Item item in player.Items)
        {
            if (item.Level >= retval.Level)
            {
                retval = item;
            }
        }

        if (retval.Id == dummyGuid)
            return null;
        return retval;
    }

    public static Item[] GetItems(this Player player)
    {
        Item[] items = new Item[player.Items.Count];

        for (int i = 0; i < player.Items.Count; i++)
        {
            items[i] = player.Items[i];
        }

        return items;
    }

    public static Item[] GetItemsWithLinq(this Player player)
    {
        return player.Items.ToArray<Item>();
    }

    public static Item FirstItem(this Player player)
    {
        if (player.Items.Count != 0)
            return player.Items[0];
        return null;
    }

    public static Item FirstItemWithLinq(this Player player)
    {
        return player.Items.First();
    }
}