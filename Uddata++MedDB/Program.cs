using System;

namespace Uddata__MedDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Menus menus = new Menus();
            do
            {
                menus.WelcomeMenu();
            } while (true);
        }
    }
}
