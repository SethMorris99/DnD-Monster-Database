using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages.Monsters
{
    public class ViewMonsterModel : PageModel
    {
        public MonsterView Monster { get; set; }

        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = @"SELECT m.MonsterID, m.MonsterName, m.ArmorClass, m.HitDice, m.Attacks, m.Alignment, 
                                          m.Xp_Award, m.NumberAppearing, m.TreasureType, m.SpecialAbilities, 
                                          m.Description, m.ImageURL, sb.Title as Title
                                   FROM Monster m
                                   JOIN SourceBook sb ON m.SourceBookID = sb.SourceBookID
                                   WHERE m.MonsterID = @MonsterID";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@MonsterID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    Monster = new MonsterView
                    {
                        MonsterID = Convert.ToInt32(reader["MonsterID"]),
                        MonsterName = reader["MonsterName"].ToString(),
                        SourceBookTitle = reader["Title"].ToString(),
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
                        GenreNames = new List<string>() // Populated next
                    };
                }
                else return NotFound();
            }

            Monster.GenreNames = PopulateMonsterGenres(Monster.MonsterID);
            return Page();
        }

        private List<string> PopulateMonsterGenres(int MonsterID)
        {
            var genres = new List<string>();
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string query = "SELECT GenreName FROM Genre g JOIN MonsterGenre mg ON g.GenreID = mg.GenreID WHERE mg.MonsterID = @MonsterID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MonsterID", MonsterID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    genres.Add(reader["GenreName"].ToString());
                }
            }
            return genres;
        }
        public IActionResult OnPostDelete(int id)
        {
            // delete monster from database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string cmdText = @"
            DELETE FROM MonsterGenre WHERE MonsterID = @MonsterID;
            DELETE FROM Monster WHERE MonsterID = @MonsterID";

                SqlCommand cmd = new SqlCommand(cmdText, conn);
                cmd.Parameters.AddWithValue("@MonsterID", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }

            //we want to redirect back to Browse after deleting
            return RedirectToPage("/Monsters/BrowseMonsters");
        }
    }
}
