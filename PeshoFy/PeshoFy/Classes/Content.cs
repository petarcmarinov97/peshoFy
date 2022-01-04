using System;
using System.Collections.Generic;
using System.Text;

namespace PeshoFy.Classes
{
    class Content : IContent
    {
        private string name;
        private string duration;

        protected Content(string name)
        {
            this.Name = name;
        }
        protected Content(string name, string duration)
        {
            this.Name = name;
            this.Duration = duration;
        }
        public string Duration { get => duration; set => duration = value; }
        public string Name { get => name; set => name = value; }

        virtual public string GetDurationTime()
        {
            return Duration;
        }
    }
}
