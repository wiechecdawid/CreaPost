using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreaPost.Services
{
    public interface IOwner
    {
        string GetFooter();
    }

    public class Owner : IOwner
    {
        private string _name;
        private DateTime _date;
        public Owner()
        {
            _name = "Dawid Wiecheć";
            _date = DateTime.Now;
        }
        public string GetFooter()
        {
            return new string($"Copyright ©{_date.Year}, {_name}. All rights reserved.");
        }
    }
}
