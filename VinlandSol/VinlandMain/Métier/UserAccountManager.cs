using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VinlandSol.IHM;

namespace VinlandSol.Métier
{
    class UserAccountManager
    {
        private List<UserAccount> userAccounts;
        private string userAccountFilePath = "userAccounts.txt";

        public UserAccountManager()
        {
            userAccounts = new List<UserAccount>();
            LoadUserAccounts();
        }

        public void CreateUserAccount(string username, string password)
        {
            UserAccount newUserAccount = new UserAccount(username, password);
            userAccounts.Add(newUserAccount);
            SaveUserAccounts();
        }

        public bool VerifyUserAccount(string username, string password)
        {
            return userAccounts.Any(account => account.Username == username && account.Password == password);
        }

        private void LoadUserAccounts()
        {
            if (File.Exists(userAccountFilePath))
            {
                string[] lines = File.ReadAllLines(userAccountFilePath);
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        string username = parts[0];
                        string password = parts[1];
                        userAccounts.Add(new UserAccount(username, password));
                    }
                }
            }
        }

        private void SaveUserAccounts()
        {
            List<string> lines = userAccounts.Select(account => $"{account.Username},{account.Password}").ToList();
            File.WriteAllLines(userAccountFilePath, lines);
        }
    }
}
