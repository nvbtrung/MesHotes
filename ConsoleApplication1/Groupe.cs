using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Groupe
    {
        public int id { get; set; }
        public string nom { get; set; }
        public List<Guest> list { get; set; }

    }
}
