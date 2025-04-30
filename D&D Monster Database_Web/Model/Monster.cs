using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class Monster
    {
        public int MonsterID { get; set; }
        [Display(Name = "Monster Name")]

        public string MonsterName { get; set; }

        
        [Display(Name = "Source Book Name")]

        public int SourceBookID { get; set; }

        [Display(Name = "Armor Class")]

        [Range(0, int.MaxValue, ErrorMessage = "Must be a positive integer")]
        public int ArmorClass { get; set; }
        
        [Display(Name = "Hit Dice")]

        public string HitDice { get; set; }


        public string Attacks { get; set; }

        public string Alignment { get; set; }

        [Display(Name = "XP Awards")]
        [Range(0, int.MaxValue, ErrorMessage = "Must be a positive integer")]
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

        public int UserID { get; set; }

        public DateTime DateAdded { get; set; }
    }
}
