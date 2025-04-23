using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using MonsterDB_Business;

namespace D_D_Monster_Database_Web.Pages
{
    public class IndexModel : PageModel
    {
        public List<MonsterPoster> MonsterPosters { get; set; } = new List<MonsterPoster>();

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        

        public string Layout { get; private set; }

        

        public void OnGet()
        {
            PopulateMonsterPosters();
        }

        // Populate the MonsterPosters list with data from the database
        private void PopulateMonsterPosters()
        {

            // Example: Retrieve data from the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string sql = "SELECT MonsterName, ImageURL FROM Monster";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    MonsterPoster monsterPoster = new MonsterPoster
                    {
                        MonsterName = reader.GetString(0),
                        ImageURL = reader.GetString(1),

                    };
                    MonsterPosters.Add(monsterPoster);
                    /*MonsterPosters.Add(new MonsterPoster
                    {
                        MonsterId = Convert.ToInt32(reader["MonsterId"]),
                        MonsterName = reader["MonsterName"].ToString(),
                        ImageURL = reader["ImageURL"].ToString(),
                        HitDice = Convert.ToInt32(reader["HitDice"])
                    }); */
                }
            }

        }
    }
}
