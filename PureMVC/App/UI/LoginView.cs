using System;
using PureMVC.App.Data;

namespace PureMVC.App.UI
{
    public class LoginView
    {
        public void RefreshView(UserData data)
        {
            Console.WriteLine(data.getName() + " login Success!");
        }
    }
}