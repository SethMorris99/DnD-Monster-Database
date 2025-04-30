using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using D_D_Monster_Database_Web.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using MonsterDB_Business;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace D_D_Monster_Database_Web.Pages.Monsters
{
    [Authorize] // This attribute is used to restrict access to the page to authenticated users
    [BindProperties] // BindProperty attribute is used to bind the property to the form data
    public class AddMonsterModel : PageModel
    {
        public Monster NewMonster { get; set; }

        // List of source books for the dropdown
        public List<SelectListItem> SourceBook { get; set; } = new List<SelectListItem>();
        // List of treasure types for the dropdown
        //public List<SelectListItem> TreasureType { get; set; } = new List<SelectListItem>();
        // List of special abilities for the dropdown
        public List<GenreInfo> Genres { get; set; } = new List<GenreInfo>();
        // List of special abilities for the dropdown
        public List<int> SelectedGenreID { get; set; } = new List<int>();

        public void OnGet()
        {
            // This method is called when the page is first loaded
            PopulateSourceBookList();
            // This method is commented out for now since we are considering adding a table for treasure types
            //PopulateTreasureTypeList();
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
            //Validate User Input
            if (ModelState.IsValid)
            {
                //Save to Database 
                //1. Create a connection to the databse 
                // string connectionString = "Server=(localdb)\\MSSQLLocalDB; Database=MonsterDatabase;Trusted_Connection = True;";

                //make a local variable 
                using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
                {
                    //2. Create a command to insert the data
                    string cmdText = @"INSERT INTO Monster 
                                     (SourceBookID,MonsterName,ArmorClass,
                                     HitDice,Attacks,Alignment,
                                     XP_Award, NumberAppearing, TreasureType,
                                     SpecialAbilities,Description,ImageURL, UserID)
                                     VALUES 
                                     (@SourceBookID,@MonsterName,@ArmorClass, @HitDice,
                                     @Attacks,@Alignment,@XP_Award,@NumberAppearing,
                                     @TreasureType, @SpecialAbilities,@Description,@ImageURL, @UserID);SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(cmdText, conn);
                    conn.Open();
                    cmd.Parameters.AddWithValue("@SourceBookID", NewMonster.SourceBookID);
                    cmd.Parameters.AddWithValue("@MonsterName", NewMonster.MonsterName);
                    cmd.Parameters.AddWithValue("@ArmorClass", NewMonster.ArmorClass);
                    cmd.Parameters.AddWithValue("@HitDice", NewMonster.HitDice);
                    cmd.Parameters.AddWithValue("@Attacks", NewMonster.Attacks);
                    cmd.Parameters.AddWithValue("@Alignment", NewMonster.Alignment);
                    cmd.Parameters.AddWithValue("@XP_Award", NewMonster.XP_Award);
                    cmd.Parameters.AddWithValue("@NumberAppearing", NewMonster.NumberAppearing);
                    cmd.Parameters.AddWithValue("@TreasureType", NewMonster.TreasureType);
                    cmd.Parameters.AddWithValue("@SpecialAbilities", NewMonster.SpecialAbilities);
                    cmd.Parameters.AddWithValue("@Description", NewMonster.Description);
                    cmd.Parameters.AddWithValue("@ImageURL", NewMonster.ImageURL);
                    int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                    cmd.Parameters.AddWithValue("@UserID", userId);

                    //capture MonsterID
                    int monsterID = Convert.ToInt32(cmd.ExecuteScalar());

                    foreach (int genreId in SelectedGenreID)
                    {
                        string genreCmdText = "INSERT INTO MonsterGenre (MonsterID, GenreID) VALUES (@MonsterID, @GenreID)";
                        using (SqlCommand genreCmd = new SqlCommand(genreCmdText, conn))
                        {
                            genreCmd.Parameters.AddWithValue("@MonsterID", monsterID);
                            genreCmd.Parameters.AddWithValue("@GenreID", genreId);
                            genreCmd.ExecuteNonQuery();
                        }
                    }

                }
                //Redirect to AddMonster Page
                return RedirectToPage("/Monsters/BrowseMonsters");
            }
            else
            {
                //Redirect the user 
                PopulateSourceBookList();
                PopulateGenresList();
                return Page();
            }
        }
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

        /*
        private void PopulateTreasureTypeList()
        {
            // Implementation for PopulateTreasureType
            using (SqlConnection conn = new SqlConnection(AppHelper.GetDBConnectionString()))
            {
                string query = "SELECT TreasureTypeID, TreasureTypeName FROM TreasureType";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var treasureType = new SelectListItem();
                        treasureType.Value = reader["TreasureTypeID"].ToString();
                        treasureType.Text = reader["TreasureTypeName"].ToString();
                        TreasureType.Add(treasureType);
                    }
                    var defaultTreasureType = new SelectListItem();
                    defaultTreasureType.Value = "0";
                    defaultTreasureType.Text = "--Select Treasure Type--";
                    TreasureType.Insert(0, defaultTreasureType);

                }
            }
        }
        */

        public class GenreInfo
        {
            public int GenreID { get; set; }
            public string GenreName { get; set; }
            public bool IsSelected { get; set; }
        }
    }
}
