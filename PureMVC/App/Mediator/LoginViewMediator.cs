using System.Collections.Generic;
using System;
using System.Diagnostics;
using PureMVC.App.Data;
using PureMVC.App.UI;
using PureMVC.Interfaces;

namespace PureMVC.App.Mediator
{
    public class LoginViewMediator : Patterns.Mediator.Mediator
    {
        public new const string NAME = "LoginViewMediator";

        private LoginView UI
        {
            get
            {
                return ViewComponent as LoginView;
            }
        }

        public LoginViewMediator()
            : base(NAME)
        {
        }

        public override string[] ListNotificationInterests()
        {
            List<string> list = new List<string>()
            {
                MSG.MSG_LOGIN
            };

            return list.ToArray();
        }

        public override void HandleNotification(INotification notification)
        {
            string name = notification.Name;

            switch (name)
            {
                case MSG.MSG_LOGIN:
                    UserData data = notification.Body as UserData;

                    if (data != null)
                    {
                        Console.WriteLine("User Name: " + data.getName());
                        Console.WriteLine("User Id: " + data.getUserId());
                        UI.RefreshView(data);
                    }
                    break;
            }
        }
    }
}