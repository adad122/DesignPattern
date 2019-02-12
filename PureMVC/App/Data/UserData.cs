
namespace PureMVC.App.Data
{
    public class UserData
    {
        private int userId;
        public int getUserId()
        {
            return userId;
        }
        public void setUserId(int userId)
        {
            this.userId = userId;
        }
        public string getName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        private string name;
    }

    public class UserLoginData
    {
        private string Account;
        public string getAccount()
        {
            return Account;
        }
        public void setAccount(string account)
        {
            Account = account;
        }

        private string Password;
        public string getPassword()
        {
            return Password;
        }
        public void setPassword(string password)
        {
            Password = password;
        }
    }
}