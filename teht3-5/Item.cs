using System;
using System.ComponentModel.DataAnnotations;


public enum ItemType
{
    SWORD, POTION, SHIELD
}

public class Item
{
    public Guid Id { get; set; }

    [Range(1, 99)]
    public int Level { get; set; }

    [EnumDataType(typeof(ItemType))]
    public ItemType Type { get; set; }
    public string Name { get; set; }
    public int Value { get; set; }

    [DateValidator]
    public DateTime CreationDate { get; set; }
}