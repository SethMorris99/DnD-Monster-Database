namespace D_D_Monster_Database_Web.Model
{
    public class AccountView
    {
        public string SystemUserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string ProfileImageURL { get; set; }
        public string AccountType { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
