using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using PureMVC.App;
using PureMVC.App.Command;
using PureMVC.Patterns.Facade;

namespace PureMVC
{
    class Program
    {
        static void Main(string[] args)
        {
            Facade.instance.RegisterCommand(Const.CMD_START_UP, () => new StartUpCommand());
            Facade.instance.SendNotification(Const.CMD_START_UP);
            Facade.instance.RemoveCommand(Const.CMD_START_UP);
        }
    }
}
