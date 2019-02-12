using PureMVC.App.Data;
using PureMVC.App.Proxy;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Facade;

namespace PureMVC.App.Command
{
    public class LoginCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            UserLoginData data = notification.Body as UserLoginData;
            LoginViewProxy loginViewProxy = Facade.instance.RetrieveProxy(LoginViewProxy.NAME) as LoginViewProxy;

            if (data == null || loginViewProxy == null)
                return;

            string name = notification.Name;
            switch (name)
            {
                case Const.CMD_LOGIN:
                    loginViewProxy.SendLogin(data.getAccount(), data.getPassword());
                    break;
            }
        }
    }
}