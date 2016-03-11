using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Table
    {
        public int id { get; set; }
        public string nom { get; set; }
        public bool isParticulier { get; set; }
        public List<Guest> list { get; set; }
        public int nbPlaceMax { get; set; }
           
        public Table()
        {
            id = 0;
            nom = "";
            isParticulier = false;
            list = null;
            nbPlaceMax = 0;
        }

        public Table(int id, int nbPlaceMax, string nom)
        {
            this.id = id;
            this.nom = nom;
            this.list = new List<Guest>(nbPlaceMax);
            this.nbPlaceMax = nbPlaceMax;
            this.isParticulier = false;
        }
        public int nbPlaceRest() { return (list == null) ? nbPlaceMax : nbPlaceMax - list.Count; }
    }
}
