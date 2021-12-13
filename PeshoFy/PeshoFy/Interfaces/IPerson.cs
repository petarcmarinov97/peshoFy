﻿using System;
using System.Collections.Generic;

namespace PeshoFy.Classes
{
    internal interface IPerson
    {
        DateTime DateOfBirth { get; set; }
        string FullName { get; set; }
        List<string> Genres { get; set; }
        string Password { get; set; }
        string Username { get; set; }
    }
}