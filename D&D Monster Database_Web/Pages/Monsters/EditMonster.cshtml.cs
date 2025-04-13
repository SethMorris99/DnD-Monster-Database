using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonsterDB_Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using static D_D_Monster_Database_Web.Pages.Monsters.AddMonsterModel;

namespace D_D_Monster_Database_Web.Pages.Monsters
{
    // This attribute is used to restrict access to the page to authenticated users
    [Authorize]

    // BindProperty attribute is used to bind the property to the form data
    [BindProperties]
    public class EditMonsterModel : PageModel
    {
        public Monster CurrentMonster { get; set; } 
        // List of source books for the dropdown
        public List<SelectListItem> SourceBook { get; set; } = new List<SelectListItem>();
        // List of treasure types for the dropdown
        //public List<SelectListItem> TreasureType { get; set; } = new List<SelectListItem>();
        // List of special abilities for the dropdown
        public List<GenreInfo> Genres { get; set; } = new List<GenreInfo>();
        // List of special abilities for the dropdown
        public List<int> SelectedGenreID { get; set; } = new List<int>();
        

        public void OnGet(int id)
        {
            PopulateMonsterDetails(id);
            // This method is called when the page is first loaded
            PopulateSourceBookList();
            // This method is commented out for now since we are considering adding a table for treasure types
            //PopulateTreasureTypeList();
            PopulateGenresList();
            // Populate the selected genres based on the monster ID
            SelectedGenreID = PopulateSelectedGenreIDS(id);
        }
        
        // This method is called when the form is submitted
        private static List<int> PopulateSelectedGenreIDS(int id)
        {
            List<int> selectedGenres = new List<int>();
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                // This is the SQL query to get the genre IDs for a specific monster
                string query = "SELECT GenreID FROM MonsterGenre WHERE MonsterID = @MonsterID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MonsterID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // Execute the command and read the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        selectedGenres.Add(reader.GetInt32(0));
                    }
                }
            }
            return selectedGenres;
        }
        // This method is called when the form is submitted
        private void PopulateMonsterDetails(int id)
        {
            // This method populates the monster details based on the monster ID
            using (SqlConnection connection = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                // This is the SQL query to get the monster details for a specific monster ID
                string query = "select MonsterID, SourceBookID, MonsterName, ArmorClass, HitDice, Attacks, Aligment, XP_Award, NumberAppearing, TreasureType, SpecialAbilites, Description, ImageURL, UserID From Monster WHERE MonsterID = @MonsterID";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MonsterID", id);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // Execute the command and read the results
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        // Populate the CurrentMonster object with the data from the database
                        CurrentMonster = new Monster
                        {
                            MonsterName = reader.GetString(0),
                            SourceBookID = reader.GetInt32(1),
                            ArmorClass = reader.GetInt32(2),
                            HitDice = reader.GetInt32(3),
                            Attacks = reader.GetString(4),
                            Alignment = reader.GetInt32(5),
                            XP_Award = reader.GetInt32(6),
                            NumberAppearing = reader.GetString(7),
                            TreasureType = reader.GetChar(8),
                            SpecialAbilities = reader.GetString(9),
                            Description = reader.GetString(10),
                            ImageURL = reader.GetString(11),
                            UserID = reader.GetInt32(12),

                        };
                    }
                }
            }
        }

        // This method is called when the form is submitted
        private void PopulateGenresList()
        {
            // This method populates the genres list with data from the database
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                // This is the SQL query to get the genre ID and name from the Genre table
                string query = "SELECT GenreID, GenreName FROM Genre";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                // Execute the command and read the results
                if (reader.HasRows)
                {
                    // The first while loop reads the genre data from the database
                    while (reader.Read())
                    {
                        var genre = new GenreInfo();
                        genre.GenreID = int.Parse(reader["GenreID"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();
                        // Set the IsSelected property to false by default
                        if (reader.HasRows)
                        {
                            // The second while loop reads the genre data from the database
                            while (reader.Read())
                            {
                                var monsterGenre = new GenreInfo();
                                monsterGenre.GenreID = int.Parse(reader["GenreID"].ToString());
                                monsterGenre.GenreName = reader["GenreName"].ToString();
                                Genres.Add(genre);
                                if (SelectedGenreID.Contains(monsterGenre.GenreID))
                                {
                                    monsterGenre.IsSelected = true;
                                }
                                else
                                {
                                    monsterGenre.IsSelected = false;
                                }
                            }
                        }

                    }
                }
            }
        }
        // This method is called when the form is submitted
        private void PopulateSourceBookList()
        {
            // This method populates the SourceBook list with data from the database
            // The connection string is retrieved from the AppHelper class
            // The query selects the SourceBookID and Title from the SourceBook table
            // The SqlCommand is used to execute the query
            // The SqlDataReader is used to read the data from the database
            // The data is added to the SourceBook list as SelectListItem objects
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string query = "SELECT SourceBookID, Title, Edition, YearPublished, PageNumber FROM SourceBook";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var sourcebook = new SelectListItem();
                        sourcebook.Value = reader["SourceBookID"].ToString();
                        sourcebook.Text = reader["Title"].ToString();
                        SourceBook.Add(sourcebook);
                    }
                    var defaultsourcebook = new SelectListItem();
                    defaultsourcebook.Value = "0";
                    defaultsourcebook.Text = "--Select Source Book--";
                    SourceBook.Insert(0, defaultsourcebook);
                }

            }
        }
    }
}
