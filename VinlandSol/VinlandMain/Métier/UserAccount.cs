using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinlandSol.Métier
{
    class UserAccount
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public UserAccount(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
