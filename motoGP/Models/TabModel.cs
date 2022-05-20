using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace motoGP.Models
{
    public class TabModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public TabModel(string str)
        {
            name = str;
        }
    }
}
