using System;
using System.Collections.Generic;
using PeshoFy.Classes;

namespace PeshoFy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //storage, login, register и ги подавам
            FileReader file = new FileReader();
            file.ReadFile();
            MenuService.DisplayMenu();
        }
    }
}
