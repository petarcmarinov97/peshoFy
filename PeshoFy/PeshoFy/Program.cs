using System;
using System.Collections.Generic;
using PeshoFy.Classes;

namespace PeshoFy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MenuController menu = new MenuController();
            menu.Menu();
            ReadData.ShowDisplayByType();
        }
    }
}
