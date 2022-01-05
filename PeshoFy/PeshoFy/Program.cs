using System;
using System.Collections.Generic;
using PeshoFy.Classes;

namespace PeshoFy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileReader file = new FileReader();
            file.ReadFile();
            MenuService.DisplayMenu();
        }
    }
}
