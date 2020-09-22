using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;
using MongoDB.Bson;

public class MongoDBRepository : IRepository
{

    private readonly IMongoCollection<Player> _playerCollection;
    private readonly IMongoCollection<BsonDocument> _bsonDocumentCollection;

    public MongoDBRepository()
    {
        var mongoClient = new MongoClient("mongodb://localhost:27017");
        var database = mongoClient.GetDatabase("game");
        _playerCollection = database.GetCollection<Player>("players");
        _bsonDocumentCollection = database.GetCollection<BsonDocument>("players");
    }

    public async Task<Item> CreateItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player p = await _playerCollection.Find(filter).FirstAsync();

        p.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, p);
        return item;
    }

    public async Task<Player> CreatePlayer(Player player)
    {
        await _playerCollection.InsertOneAsync(player);
        return player;
    }

    public async Task<Item> DeleteItem(Guid playerId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player p = await _playerCollection.Find(filter).FirstAsync();
        Item ret = p.Inventory.Find(i => i.Id == item.Id);
        p.Inventory.Remove(ret);
        return ret;
    }

    public async Task<Player> DeletePlayer(Guid playerId)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerId);
        return await _playerCollection.FindOneAndDeleteAsync(filter);
    }

    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player p = await _playerCollection.Find(filter).FirstAsync();
        List<Item> inv = p.Inventory;
        return inv.ToArray<Item>();
    }

    public async Task<Player[]> GetAllPlayers()
    {
        var players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
        return players.ToArray();
    }

    public async Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player p = await _playerCollection.Find(filter).FirstAsync();
        Item item = p.Inventory.Find(item => item.Id == itemId);
        return item;
    }

    public Task<Player> GetPlayer(Guid playerId)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        return _playerCollection.Find(filter).FirstAsync();
    }

    public async Task<Item> UpdateItem(Guid playerId, Guid itemId, Item item)
    {
        var filter = Builders<Player>.Filter.Eq(player => player.Id, playerId);
        Player p = await _playerCollection.Find(filter).FirstAsync();

        p.Inventory.Add(item);
        await _playerCollection.ReplaceOneAsync(filter, p);
        return item;
    }

    public async Task<Player> UpdatePlayer(Guid playerID, Player player)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, player.Id);
        ReplaceOneResult res = await _playerCollection.ReplaceOneAsync(filter, player);
        if (res.MatchedCount == 0)
        {
            throw new NotFoundException();
        }
        return player;
    }
}