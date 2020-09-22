using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

[ApiController]
[Route("[controller]")]
public class ItemController : ControllerBase
{
    private readonly ILogger<ItemController> _logger;
    private readonly MongoDBRepository _mongoDBRepository;
    private readonly Random _random;
    public ItemController(ILogger<ItemController> logger, MongoDBRepository mongoDBRepository)
    {
        _logger = logger;
        _mongoDBRepository = mongoDBRepository;
        _random = new Random();
    }
    /*
        [HttpGet]
        public async Task<Player[]> Get()
        {
            var players = (await _fileRepository.GetAll()).ToList<Player>();
            Player player = new Player()
            {
                Id = Guid.NewGuid(),
                Name = "TestPlayer123",
                Score = _random.Next(0, 255),
                Level = _random.Next(0, 255),
                IsBanned = false
            };

            //await _fileRepository.Delete(players.Last().Id);
            Player p = await _fileRepository.Create(player);
            Console.WriteLine(p);
            return players.ToArray();
        }
    */
    [HttpGet("/players/{playerId}/items/{itemId}")]
    public Task<Item> GetItem(Guid playerId, Guid itemId)
    {
        return _mongoDBRepository.GetItem(playerId, itemId);
    }

    [HttpGet("/players/{playerId}/items")]
    public async Task<Item[]> GetAllItems(Guid playerId)
    {
        return await _mongoDBRepository.GetAllItems(playerId);
    }

    [HttpPost("/players/{playerId}/items/n")]
    public async Task<Item> CreateItem(Guid playerId, NewItem item)
    {
        Item i = new Item()
        {
            Id = Guid.NewGuid(),
            Name = item.Name,
            Value = _random.Next(0, 255),
            CreationDate = DateTime.Now
        };
        return await _mongoDBRepository.CreateItem(playerId, i);
    }

    [HttpPost("/players/{playerId}/items/{itemId}/m")]
    public async Task<Item> ModifyItem(Guid playerId, Guid itemId, ModifiedItem item)
    {
        return await _mongoDBRepository.UpdateItem(playerId, itemId, item);
    }

    [HttpPost("/players/{playerId}/items/{itemId}/d")]
    public async Task<Player> DeleteItem(Guid playerId, Guid itemId)
    {
        return await _mongoDBRepository.DeletePlayer(playerId);
    }
}