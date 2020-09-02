using System;
using System.Collections.Generic;
using System.Linq;

namespace teht2
{
    class Program
    {
        public delegate void PrintItemDel(Item item);
        public static void PrintItem(Item item)
        {
            Console.WriteLine("Id: " + item.Id + "\nLevel: " + item.Level);
        }
        static void Main(string[] args)
        {
            List<Player> players = Player.InstantiatePlayers();
            Boolean a = Player.DuplicatePlayerIDs(players);
            Console.WriteLine(a);

            Guid guid = Guid.NewGuid();

            List<Player> ps = new List<Player>
            {
                new Player { Id = guid },
                new Player { Id = Guid.NewGuid() },
                new Player { Id = guid }
            };

            Boolean b = Player.DuplicatePlayerIDs(ps);
            Console.WriteLine(b);

            Player player = new Player
            {
                Id = Guid.NewGuid(),
                Score = 0,
                Items = new List<Item> {
                    new Item { Name = "miakka", Id = Guid.NewGuid(), Level = 1, },
                    new Item { Name = "kirves", Id = Guid.NewGuid(), Level = 2, },
                    new Item { Name = "kala", Id = Guid.NewGuid(), Level = 30, },
                    new Item { Name = "kompassi", Id = Guid.NewGuid(), Level = 5, },
                }
            };

            Console.WriteLine(player.GetHighestLevelItem().Name);

            Console.WriteLine(string.Join(", ", player.GetItems().Select<Item, string>(i => i.Name)));
            Console.WriteLine(string.Join(", ", player.GetItemsWithLinq().Select<Item, string>(i => i.Name)));
            Console.WriteLine(player.FirstItem().Name);
            Console.WriteLine(player.FirstItemWithLinq().Name);

            Player.ProcessEachItem(player, PrintItem);

            Player.ProcessEachItem(player, (Item item) =>
            {
                Console.WriteLine("Id: " + item.Id + "\nLevel: " + item.Level);
            });



            List<PlayerFromAnotherGame> playersFromAnotherGame = PlayerFromAnotherGame.InstantiatePlayers();

            Game<Player> thisGame = new Game<Player>(players);
            Game<PlayerFromAnotherGame> thatGame = new Game<PlayerFromAnotherGame>(playersFromAnotherGame);

            Console.WriteLine("Top scores from this game: ");
            Console.WriteLine(string.Join(", ", thisGame.GetTop10Players().Select<Player, int>(p => p.Score)));
            Console.WriteLine("Top scores from that game: ");
            Console.WriteLine(string.Join(", ", thatGame.GetTop10Players().Select<PlayerFromAnotherGame, int>(p => p.Score)));
        }
    }
}


