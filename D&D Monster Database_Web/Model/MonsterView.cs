using D_D_Monster_Database_Web.Model;
using D_D_Monster_Database_Web.Pages.Monsters;
using System;
using System.ComponentModel.DataAnnotations;

public class MonsterView
{

    public int MonsterID { get; set; }

    [Display(Name = "Monster Name")]

    public string MonsterName { get; set; }

    [Display(Name = "Source Book Name")]

    public int SourceBookID { get; set; }

    public string SourceBookTitle { get; set; }

    [Display(Name = "Armor Class")]

    public int ArmorClass { get; set; }

    [Display(Name = "Hit Dice")]

    public string HitDice { get; set; }

    public string Attacks { get; set; }

    public string Alignment { get; set; }

    [Display(Name = "XP Awards")]

    public int XP_Award { get; set; }

    [Display(Name = "Number Appearing")]

    public string NumberAppearing { get; set; }

    [Display(Name = "Treasure Type")]

    public char TreasureType { get; set; }

    [Display(Name = "Special Abilities")]

    public string SpecialAbilities { get; set; }

    public string Description { get; set; }

    [Display(Name = "Image URL")]
     public string ImageURL { get; set; }

    [Display(Name = "User ID")]
    public int UserID { get; set; }

    public List<string> GenreNames { get; set; } = new List<string>();


    public DateTime DateAdded { get; set; }
}
