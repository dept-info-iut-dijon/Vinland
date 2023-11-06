using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.IHM;

namespace VinlandSol.Métier
{
    class UserManager
    {
        private List<User> users;

        public UserManager()
        {
            users = new List<User>();
            LoadUsers();
        }

        public void CreateUser(string nom, string mdp)
        {
            LoadUsers();
            int id = 0; // Last User
            User newUser = new User(id, nom, mdp);
            users.Add(newUser);
            SaveUsers();
        }

        public bool VerifyUser(string nom, string mdp)
        {
            return users.Any(account => account.Nom == nom && account.Mdp == mdp);
        }

        private void LoadUsers()
        {
            /// Remplir users
        }

        private void SaveUsers()
        {
            /// Update bdd
        }
    }
}
