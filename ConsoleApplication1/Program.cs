using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {        
        public static void Solve(List<Guest> listGuest, List<Table> listTable)
        {
            int n = 5;
            //When we have a guest who dont have place
            while (listGuest.Count > 0)
            {
                listTable.ForEach(x =>
                    {
                        //When the table rest place
                        while (x.nbPlaceRest() > 0)
                        {
                            //When the table rest more than 1 place
                            if (x.nbPlaceRest() > 1)
                            {
                                //When the table had guest
                                if (x.list.Count != 0)
                                {
                                    //When we have a couple who had same last name with one guest in the table same age
                                    if (listGuest.Any(g => g.idCouple != 0 && x.list.Exists(nf => nf.firstName == g.firstName && Math.Abs(g.age - x.list.Average(d => d.age)) < n)))
                                    {
                                        Guest gCouple = listGuest.First(g => g.idCouple != 0 && x.list.Exists(nf => nf.firstName == g.firstName && Math.Abs(g.age - x.list.Average(d => d.age)) < n));
                                        Guest couple = listGuest.First(g => g.id == gCouple.idCouple);
                                        x.list.Add(gCouple); listGuest.Remove(gCouple);
                                        x.list.Add(couple);  listGuest.Remove(couple);
                                    }
                                    //no couple same last name same age
                                    else if (listGuest.Any(g => x.list.Exists(nf => nf.firstName == g.firstName && Math.Abs(g.age - x.list.Average(d => d.age)) < n && g.idCouple == 0)))
                                    {
                                        Guest gFamille = listGuest.First(g => x.list.Exists(nf => nf.firstName == g.firstName && Math.Abs(g.age - x.list.Average(d => d.age)) < n && g.idCouple == 0));
                                        x.list.Add(gFamille);  listGuest.Remove(gFamille);
                                    }
                                    //Couple same age
                                    else if (listGuest.Any(g => g.idCouple != 0 && x.list.Exists(nf => Math.Abs(g.age - x.list.Average(d => d.age)) < n)))
                                    {
                                        Guest gCouple = listGuest.First(g => g.idCouple != 0 && x.list.Exists(nf => Math.Abs(g.age - x.list.Average(d => d.age)) < n));
                                        Guest couple = listGuest.First(g => g.id == gCouple.idCouple);
                                        x.list.Add(gCouple); listGuest.Remove(gCouple);
                                        x.list.Add(couple);  listGuest.Remove(couple);
                                        Console.Out.WriteLine("Table : " + gCouple.firstName + " " + gCouple.lastName);
                                        Console.Out.WriteLine("Table : " + couple.firstName + " " + couple.lastName);
                                    }                                   
                                    //No couple same age
                                    else if (listGuest.Any(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n) && g.idCouple == 0))
                                    {
                                        Guest age = listGuest.FirstOrDefault(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n));
                                        x.list.Add(age);  listGuest.Remove(age);
                                    }
                                    //Rest
                                    else
                                    {
                                        Guest gCouple = listGuest.FirstOrDefault(g => g.idCouple != 0);
                                        //couple
                                        if (gCouple != null)
                                        {
                                            Guest couple = listGuest.FirstOrDefault(g => g.id == gCouple.idCouple);
                                            x.list.Add(gCouple); listGuest.Remove(gCouple);
                                            x.list.Add(couple);  listGuest.Remove(couple);
                                        }
                                        //Seul
                                        else
                                        {
                                            if (listGuest.Count == 0) return;                                                                                    
                                            while (listGuest.FirstOrDefault(g => g.idCouple == 0 && x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n)) == null)
                                            {
                                                n++;
                                            }
                                            Guest age = listGuest.FirstOrDefault(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n));
                                            x.list.Add(age); listGuest.Remove(age);
                                        }
                                    }
                                }
                                //Table empty
                                else
                                {
                                    //Find a couple 
                                    if (listGuest.Any(g => g.idCouple != 0))
                                    {
                                        Guest gCouple = listGuest.First(g => g.idCouple != 0);
                                        Guest couple = listGuest.First(g => g.id == gCouple.idCouple);
                                        x.list.Add(gCouple);  listGuest.Remove(gCouple);
                                        x.list.Add(couple);  listGuest.Remove(couple);
                                    }
                                    //no couple
                                    else
                                    {
                                        Guest first = listGuest.First(g => g.idCouple == 0);
                                        x.list.Add(first);  listGuest.Remove(first);
                                    }
                                }
                            }
                            //When the table rest 1 place
                            else
                            {
                                //Same last name same age no couple
                                if (listGuest.Any(g => x.list.Exists(nf => nf.firstName == g.firstName && Math.Abs(g.age - x.list.Average(d => d.age)) < n) && g.idCouple == 0))
                                {
                                    Guest gFamille = listGuest.First(g => x.list.Exists(nf => nf.firstName == g.firstName && Math.Abs(g.age - x.list.Average(d => d.age)) < n) && g.idCouple == 0);
                                    x.list.Add(gFamille); listGuest.Remove(gFamille);
                                }
                                //Same age no couple
                                else if (listGuest.Any(g => x.list.Exists(nf => Math.Abs(g.age - x.list.Average(d => d.age)) < n) && g.idCouple == 0))
                                {
                                    Guest age = listGuest.FirstOrDefault(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n) && g.idCouple == 0);
                                    x.list.Add(age); listGuest.Remove(age);
                                }
                                //no couple
                                else
                                {                                   
                                    break;
                                }
                            }
                        }
                    });
            }

        }

        static void Main(string[] args)
        {            
            List<Guest> listHote = new List<Guest>{
                new Guest{id=1,lastName="Anouck",firstName ="Aubry",age=60,idCouple=2},
                new Guest{id=2,lastName="Jean-Paul",firstName ="Aubry",age=60,idCouple=1},
                new Guest{id=3,lastName="Marie",firstName ="Augé",age=32,idCouple=4},
                new Guest{id=4,lastName="Romain",firstName ="Pilot",age=32,idCouple=3},
                new Guest{id=5,lastName="Matthieu",firstName ="Bach",age=32,idCouple=6},
                new Guest{id=6,lastName="Stéphanie",firstName ="Parron",age=32,idCouple=5},
                new Guest{id=7,lastName="Alain",firstName ="Bajard",age=65,idCouple=8},
                new Guest{id=8,lastName="Pascale",firstName ="Bajard",age=65,idCouple=7},
                new Guest{id=9,lastName="Biljana",firstName ="Bernet",age=30,idCouple=10},
                new Guest{id=10,lastName="Matthieu",firstName ="Bernet",age=30,idCouple=9},
                new Guest{id=11,lastName="Didier",firstName ="Bidault",age=50,idCouple=12},
                new Guest{id=12,lastName="Esma",firstName ="Bidault",age=47,idCouple=11},
                new Guest{id=13,lastName="Marie-Paule",firstName ="Blanchard",age=65,idCouple=14},
                new Guest{id=14,lastName="Joël",firstName ="Garo",age=60,idCouple=13},
                new Guest{id=15,lastName="David",firstName ="Bondoux",age=32,idCouple=16},
                new Guest{id=16,lastName="Laurence",firstName ="Bondoux",age=32,idCouple=15},
                new Guest{id=17,lastName="Christiane",firstName ="Bruneau",age=55,idCouple=18},
                new Guest{id=18,lastName="Jacques",firstName ="Bruneau",age=55,idCouple=17},
                new Guest{id=19,lastName="Aléxis",firstName ="Canart",age=40,idCouple=20},
                new Guest{id=20,lastName="Sabrina",firstName ="Demol",age=40,idCouple=19},
                new Guest{id=21,lastName="Guy",firstName ="Cornilleau",age=70,idCouple=22},
                new Guest{id=22,lastName="Liliane",firstName ="Cornilleau",age=70,idCouple=21},
                new Guest{id=23,lastName="Amandine",firstName ="Coste",age=33,idCouple=24},
                new Guest{id=24,lastName="Jérôme",firstName ="Passepont",age=33,idCouple=23},
                new Guest{id=25,lastName="Beppino",firstName ="De Santi",age=70,idCouple=26},
                new Guest{id=26,lastName="Anka",firstName ="Rusman",age=62,idCouple=25},
                new Guest{id=27,lastName="Priscille",firstName ="Demol",age=38,idCouple=28},
                new Guest{id=28,lastName="Sébastien",firstName ="Demol",age=38,idCouple=27},
                new Guest{id=29,lastName="Céline",firstName ="Février",age=28,idCouple=30},
                new Guest{id=30,lastName="Michael",firstName ="Thouvenin",age=31,idCouple=29},
                new Guest{id=31,lastName="Dominique",firstName ="Fischer",age=50,idCouple=32},
                new Guest{id=32,lastName="Hervé",firstName ="Fischer",age=50,idCouple=31},
                new Guest{id=33,lastName="Aymeric",firstName ="France",age=45,idCouple=34},
                new Guest{id=34,lastName="Céline",firstName ="Olszewski",age=42,idCouple=33},
                new Guest{id=35,lastName="Grégory",firstName ="Frossard",age=31,idCouple=36},
                new Guest{id=36,lastName="Than",firstName ="Frossard",age=31,idCouple=35},
                new Guest{id=37,lastName="Olivier",firstName ="Golinski",age=35,idCouple=38},
                new Guest{id=38,lastName="Vanessa",firstName ="Golinski",age=32,idCouple=37},
                new Guest{id=39,lastName="Charlotte",firstName ="Hubert",age=32,idCouple=40},
                new Guest{id=40,lastName="Jérôme",firstName ="Hubert",age=32,idCouple=39},
                new Guest{id=41,lastName="Esma",firstName ="Kazic",age=60,idCouple=42},
                new Guest{id=42,lastName="Samo",firstName ="Kazic",age=60,idCouple=41},
                new Guest{id=43,lastName="Goran",firstName ="Kazic",age=50,idCouple=44},
                new Guest{id=44,lastName="Isabelle",firstName ="Kazic",age=50,idCouple=43},
                new Guest{id=45,lastName="Jasim",firstName ="Kazic",age=42,idCouple=46},
                new Guest{id=46,lastName="Nurija",firstName ="Kazic",age=42,idCouple=45},
                new Guest{id=47,lastName="Kevin",firstName ="Kazic",age=32,idCouple=48},
                new Guest{id=48,lastName="Anne-Sophie",firstName ="Leneveu",age=32,idCouple=47},
                new Guest{id=49,lastName="Rukija",firstName ="Kazic",age=45,idCouple=50},
                new Guest{id=50,lastName="Sejad",firstName ="Kazic",age=60,idCouple=49},
                new Guest{id=51,lastName="Said",firstName ="Lalliti",age=50,idCouple=52},
                new Guest{id=52,lastName="Véronique",firstName ="Lalliti",age=49,idCouple=51},
                new Guest{id=53,lastName="François",firstName ="Legueux",age=33,idCouple=54},
                new Guest{id=54,lastName="Malika",firstName ="Legueux",age=33,idCouple=53},
                new Guest{id=55,lastName="Françoise",firstName ="Leneveu",age=63,idCouple=56},
                new Guest{id=56,lastName="Gérard",firstName ="Leneveu",age=66,idCouple=55},
                new Guest{id=57,lastName="Bernard",firstName ="Menguy",age=75,idCouple=58},
                new Guest{id=58,lastName="Jaqueline",firstName ="Menguy",age=75,idCouple=57},
                new Guest{id=59,lastName="Franck",firstName ="Mentzer",age=75,idCouple=60},
                new Guest{id=60,lastName="Maryse",firstName ="Mentzer",age=75,idCouple=59},
                new Guest{id=61,lastName="Angélique",firstName ="Pique",age=33,idCouple=62},
                new Guest{id=62,lastName="Mathieu",firstName ="Ponzoni",age=33,idCouple=61},
                new Guest{id=63,lastName="Émilie",firstName ="Ponzoni",age=33,idCouple=64},
                new Guest{id=64,lastName="Thomas",firstName ="Ponzoni",age=33,idCouple=63},
                new Guest{id=65,lastName="Eric",firstName ="Ragueneau",age=50,idCouple=66},
                new Guest{id=66,lastName="Mélanie",firstName ="Ragueneau",age=50,idCouple=65},
                new Guest{id=67,lastName="Bernard",firstName ="Segreto",age=65,idCouple=68},
                new Guest{id=68,lastName="Michèle",firstName ="Segreto",age=65,idCouple=67},
                new Guest{id=69,lastName="Arezki",firstName ="Slimi",age=70,idCouple=70},
                new Guest{id=70,lastName="Jeanine",firstName ="Slimi",age=60,idCouple=69},
                new Guest{id=71,lastName="Michel",firstName ="Tuphin",age=75,idCouple=72},
                new Guest{id=72,lastName="Sylviane",firstName ="Tuphin",age=75,idCouple=71},
                new Guest{id=73,lastName="Marie-Thérèse",firstName ="Vannier",age=70,idCouple=74},
                new Guest{id=74,lastName="Serge",firstName ="Vannier",age=70,idCouple=73},
                new Guest{id=75,lastName="Arnaud",firstName ="Vautier",age=42,idCouple=76},
                new Guest{id=76,lastName="Christelle",firstName ="Vautier",age=42,idCouple=75},
                new Guest{id=77,lastName="Martine",firstName ="Olivrin",age=60,idCouple=78},
                new Guest{id=78,lastName="Yvon",firstName ="Olivrin",age=60,idCouple=77},
                new Guest{id=79,lastName="Armony",firstName ="Aubry",age=16,idCouple=0},
                new Guest{id=80,lastName="Anaelle",firstName ="Bidault",age=16,idCouple=0},
                new Guest{id=81,lastName="Corentin",firstName ="Bidault",age=19,idCouple=0},
                new Guest{id=82,lastName="Martial",firstName ="Blanchard",age=90,idCouple=0},
                new Guest{id=83,lastName="Annick",firstName ="Bozier",age=62,idCouple=0},
                new Guest{id=84,lastName="Océane",firstName ="Canart",age=5,idCouple=0},
                new Guest{id=85,lastName="Emilie",firstName ="Chollat",age=32,idCouple=0},
                new Guest{id=86,lastName="Dominique",firstName ="Courtot",age=60,idCouple=0},
                new Guest{id=87,lastName="Alain",firstName ="Demol",age=61,idCouple=0},
                new Guest{id=88,lastName="Josée",firstName ="Demol",age=62,idCouple=0},
                new Guest{id=89,lastName="",firstName ="Dj 1",age=45,idCouple=90},
                new Guest{id=90,lastName="",firstName ="Dj2",age=40,idCouple=89},
                new Guest{id=91,lastName="Anais",firstName ="Fischer",age=26,idCouple=0},
                new Guest{id=92,lastName="Tiphaine",firstName ="Fischer",age=19,idCouple=0},
                new Guest{id=93,lastName="Julien",firstName ="Gries",age=32,idCouple=0},
                new Guest{id=94,lastName="Zineb",firstName ="Haddadi",age=38,idCouple=0},
                new Guest{id=95,lastName="Maxime",firstName ="Hirtz",age=32,idCouple=0},
                new Guest{id=96,lastName="Vincent",firstName ="Hornsperger",age=32,idCouple=0},
                new Guest{id=97,lastName="Christophe",firstName ="Jucker",age=36,idCouple=0},
                new Guest{id=98,lastName="Anna",firstName ="Kazic",age=85,idCouple=0},
                new Guest{id=99,lastName="Florian",firstName ="Kazic",age=19,idCouple=0},
                new Guest{id=100,lastName="Lucas",firstName ="Kazic",age=13,idCouple=0},
                new Guest{id=101,lastName="Mujo",firstName ="Kazic",age=45,idCouple=0},
                new Guest{id=102,lastName="Sandra",firstName ="Kazic",age=38,idCouple=0},
                new Guest{id=103,lastName="Leila",firstName ="Lalliti",age=22,idCouple=0},
                new Guest{id=104,lastName="Soreya",firstName ="Lalliti",age=18,idCouple=0},
                new Guest{id=105,lastName="Christiane",firstName ="Lambert",age=80,idCouple=0},
                new Guest{id=106,lastName="Benjamin",firstName ="Legueux",age=30,idCouple=0},
                new Guest{id=107,lastName="Vanessa",firstName ="Mellon",age=32,idCouple=0},
                new Guest{id=108,lastName="Pascal",firstName ="Olery",age=32,idCouple=0},
                new Guest{id=109,lastName="Alexandre",firstName ="Piel",age=32,idCouple=0},
                new Guest{id=110,lastName="Daniel",firstName ="Rusman",age=34,idCouple=0},
                new Guest{id=111,lastName="Stéphane",firstName ="Saquet",age=32,idCouple=0},
                new Guest{id=112,lastName="Hedi",firstName ="Sellami",age=32,idCouple=0},
                new Guest{id=113,lastName="Alexis",firstName ="Slimi",age=23,idCouple=0},
                new Guest{id=114,lastName="Fatima",firstName ="Slimi",age=38,idCouple=0},
                new Guest{id=115,lastName="Cyril",firstName ="Vermandé",age=32,idCouple=0}

            };

            //Console.Out.WriteLine("List des hôtes :");
            //Console.Out.WriteLine("|--lastName--|--Nom--|--Age--|--Coupe--|");
            //foreach (Guest h in listHote)
            //{
            //    Console.Out.WriteLine(h.lastName + " | " + h.firstName + " | " + h.age + " | " + h.idCouple + " |");
            //}           
            List<Table> listTable = new List<Table>{
                new Table(1,10,"Table 1"),
                new Table(2,10, "Table 2"),
                new Table(3,10,"Table 3"),
                new Table(4,10,"Table 4"),
                new Table(5,10,"Table 5"),
                new Table(6,10,"Table 6"),
                new Table(7,10,"Table 7"),
                new Table(8,10,"Table 8"),
                new Table(9,10,"Table 9"),
                new Table(10,10,"Table 10"),
                new Table(11,10,"Table 11"),
                new Table(12,10,"Table 12")
            };          
            Solve(listHote, listTable);           
            //Console.Out.WriteLine("|--------------Table-------------------|");
            //foreach (Table t in listTable)
            //{
            //    Console.Out.WriteLine(t.nom);
            //    Console.Out.WriteLine("|--Hôte--|");
            //    foreach (Guest h in t.list)
            //    {
            //        Console.Out.WriteLine(h.firstName + " " + h.lastName + "|" + h.age);
            //    }
            //}
        }
    }
}
