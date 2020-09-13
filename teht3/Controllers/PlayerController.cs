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
    private readonly FileRepository _fileRepository;
    private readonly Random _random;
    public PlayerController(ILogger<PlayerController> logger, FileRepository fileRepository)
    {
        _logger = logger;
        _fileRepository = fileRepository;
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
    [HttpGet("/players/{id}")]
    public Task<Player> Get(Guid id)
    {
        return _fileRepository.Get(id); ;
    }

    [HttpGet("/players")]
    public async Task<Player[]> GetAll()
    {
        return await _fileRepository.GetAll();
    }

    [HttpPost("/players/new")]
    public async Task<Player> Create(NewPlayer player)
    {
        Player p = new Player()
        {
            Id = Guid.NewGuid(),
            Name = player.Name,
            Score = _random.Next(0, 255),
            Level = _random.Next(0, 255),
            IsBanned = false
        };
        return await _fileRepository.Create(p);
    }

    [HttpPost("/players/{id}/m")]
    public async Task<Player> Modify(Guid id, ModifiedPlayer player)
    {
        return await _fileRepository.Modify(id, player);
    }

    [HttpPost("/players/{id}/d")]
    public async Task<Player> Delete(Guid id)
    {
        return await _fileRepository.Delete(id);
    }
}