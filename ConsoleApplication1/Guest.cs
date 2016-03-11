using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Guest
    {
        //
        public int id { get; set; }
        public string prenom { get; set; }
        public string nomFamille { get; set; }
        public int age { get; set; }
        public int idCouple { get; set; }
        public int idGroupe { get; set; }
        public bool isPlace { get; set; }
        public int pointContent { get; set; }
    }
}
