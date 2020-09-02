using System;

public class Item
{
    public String Name { get; set; }
    public Guid Id { get; set; }
    public int Level { get; set; }

    public Delegate LevelUp()
    {
        Level++;
        return null;
    }
}