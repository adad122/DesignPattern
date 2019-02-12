using PureMVC.App.Data;
using PureMVC.App.Mediator;
using PureMVC.App.Proxy;
using PureMVC.App.UI;
using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Facade;

namespace PureMVC.App.Command
{
    public class StartUpCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Facade.instance.RegisterCommand(Const.CMD_LOGIN, () => new LoginCommand());
            LoginViewMediator mediator = new LoginViewMediator {ViewComponent = new LoginView()};
            Facade.instance.RegisterMediator(mediator);
            Facade.instance.RegisterProxy(new LoginViewProxy());

            UserLoginData loginData = new UserLoginData();
            loginData.setAccount("acc123");
            loginData.setPassword("pwd123");
            Facade.instance.SendNotification(Const.CMD_LOGIN, loginData);
        }
    }
}