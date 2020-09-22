using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class MongoDBRepository : IRepository
{
    public Task<Item> CreateItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> CreatePlayer(Player player)
    {
        throw new NotImplementedException();
    }

    public Task<Item> DeleteItem(Guid playerId, Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> DeletePlayer(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Item[]> GetAllItems(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Player[]> GetAllPlayers()
    {
        throw new NotImplementedException();
    }

    public Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Task<Player> GetPlayer(Guid playerId)
    {
        throw new NotImplementedException();
    }

    public Task<Item> UpdateItem(Guid playerId, Guid itemId, ModifiedItem item)
    {
        throw new NotImplementedException();
    }

    public Task<Player> UpdatePlayer(Guid playerID, ModifiedPlayer player)
    {
        throw new NotImplementedException();
    }
}