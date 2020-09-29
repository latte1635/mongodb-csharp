using System;
using System.Collections.Generic;

public class Player
{
    public List<Item> Inventory { get; set; }
    public List<string> Tags { get; set; }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public bool IsBanned { get; set; }
    public DateTime CreationTime { get; set; }
}