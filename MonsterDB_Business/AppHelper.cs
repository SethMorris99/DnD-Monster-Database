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

        public string GeneraatePasswordHash(string password)
        {
            string passswordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(password);
            return passswordHash;

        }
        public static bool VerifyPassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.EnhancedVerify(password, correctHash);
        }
    }
}
