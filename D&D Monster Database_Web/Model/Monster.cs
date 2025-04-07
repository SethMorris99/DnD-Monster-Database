using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace D_D_Monster_Database_Web.Model
{
    public class Monster
    {
        [Display(Name = "Monster Name")]

        public string MonsterName { get; set; }

        
        [Display(Name = "Source Book Name")]

        public int SourceBookID { get; set; }

        [Display(Name = "Armor Class")]

        public int ArmorClass { get; set; }
        
        [Display(Name = "Hit Points")]

        public int HitDice { get; set; }


        public int Attacks { get; set; }

        [Display(Name = "XP Awards")]

        public int XP_Award { get; set; }

        [Display(Name = "Number Appering")]

        public int NumberAppearing { get; set; }

        [Display(Name = "Treasure Type")]

        public int TreasureType { get; set; }

        [Display(Name = "Special Abilities")]

        public int SpecialAbilities { get; set; }


        public string Description { get; set; }

        [Display(Name = "Image URL")]
        public int ImageURL { get; set; }

        public int UserID { get; set; }

        public DateTime DateAdded { get; set; }


    }
}
