using PureMVC.App.Data;

namespace PureMVC.App.Proxy
{
    public class LoginViewProxy : Patterns.Proxy.Proxy
    {
        public new const string NAME = "LoginViewProxy";

        public UserData userData;

        public LoginViewProxy()
            : base(NAME)
        {
            userData = new UserData();
        }

        public void SendLogin(string account, string password)
        {
            //假装完成通信
            userData.setName("Test1");
            userData.setUserId(1234567);
            OnReciveLogin(userData);
        }

        private void OnReciveLogin(UserData data)
        {
            SendNotification(MSG.MSG_LOGIN, data);
        }
    }
}