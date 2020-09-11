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

        await _fileRepository.Delete(players.Last().Id);

        return players.ToArray();
    }
}