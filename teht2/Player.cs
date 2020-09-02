using System;
using System.Collections.Generic;
using System.Linq;

public class Player : IPlayer
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public List<Item> Items { get; set; }



    public static List<Player> InstantiatePlayers()
    {
        List<Player> result = new List<Player>();
        for (int i = 0; i < 1000; i++)
            result.Add(new Player { Id = Guid.NewGuid(), Score = new Random().Next(0, 1000000), Items = new List<Item>() });

        return result;
    }

    public static Boolean DuplicatePlayerIDs(List<Player> players)
    {
        return players.GroupBy(player => player.Id).Where(p => p.Count() > 1).Select(y => y).ToList().Count() == 1;
    }

    public static void ProcessEachItem(Player player, Action<Item> process)
    {
        foreach (Item item in player.Items)
        {
            process(item);
        }
    }
}
