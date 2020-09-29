using System;
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
        if (p == null)
        {
            throw new NotFoundException();
        }

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

    public async Task<Player[]> GetAllPlayers(int min = int.MinValue)
    {
        if (min == int.MinValue)
        {
            List<Player> players = await _playerCollection.Find(new BsonDocument()).ToListAsync();
            Console.WriteLine("playerCount=" + players.Count);
            return players.ToArray();
        }
        else
        {
            FilterDefinition<Player> filter = Builders<Player>.Filter.Gt("Score", min);
            List<Player> players = await _playerCollection.Find(filter).ToListAsync();
            Console.WriteLine("playerCount=" + players.Count);
            return players.ToArray();
        }
    }

    public async Task<Player[]> GetPlayersByTag(string tag)
    {
        List<Player> players = await _playerCollection.Find(player => player.Tags.Any(Tag => Tag == tag)).ToListAsync();
        return players.ToArray();
    }

    public async Task<Player[]> GetPlayersWithItemType(ItemType itemType)
    {
        // Builders<Player>.Filter.ElemMatch<Item>(p => p.Inventory, Builders<Item>.Filter.Eq(i => i.Type, itemType));
        List<Player> players = await _playerCollection.Find(player => player.Inventory.Any(Item => Item.Type == itemType)).ToListAsync();
        return players.ToArray();
    }

    public async Task<Player[]> GetPlayersWithInvSize(int size)
    {
        List<Player> players = await _playerCollection.Find(player => player.Inventory.Count == size).ToListAsync();
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

    public Task<Player> GetPlayerByName(string name)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq("Name", name);
        return _playerCollection.Find(filter).FirstOrDefaultAsync<Player>();
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

        return player;
    }

    public async Task<Player> UpdatePlayerName(Guid playerID, string newName)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerID);
        var update = Builders<Player>.Update.Set("Name", newName);
        _playerCollection.UpdateOne(filter, update);
        Player p = await _playerCollection.Find(filter).FirstAsync();

        return p;
    }

    public async Task<Player> IncrementPlayerScore(Guid playerID, int amount)
    {
        FilterDefinition<Player> filter = Builders<Player>.Filter.Eq(p => p.Id, playerID);
        var update = Builders<Player>.Update.Inc("Score", amount);
        _playerCollection.UpdateOne(filter, update);
        Player p = await _playerCollection.Find(filter).FirstAsync();

        return p;
    }
}