using System;
using System.Collections.Generic;
using System.Linq;

public class Game<T> where T : IPlayer
{
    private List<T> _players;

    public Game(List<T> players)
    {
        _players = players;
    }

    public T[] GetTop10Players()
    {
        List<T> orderedPlayers = _players.OrderBy(p => p.Score).ToList();
        T[] top10 = new T[10];
        for (int i = 0; i < 10; i++)
        {
            top10[i] = orderedPlayers[orderedPlayers.Count - 1 - i];
        }

        return top10;
    }
}