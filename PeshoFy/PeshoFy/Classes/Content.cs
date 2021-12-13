using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    abstract class Content : IContent
    {
        private string name;
        private string duration;
        protected Content(string name, string duration)
        {
            this.name = name;
            this.duration = duration;
        }
        public string Duration { get => duration; set => duration = value; }
        public string Name { get => name; set => name = value; }
    }
}
