using System;
using System.Threading.Tasks;

public interface IRepository
{
    Task<Player> CreatePlayer(Player player);
    Task<Player> GetPlayer(Guid playerId);
    Task<Player[]> GetAllPlayers();
    Task<Player> UpdatePlayer(Guid playerID, Player player);
    Task<Player> DeletePlayer(Guid playerId);

    Task<Item> CreateItem(Guid playerId, Item item);
    Task<Item> GetItem(Guid playerId, Guid itemId);
    Task<Item[]> GetAllItems(Guid playerId);
    Task<Item> UpdateItem(Guid playerId, Guid itemId, ModifiedItem item);
    Task<Item> DeleteItem(Guid playerId, Item item);
}