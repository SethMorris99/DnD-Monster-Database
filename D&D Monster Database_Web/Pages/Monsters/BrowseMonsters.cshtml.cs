using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.Monsters
{
    [Authorize]
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
                string cmdText = "SELECT m.MonsterID, m.SourceBookID, m.MonsterName, m.ArmorClass, m.HitDice, m.Attacks, m.Alignment, m.Xp_Award, m.NumberAppearing, m.TreasureType, m.SpecialAbilities, m.Description, m.ImageURL " +
                                 "FROM Monster m JOIN MonsterGenre g ON m.MonsterID = g.MonsterID";
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        MonsterView monster = new MonsterView
                        {
                            MonsterID = Convert.ToInt32(reader["MonsterID"]),
                            MonsterName = reader["MonsterName"].ToString(),
                            SourceBookID = Convert.ToInt32(reader["SourceBookID"]),
                            ArmorClass = Convert.ToInt32(reader["ArmorClass"]),
                            HitDice = reader["HitDice"].ToString(),
                            Attacks = reader["Attacks"].ToString(),
                            Alignment = reader["Alignment"].ToString(),
                            XP_Award = Convert.ToInt32(reader["XP_Award"]),
                            NumberAppearing = reader["NumberAppearing"].ToString(),
                            TreasureType = Convert.ToChar(reader["TreasureType"]),
                            SpecialAbilities = reader["SpecialAbilities"].ToString(),
                            Description = reader["Description"].ToString(),
                            ImageURL = reader["ImageURL"].ToString(),
                            GenreNames = PopulateMonsterGenres(reader.GetInt32(0)),
                        };
                        Monsters.Add(monster);
                    }
                }
            }
        }


        private List<string> PopulateMonsterGenres(int MonsterID)
        {
            // This method populates the genres for a specific monster
            List<string> geners = new List<string>();
            
            // Open a connection to the database using the connection string from AppHelper
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                // This is the SQL query to get the genre names for a specific monster
                string cmdText = "SELECT g.GenreName FROM Genre g " + 
                    "JOIN MonsterGenre mg on g.GenreID = mg.GenreID " + 
                    "WHERE mg.MonsterID = @MonsterID";
                // Create a SqlCommand object with the query and the connection
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@MonsterID", MonsterID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                
                // Execute the command and read the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        geners.Add(reader.GetString(0));
                    }
                   
                }
            }
            return geners;
        }

        public IActionResult OnPostDelete(int id)
        {
            //Delete the monster form the database 
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = "Delete From MonsterGenre where MonsterID = @MonsterID; Delete from Monster where MonsterID = @MonsterID" ;
                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@MonsterID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            PopulateMonsterList();
            return Page();
        }
    }
}
