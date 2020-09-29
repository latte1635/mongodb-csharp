using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly MongoDBRepository _mongoDBRepository;
    private readonly Random _random;
    public PlayerController(ILogger<PlayerController> logger, MongoDBRepository mongoDBRepository)
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
    [HttpGet("/players/{id:int}")]
    public Task<Player> GetPlayerById(Guid id)
    {
        return _mongoDBRepository.GetPlayer(id);
    }

    [HttpGet("/players/{playerName}")]
    public Task<Player> GetPlayerByName(string playerName)
    {
        Console.WriteLine(playerName);
        return _mongoDBRepository.GetPlayerByName(playerName);
    }

    [HttpGet("/playersbytag/{tag}")]
    public Task<Player[]> GetPlayersByTag(string tag)
    {
        Console.WriteLine(tag);
        return _mongoDBRepository.GetPlayersByTag(tag);
    }

    [HttpGet("/playersByItemType/{itemType}")]
    public async Task<Player[]> GetAllPlayersWithItemType(string itemType)
    {
        return await _mongoDBRepository.GetPlayersWithItemType((ItemType)Enum.Parse(typeof(ItemType), itemType));
    }

    [HttpGet("/playersByInvSize/{size:int}")]
    public async Task<Player[]> GetAllPlayersWithItemType(int size)
    {
        return await _mongoDBRepository.GetPlayersWithInvSize(size);
    }

    [HttpGet("/players")]
    public async Task<Player[]> GetAll([FromQuery(Name = "minscore")] int minScore = int.MinValue)
    {
        return await _mongoDBRepository.GetAllPlayers(minScore);
    }

    [HttpPost("/players/n")]
    public async Task<Player> Create(NewPlayer player)
    {

        Player p = new Player()
        {
            Id = Guid.NewGuid(),
            Name = player.Name,
            Tags = new List<string>(),
            Score = _random.Next(0, 255),
            Level = _random.Next(0, 255),
            IsBanned = false,
            Inventory = new List<Item>(),
            CreationTime = DateTime.Now
        };
        return await _mongoDBRepository.CreatePlayer(p);
    }

    [HttpPost("/players/{playerId}/m")]
    public async Task<Player> Modify(Guid playerId, Player player)
    {
        return await _mongoDBRepository.UpdatePlayer(playerId, player);
    }

    [HttpGet("/players/{playerId}/updatename/{newname:alpha}")]
    public async Task<Player> UpdatePlayerName(Guid playerId, string newname)
    {
        return await _mongoDBRepository.UpdatePlayerName(playerId, newname);
    }

    [HttpGet("/players/{playerId}/incrementscore/{amount:int}")]
    public async Task<Player> UpdatePlayerName(Guid playerId, int amount)
    {
        return await _mongoDBRepository.IncrementPlayerScore(playerId, amount);
    }

    [HttpPost("/players/{playerId}/d")]
    public async Task<Player> Delete(Guid playerId)
    {
        return await _mongoDBRepository.DeletePlayer(playerId);
    }
}