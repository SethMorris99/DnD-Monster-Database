using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.Monsters
{
    public class BrowseMonstersModel : PageModel
    {

        public List<MonsterView> Monsters { get; set; } = new List<MonsterView>();
        public void OnGet()
        {
            PopulateMonsterList();
        }

        private void PopulateMonsterList()
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT * FROM Monster m JOIN MonsterGenre g on m.MonsterID = g.MonsterID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MonsterView monster = new MonsterView {
                            MonsterName = reader["MonsterName"].ToString(),
                            SourceBookID = Convert.ToInt32(reader["SourceBookID"]),
                            ArmorClass = Convert.ToInt32(reader["ArmorClass"]),
                            HitDice = Convert.ToInt32(reader["HitDice"]),
                            Attacks = reader["Attacks"].ToString(),
                            Alignment = Convert.ToInt32(reader["Alignment"]),
                            XP_Award = Convert.ToInt32(reader["XP_Award"]),
                            NumberAppearing = reader["NumberAppearing"].ToString(),
                            TreasureType = Convert.ToChar(reader["TreasureType"]),
                            SpecialAbilities = reader["SpecialAbilities"].ToString(),
                            Description = reader["Description"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                        };
                        Monsters.Add(monster);
                    }
                }
            }
        }
    }
}
