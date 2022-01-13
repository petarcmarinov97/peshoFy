using System;
using System.Collections.Generic;

namespace PeshoFy.Classes
{
    public interface IUser
    {
        string DateOfBirth { get; set; }
        string FullName { get; set; }
        List<string> Genres { get; set; }
        string Password { get; set; }
        string Username { get; set; }

        public string PrintCollection(Constants.typeCollection type);
    }
}