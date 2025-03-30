using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonsterDB_Business
{
    public class AppHelper
    {
        public static string GetDBConnectionString()
        {
            return "Server=(localdb)\\MSSQLLocalDB; Database=MonsterDatabase;Trusted_Connection = True;";
        }
        public static string GetDefaultProfilePicture()
        {
            return "https:///static.vecteezy.com/system/resources/previews/009/292/244/non_2x/default-avatar-icon-of-social-media-user-vector.jpg";
        }

        public static string GeneratePasswordHash(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return passwordHash;

        }
        public static bool VerifyPassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, correctHash);
        }
    }
}
