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
    [HttpGet("/players/{playerId}")]
    public Task<Player> Get(Guid playerId)
    {
        return _mongoDBRepository.GetPlayer(playerId);
    }

    [HttpGet("/players")]
    public async Task<Player[]> GetAll()
    {
        return await _mongoDBRepository.GetAllPlayers();
    }

    [HttpPost("/players/n")]
    public async Task<Player> Create(NewPlayer player)
    {

        Player p = new Player()
        {
            Id = Guid.NewGuid(),
            Name = player.Name,
            Score = _random.Next(0, 255),
            Level = _random.Next(0, 255),
            IsBanned = false,
            Inventory = new List<Item>()
        };
        return await _mongoDBRepository.CreatePlayer(p);
    }

    [HttpPost("/players/{playerId}/m")]
    public async Task<Player> Modify(Guid playerId, Player player)
    {
        return await _mongoDBRepository.UpdatePlayer(playerId, player);
    }

    [HttpPost("/players/{playerId}/d")]
    public async Task<Player> Delete(Guid playerId)
    {
        return await _mongoDBRepository.DeletePlayer(playerId);
    }
}