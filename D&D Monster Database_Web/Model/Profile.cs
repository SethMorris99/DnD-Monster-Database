using System;
using System.ComponentModel.DataAnnotations;

public class Profile
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string ProfileImageURL { get; set; }
    public string AccountType { get; set; }
    public DateTime LastLoginTime { get; set; }

}
