using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonsterDB_Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Data.SqlClient;
using static D_D_Monster_Database_Web.Pages.Monsters.AddMonsterModel;
using System.Security.Claims;

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
            // Populate the selected genres based on the monster ID
            SelectedGenreID = PopulateSelectedGenreIDS(id);
            PopulateGenresList();
            
        }
        public IActionResult OnPost()
        {
            // force genres to be selected
            if (SelectedGenreID == null || !SelectedGenreID.Any())
            {
                ModelState.AddModelError("GenreError", "Please select at least one genre.");
                PopulateSourceBookList();
                PopulateGenresList();
                return Page();
            }
            if (ModelState.IsValid)
            {
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    conn.Open();

                    //Update Monster data
                    string cmdText = @"UPDATE Monster SET
                                 SourceBookID = @SourceBookID,
                                 MonsterName = @MonsterName,
                                 ArmorClass = @ArmorClass,
                                 HitDice = @HitDice,
                                 Attacks = @Attacks,
                                 Alignment = @Alignment,
                                 XP_Award = @XP_Award,
                                 NumberAppearing = @NumberAppearing,
                                 TreasureType = @TreasureType,
                                 SpecialAbilities = @SpecialAbilities,
                                 Description = @Description,
                                 ImageURL = @ImageURL
                                 WHERE MonsterID = @MonsterID";

                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    cmd.Parameters.AddWithValue("@MonsterID", CurrentMonster.MonsterID);
                    cmd.Parameters.AddWithValue("@SourceBookID", CurrentMonster.SourceBookID);
                    cmd.Parameters.AddWithValue("@MonsterName", CurrentMonster.MonsterName);
                    cmd.Parameters.AddWithValue("@ArmorClass", CurrentMonster.ArmorClass);
                    cmd.Parameters.AddWithValue("@HitDice", CurrentMonster.HitDice);
                    cmd.Parameters.AddWithValue("@Attacks", CurrentMonster.Attacks);
                    cmd.Parameters.AddWithValue("@Alignment", CurrentMonster.Alignment);
                    cmd.Parameters.AddWithValue("@XP_Award", CurrentMonster.XP_Award);
                    cmd.Parameters.AddWithValue("@NumberAppearing", CurrentMonster.NumberAppearing);
                    cmd.Parameters.AddWithValue("@TreasureType", CurrentMonster.TreasureType);
                    cmd.Parameters.AddWithValue("@SpecialAbilities", CurrentMonster.SpecialAbilities);
                    cmd.Parameters.AddWithValue("@Description", CurrentMonster.Description);
                    cmd.Parameters.AddWithValue("@ImageURL", CurrentMonster.ImageURL);
                    cmd.ExecuteNonQuery();


                    //Update Genres: remove old and insert new
                    SqlCommand deleteCmd = new SqlCommand("DELETE FROM MonsterGenre WHERE MonsterID = @MonsterID", conn);
                    deleteCmd.Parameters.AddWithValue("@MonsterID", CurrentMonster.MonsterID);
                    deleteCmd.ExecuteNonQuery();
                    foreach (int genreId in SelectedGenreID)
                    {
                        SqlCommand genreCmd = new SqlCommand("INSERT INTO MonsterGenre (MonsterID, GenreID) VALUES (@MonsterID, @GenreID)", conn);
                        genreCmd.Parameters.AddWithValue("@MonsterID", CurrentMonster.MonsterID);
                        genreCmd.Parameters.AddWithValue("@GenreID", genreId);
                        genreCmd.ExecuteNonQuery();
                    }

                }

                return RedirectToPage("/Monsters/BrowseMonsters");
            }
            else
            {
                // repopulate and refresh
                PopulateSourceBookList();
                PopulateGenresList();
                return Page();
            }
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
                string query = "select MonsterID, SourceBookID, MonsterName, ArmorClass, HitDice, Attacks, Alignment, XP_Award, NumberAppearing, TreasureType, SpecialAbilities, Description, ImageURL, UserID From Monster WHERE MonsterID = @MonsterID";
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

                        };
                    }
                }
            }
        }

        // This method is called when the form is submitted
        private void PopulateGenresList()
        {
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string query = "SELECT GenreID, GenreName FROM Genre";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var genre = new GenreInfo();
                        genre.GenreID = int.Parse(reader["GenreID"].ToString());
                        genre.GenreName = reader["GenreName"].ToString();
                        genre.IsSelected = false;
                        Genres.Add(genre);
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
