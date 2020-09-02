using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerFromAnotherGame : IPlayer
{
    public Guid Id { get; set; }
    public int Score { get; set; }
    public List<Item> Items { get; set; }



    public static List<PlayerFromAnotherGame> InstantiatePlayers()
    {
        List<PlayerFromAnotherGame> result = new List<PlayerFromAnotherGame>();
        for (int i = 0; i < 1000; i++)
            result.Add(new PlayerFromAnotherGame { Id = Guid.NewGuid(), Score = new Random().Next(0, 1000000), Items = new List<Item>() });

        return result;
    }
}