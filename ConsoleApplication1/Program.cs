using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {


        public static List<Table> Solve(List<Guest> listHote, List<Table> listTable)
        {
            List<Table> listResult = new List<Table>();
            //Place first all the couple
            listResult = MettreLesCouple(listHote, listTable);
            listResult = MettreLesReste(listHote, listResult);
            return listResult;
        }



        private static List<Table> MettreLesReste(List<Guest> listHote, List<Table> listResult)
        {
            List<Table> result = listResult;
            foreach (Guest h in listHote)
            {
                //Check if this guest had a place or not
                if (!h.isPlace)
                {
                    Table tableEmpty = listResult.FirstOrDefault(t => t.nbPlaceRest() == t.nbPlaceMax);
                    if (tableEmpty != null)
                    {
                        tableEmpty.list.Add(h); h.isPlace = true;
                    }
                    else
                    {
                        int contentTemp = h.pointContent;
                        int tableChoisit = -1;
                        foreach (Table t in listResult)
                        {
                            if (t.nbPlaceRest() >= 1)
                            {
                                if (contentTemp <= contentAvecTable(h, t))
                                {
                                    contentTemp = contentAvecTable(h, t);
                                    tableChoisit = t.id;
                                }
                            }
                        }
                        result.First(t => t.id == tableChoisit).list.Add(h); h.isPlace = true;
                    }
                }
            }
            return result;
        }
        public static int contentAvecTable(Guest h, Table t)
        {
            int content = h.pointContent;
            foreach (Guest d in t.list)
            {
                content += contentAvecHote(h, d);
            }
            return content;
        }

        public static int contentAvecList(Guest h, List<Guest> t)
        {
            int content = h.pointContent;
            foreach (Guest d in t)
            {
                content += contentAvecHote(h, d);
            }
            return content;
        }
        private static int contentAvecHote(Guest h, Guest d)
        {
            int content = h.pointContent;
            if (Math.Abs(h.age - d.age) <= 3) content += 10;
            //if (h.nomFamille == d.nomFamille) content += 50;
            if (h.idCouple == d.id) content += 100;
            return content;
        }
        public static List<Table> MettreLesCouple(List<Guest> listHote, List<Table> listTable)
        {
            foreach (Guest h in listHote)
            {
                //Check if this guest had a place or not
                if (!h.isPlace)
                {
                    //Check if this guest had couple or not
                    if (h.idCouple != 0)
                    {
                        //Find the couple of h
                        Guest couple = listHote.First(d => d.id == h.idCouple);

                        //Find the first empty table
                        Table table = listTable.FirstOrDefault(t => t.nbPlaceRest() < t.nbPlaceMax);
                        if (table == null)
                        {
                            table = listTable.ElementAt(0);
                            //Place the couple in the table
                            table.list.Add(h); h.isPlace = true;
                            table.list.Add(couple); couple.isPlace = true;
                            continue;
                        }
                        else
                        {
                            //Find the first table which one rest more than 2 places
                            Table tableRest = listTable.FirstOrDefault(t => t.nbPlaceRest() < t.nbPlaceMax);
                            if (tableRest != null)
                            {
                                //Place the couple in the table
                                tableRest.list.Add(h); h.isPlace = true;
                                tableRest.list.Add(couple); couple.isPlace = true;
                                continue;
                            }
                            else
                                //Dont have place for this couple
                                throw new Exception("Dont have enough place for couple");
                        }
                    }
                }
            }
            return listTable;
        }


        /*
         * 
         * 
         * 
         * 
         * 
         * 
         */
        //public static void Solve2(List<Guest> listGuest, List<Table> listTable)
        //{
        //    List<Guest>[] list = new List<Guest>[listGuest.Count];
        //    int i = 0;
        //    while (listGuest.Count != 0)
        //    {
        //        string nomFamille = listGuest.ElementAt(0).nomFamille;
        //        foreach (Guest g in listGuest.FindAll(g => g.nomFamille == nomFamille))
        //        {
        //            if (list[i] == null) list[i] = new List<Guest>();
        //            list[i].Add(g);
        //            listGuest.Remove(g);
        //        }
        //        i++;
        //    }

        //    Calcule point content and sort with point content
        //    list.ToList().ForEach(x =>
        //    {
        //        if (x == null) return;
        //        x.ForEach(y =>
        //        {
        //            y.pointContent = contentAvecList(y, x);
        //        });
        //        x.OrderBy(y => y.pointContent);
        //    });

        //    foreach (List<Guest> l in list)
        //    {
        //        if (l == null) break;
        //        foreach (Guest g in l)
        //        {
        //            g.pointContent = contentAvecList(g, l);
        //        }

        //        l.Sort(delegate(Guest a, Guest b)
        //        {
        //            return a.pointContent.CompareTo(b.pointContent);
        //        });
        //    }


        //    foreach (List<Guest> l in list)
        //    {
        //        if (l == null) continue;
        //        else
        //        {
        //            Table t = listTable.OrderBy(td => td.nbPlaceRest()).ElementAt(0);
        //            while (l.Count > t.nbPlaceMax)
        //            {
        //                Guest g = l.ElementAt(0);
        //                if (g.idCouple != 0)
        //                {
        //                    if (t.nbPlaceRest() < 2) t = listTable.First(td => td.nbPlaceRest() == td.nbPlaceMax);
        //                    else
        //                    {
        //                        t.list.Add(g); g.isPlace = true; l.Remove(g);
        //                        Guest couple = l.First(c => c.id == g.idCouple);
        //                        t.list.Add(couple); couple.isPlace = true; l.Remove(couple);
        //                    }
        //                }
        //                else
        //                {
        //                    t.list.Add(g); l.Remove(g); g.isPlace = true;
        //                }
        //                if (t.nbPlaceRest() == t.nbPlaceMax) t = listTable.First(td => td.nbPlaceRest() == td.nbPlaceMax);
        //            }
        //            t = listTable.OrderBy(td => td.nbPlaceRest()).ElementAt(0);
        //            t.list.AddRange(l);
        //        }
        //    }



        //    foreach (Table tt in listTable)
        //    {
        //        Console.Out.WriteLine("---Table " + tt.id + "------");
        //        foreach (Guest g in tt.list)
        //        {
        //            Console.Out.WriteLine(g.prenom + " " + g.nomFamille + " " + g.pointContent);
        //        }
        //        Console.Out.WriteLine();
        //    }
            

        //}

        //public static void preTri()
        //{

        //}

        public static void Solve3(List<Guest> listGuest, List<Table> listTable)
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
                                    if (listGuest.Any(g => g.idCouple != 0 && x.list.Exists(nf => nf.nomFamille == g.nomFamille && Math.Abs(g.age - x.list.Average(d=>d.age)) < n )))
                                    {
                                        Guest gCouple = listGuest.First(g => g.idCouple != 0 && x.list.Exists(nf => nf.nomFamille == g.nomFamille && Math.Abs(g.age - x.list.Average(d => d.age)) < n));
                                        Guest couple = listGuest.First(g => g.id == gCouple.idCouple);
                                        x.list.Add(gCouple); gCouple.isPlace = true; listGuest.Remove(gCouple);
                                        x.list.Add(couple); couple.isPlace = true; listGuest.Remove(couple);                                        
                                    }
                                    //no couple same last name same age
                                    else if (listGuest.Any(g => x.list.Exists(nf => nf.nomFamille == g.nomFamille && Math.Abs(g.age - x.list.Average(d => d.age)) < n && g.idCouple == 0)))
                                    {
                                        Guest gFamille = listGuest.First(g => x.list.Exists(nf => nf.nomFamille == g.nomFamille && Math.Abs(g.age - x.list.Average(d=>d.age)) < n  && g.idCouple == 0));
                                        x.list.Add(gFamille); gFamille.isPlace = true; listGuest.Remove(gFamille);
                                    }
                                    //Couple same age
                                    else if (listGuest.Any(g => g.idCouple != 0 && x.list.Exists(nf => Math.Abs(g.age - x.list.Average(d=>d.age)) < n )))
                                    {
                                        Guest gCouple = listGuest.First(g => g.idCouple != 0 && x.list.Exists(nf => Math.Abs(g.age - x.list.Average(d=>d.age)) < n ));
                                        Guest couple = listGuest.First(g => g.id == gCouple.idCouple);
                                        x.list.Add(gCouple); gCouple.isPlace = true; listGuest.Remove(gCouple);
                                        x.list.Add(couple); couple.isPlace = true; listGuest.Remove(couple);
                                        Console.Out.WriteLine("Table : " + gCouple.nomFamille + " " + gCouple.prenom);
                                        Console.Out.WriteLine("Table : " + couple.nomFamille + " " + couple.prenom);
                                    }
                                    //Same first name same age
                                    else if (listGuest.Any(g => x.list.Exists(nf => nf.nomFamille == g.nomFamille) && Math.Abs(g.age - x.list.Average(d => d.age)) < n))
                                    {
                                        Guest nameAge = listGuest.First(g => x.list.Exists(nf => nf.nomFamille == g.nomFamille) && Math.Abs(g.age - x.list.Average(d => d.age)) < n);
                                        x.list.Add(nameAge); nameAge.isPlace = true; listGuest.Remove(nameAge);
                                    }
                                    //Same age
                                    else if(listGuest.Any(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d=>d.age)) < n )))
                                    {
                                        Guest age = listGuest.FirstOrDefault(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d=>d.age)) < n ));
                                        x.list.Add(age); age.isPlace = true; listGuest.Remove(age);
                                    }
                                    //Rest
                                    else {
                                        Guest gCouple = listGuest.FirstOrDefault(g => g.idCouple != 0);
                                        //couple
                                        if (gCouple != null)
                                        {
                                            Guest couple = listGuest.FirstOrDefault(g => g.id == gCouple.idCouple);
                                            x.list.Add(gCouple); gCouple.isPlace = true; listGuest.Remove(gCouple);
                                            x.list.Add(couple); couple.isPlace = true; listGuest.Remove(couple);
                                        }
                                        //Seul
                                        else
                                        {
                                            if (listGuest.Count == 0) return;
                                            //Guest seul = listGuest.ElementAt(0);
                                            //x.list.Add(seul); seul.isPlace = true; listGuest.Remove(seul);                                            
                                            while (listGuest.FirstOrDefault(g =>g.idCouple == 0 && x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n)) == null)
                                            {
                                                n++;                                              
                                            }
                                            Guest age = listGuest.FirstOrDefault(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d => d.age)) < n));
                                            x.list.Add(age); age.isPlace = true; listGuest.Remove(age);
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
                                        x.list.Add(gCouple); gCouple.isPlace = true; listGuest.Remove(gCouple);
                                        x.list.Add(couple); couple.isPlace = true; listGuest.Remove(couple);
                                    }
                                    //no couple
                                    else
                                    {
                                        Guest first = listGuest.First(g => g.idCouple == 0);
                                        x.list.Add(first); first.isPlace = true; listGuest.Remove(first);
                                    }
                                }
                            }
                            //When the table rest 1 place
                            else
                            {
                                //Same last name same age no couple
                                if (listGuest.Any(g => x.list.Exists(nf => nf.nomFamille == g.nomFamille && Math.Abs(g.age - x.list.Average(d=>d.age)) < n ) && g.idCouple == 0))
                                {
                                    Guest gFamille = listGuest.First(g => x.list.Exists(nf => nf.nomFamille == g.nomFamille && Math.Abs(g.age - x.list.Average(d=>d.age)) < n ) && g.idCouple == 0);
                                    x.list.Add(gFamille); gFamille.isPlace = true; listGuest.Remove(gFamille);
                                }
                                //Same age no couple
                                else if (listGuest.Any(g => x.list.Exists(nf => Math.Abs(g.age - x.list.Average(d=>d.age)) < n) && g.idCouple == 0))
                                {
                                    Guest age = listGuest.FirstOrDefault(g => x.list.Exists(a => Math.Abs(g.age - x.list.Average(d=>d.age)) < n ) && g.idCouple == 0);
                                    x.list.Add(age); age.isPlace = true; listGuest.Remove(age);
                                }
                                //no couple
                                else
                                {
                                    //if (listGuest.Count == 0) return;
                                    //Guest first = listGuest.First(g => g.idCouple == 0);
                                    //x.list.Add(first); first.isPlace = true; listGuest.Remove(first);
                                    break;
                                }
                            }
                        }
                    });
            }

        }

        static void Main(string[] args)
        {
            List<Groupe> listGroupe = new List<Groupe>{
                new Groupe{id = 1, list = new List<Guest>(), nom = "Colluege"},
                new Groupe{id = 2, list = new List<Guest>(), nom = "Entreprise"},
                new Groupe{id = 3, list = new List<Guest>(), nom = "Class"}
            };
            List<Guest> listHote = new List<Guest>{
               new Guest{id=1,prenom="Anouck",nomFamille ="Aubry",age=60,idCouple=2,isPlace=false},
new Guest{id=2,prenom="Jean-Paul",nomFamille ="Aubry",age=60,idCouple=1,isPlace=false},
new Guest{id=3,prenom="Marie",nomFamille ="Augé",age=32,idCouple=4,isPlace=false},
new Guest{id=4,prenom="Romain",nomFamille ="Pilot",age=32,idCouple=3,isPlace=false},
new Guest{id=5,prenom="Matthieu",nomFamille ="Bach",age=32,idCouple=6,isPlace=false},
new Guest{id=6,prenom="Stéphanie",nomFamille ="Parron",age=32,idCouple=5,isPlace=false},
new Guest{id=7,prenom="Alain",nomFamille ="Bajard",age=65,idCouple=8,isPlace=false},
new Guest{id=8,prenom="Pascale",nomFamille ="Bajard",age=65,idCouple=7,isPlace=false},
new Guest{id=9,prenom="Biljana",nomFamille ="Bernet",age=30,idCouple=10,isPlace=false},
new Guest{id=10,prenom="Matthieu",nomFamille ="Bernet",age=30,idCouple=9,isPlace=false},
new Guest{id=11,prenom="Didier",nomFamille ="Bidault",age=50,idCouple=12,isPlace=false},
new Guest{id=12,prenom="Esma",nomFamille ="Bidault",age=47,idCouple=11,isPlace=false},
new Guest{id=13,prenom="Marie-Paule",nomFamille ="Blanchard",age=65,idCouple=14,isPlace=false},
new Guest{id=14,prenom="Joël",nomFamille ="Garo",age=60,idCouple=13,isPlace=false},
new Guest{id=15,prenom="David",nomFamille ="Bondoux",age=32,idCouple=16,isPlace=false},
new Guest{id=16,prenom="Laurence",nomFamille ="Bondoux",age=32,idCouple=15,isPlace=false},
new Guest{id=17,prenom="Christiane",nomFamille ="Bruneau",age=55,idCouple=18,isPlace=false},
new Guest{id=18,prenom="Jacques",nomFamille ="Bruneau",age=55,idCouple=17,isPlace=false},
new Guest{id=19,prenom="Aléxis",nomFamille ="Canart",age=40,idCouple=20,isPlace=false},
new Guest{id=20,prenom="Sabrina",nomFamille ="Demol",age=40,idCouple=19,isPlace=false},
new Guest{id=21,prenom="Guy",nomFamille ="Cornilleau",age=70,idCouple=22,isPlace=false},
new Guest{id=22,prenom="Liliane",nomFamille ="Cornilleau",age=70,idCouple=21,isPlace=false},
new Guest{id=23,prenom="Amandine",nomFamille ="Coste",age=33,idCouple=24,isPlace=false},
new Guest{id=24,prenom="Jérôme",nomFamille ="Passepont",age=33,idCouple=23,isPlace=false},
new Guest{id=25,prenom="Beppino",nomFamille ="De Santi",age=70,idCouple=26,isPlace=false},
new Guest{id=26,prenom="Anka",nomFamille ="Rusman",age=62,idCouple=25,isPlace=false},
new Guest{id=27,prenom="Priscille",nomFamille ="Demol",age=38,idCouple=28,isPlace=false},
new Guest{id=28,prenom="Sébastien",nomFamille ="Demol",age=38,idCouple=27,isPlace=false},
new Guest{id=29,prenom="Céline",nomFamille ="Février",age=28,idCouple=30,isPlace=false},
new Guest{id=30,prenom="Michael",nomFamille ="Thouvenin",age=31,idCouple=29,isPlace=false},
new Guest{id=31,prenom="Dominique",nomFamille ="Fischer",age=50,idCouple=32,isPlace=false},
new Guest{id=32,prenom="Hervé",nomFamille ="Fischer",age=50,idCouple=31,isPlace=false},
new Guest{id=33,prenom="Aymeric",nomFamille ="France",age=45,idCouple=34,isPlace=false},
new Guest{id=34,prenom="Céline",nomFamille ="Olszewski",age=42,idCouple=33,isPlace=false},
new Guest{id=35,prenom="Grégory",nomFamille ="Frossard",age=31,idCouple=36,isPlace=false},
new Guest{id=36,prenom="Than",nomFamille ="Frossard",age=31,idCouple=35,isPlace=false},
new Guest{id=37,prenom="Olivier",nomFamille ="Golinski",age=35,idCouple=38,isPlace=false},
new Guest{id=38,prenom="Vanessa",nomFamille ="Golinski",age=32,idCouple=37,isPlace=false},
new Guest{id=39,prenom="Charlotte",nomFamille ="Hubert",age=32,idCouple=40,isPlace=false},
new Guest{id=40,prenom="Jérôme",nomFamille ="Hubert",age=32,idCouple=39,isPlace=false},
new Guest{id=41,prenom="Esma",nomFamille ="Kazic",age=60,idCouple=42,isPlace=false},
new Guest{id=42,prenom="Samo",nomFamille ="Kazic",age=60,idCouple=41,isPlace=false},
new Guest{id=43,prenom="Goran",nomFamille ="Kazic",age=50,idCouple=44,isPlace=false},
new Guest{id=44,prenom="Isabelle",nomFamille ="Kazic",age=50,idCouple=43,isPlace=false},
new Guest{id=45,prenom="Jasim",nomFamille ="Kazic",age=42,idCouple=46,isPlace=false},
new Guest{id=46,prenom="Nurija",nomFamille ="Kazic",age=42,idCouple=45,isPlace=false},
new Guest{id=47,prenom="Kevin",nomFamille ="Kazic",age=32,idCouple=48,isPlace=false},
new Guest{id=48,prenom="Anne-Sophie",nomFamille ="Leneveu",age=32,idCouple=47,isPlace=false},
new Guest{id=49,prenom="Rukija",nomFamille ="Kazic",age=45,idCouple=50,isPlace=false},
new Guest{id=50,prenom="Sejad",nomFamille ="Kazic",age=60,idCouple=49,isPlace=false},
new Guest{id=51,prenom="Said",nomFamille ="Lalliti",age=50,idCouple=52,isPlace=false},
new Guest{id=52,prenom="Véronique",nomFamille ="Lalliti",age=49,idCouple=51,isPlace=false},
new Guest{id=53,prenom="François",nomFamille ="Legueux",age=33,idCouple=54,isPlace=false},
new Guest{id=54,prenom="Malika",nomFamille ="Legueux",age=33,idCouple=53,isPlace=false},
new Guest{id=55,prenom="Françoise",nomFamille ="Leneveu",age=63,idCouple=56,isPlace=false},
new Guest{id=56,prenom="Gérard",nomFamille ="Leneveu",age=66,idCouple=55,isPlace=false},
new Guest{id=57,prenom="Bernard",nomFamille ="Menguy",age=75,idCouple=58,isPlace=false},
new Guest{id=58,prenom="Jaqueline",nomFamille ="Menguy",age=75,idCouple=57,isPlace=false},
new Guest{id=59,prenom="Franck",nomFamille ="Mentzer",age=75,idCouple=60,isPlace=false},
new Guest{id=60,prenom="Maryse",nomFamille ="Mentzer",age=75,idCouple=59,isPlace=false},
new Guest{id=61,prenom="Angélique",nomFamille ="Pique",age=33,idCouple=62,isPlace=false},
new Guest{id=62,prenom="Mathieu",nomFamille ="Ponzoni",age=33,idCouple=61,isPlace=false},
new Guest{id=63,prenom="Émilie",nomFamille ="Ponzoni",age=33,idCouple=64,isPlace=false},
new Guest{id=64,prenom="Thomas",nomFamille ="Ponzoni",age=33,idCouple=63,isPlace=false},
new Guest{id=65,prenom="Eric",nomFamille ="Ragueneau",age=50,idCouple=66,isPlace=false},
new Guest{id=66,prenom="Mélanie",nomFamille ="Ragueneau",age=50,idCouple=65,isPlace=false},
new Guest{id=67,prenom="Bernard",nomFamille ="Segreto",age=65,idCouple=68,isPlace=false},
new Guest{id=68,prenom="Michèle",nomFamille ="Segreto",age=65,idCouple=67,isPlace=false},
new Guest{id=69,prenom="Arezki",nomFamille ="Slimi",age=70,idCouple=70,isPlace=false},
new Guest{id=70,prenom="Jeanine",nomFamille ="Slimi",age=60,idCouple=69,isPlace=false},
new Guest{id=71,prenom="Michel",nomFamille ="Tuphin",age=75,idCouple=72,isPlace=false},
new Guest{id=72,prenom="Sylviane",nomFamille ="Tuphin",age=75,idCouple=71,isPlace=false},
new Guest{id=73,prenom="Marie-Thérèse",nomFamille ="Vannier",age=70,idCouple=74,isPlace=false},
new Guest{id=74,prenom="Serge",nomFamille ="Vannier",age=70,idCouple=73,isPlace=false},
new Guest{id=75,prenom="Arnaud",nomFamille ="Vautier",age=42,idCouple=76,isPlace=false},
new Guest{id=76,prenom="Christelle",nomFamille ="Vautier",age=42,idCouple=75,isPlace=false},
new Guest{id=77,prenom="Martine",nomFamille ="Olivrin",age=60,idCouple=78,isPlace=false},
new Guest{id=78,prenom="Yvon",nomFamille ="Olivrin",age=60,idCouple=77,isPlace=false},
new Guest{id=79,prenom="Armony",nomFamille ="Aubry",age=16,idCouple=0,isPlace=false},
new Guest{id=80,prenom="Anaelle",nomFamille ="Bidault",age=16,idCouple=0,isPlace=false},
new Guest{id=81,prenom="Corentin",nomFamille ="Bidault",age=19,idCouple=0,isPlace=false},
new Guest{id=82,prenom="Martial",nomFamille ="Blanchard",age=90,idCouple=0,isPlace=false},
new Guest{id=83,prenom="Annick",nomFamille ="Bozier",age=62,idCouple=0,isPlace=false},
new Guest{id=84,prenom="Océane",nomFamille ="Canart",age=5,idCouple=0,isPlace=false},
new Guest{id=85,prenom="Emilie",nomFamille ="Chollat",age=32,idCouple=0,isPlace=false},
new Guest{id=86,prenom="Dominique",nomFamille ="Courtot",age=60,idCouple=0,isPlace=false},
new Guest{id=87,prenom="Alain",nomFamille ="Demol",age=61,idCouple=0,isPlace=false},
new Guest{id=88,prenom="Josée",nomFamille ="Demol",age=62,idCouple=0,isPlace=false},
new Guest{id=89,prenom="",nomFamille ="Dj 1",age=45,idCouple=90,isPlace=false},
new Guest{id=90,prenom="",nomFamille ="Dj2",age=40,idCouple=89,isPlace=false},
new Guest{id=91,prenom="Anais",nomFamille ="Fischer",age=26,idCouple=0,isPlace=false},
new Guest{id=92,prenom="Tiphaine",nomFamille ="Fischer",age=19,idCouple=0,isPlace=false},
new Guest{id=93,prenom="Julien",nomFamille ="Gries",age=32,idCouple=0,isPlace=false},
new Guest{id=94,prenom="Zineb",nomFamille ="Haddadi",age=38,idCouple=0,isPlace=false},
new Guest{id=95,prenom="Maxime",nomFamille ="Hirtz",age=32,idCouple=0,isPlace=false},
new Guest{id=96,prenom="Vincent",nomFamille ="Hornsperger",age=32,idCouple=0,isPlace=false},
new Guest{id=97,prenom="Christophe",nomFamille ="Jucker",age=36,idCouple=0,isPlace=false},
new Guest{id=98,prenom="Anna",nomFamille ="Kazic",age=85,idCouple=0,isPlace=false},
new Guest{id=99,prenom="Florian",nomFamille ="Kazic",age=19,idCouple=0,isPlace=false},
new Guest{id=100,prenom="Lucas",nomFamille ="Kazic",age=13,idCouple=0,isPlace=false},
new Guest{id=101,prenom="Mujo",nomFamille ="Kazic",age=45,idCouple=0,isPlace=false},
new Guest{id=102,prenom="Sandra",nomFamille ="Kazic",age=38,idCouple=0,isPlace=false},
new Guest{id=103,prenom="Leila",nomFamille ="Lalliti",age=22,idCouple=0,isPlace=false},
new Guest{id=104,prenom="Soreya",nomFamille ="Lalliti",age=18,idCouple=0,isPlace=false},
new Guest{id=105,prenom="Christiane",nomFamille ="Lambert",age=80,idCouple=0,isPlace=false},
new Guest{id=106,prenom="Benjamin",nomFamille ="Legueux",age=30,idCouple=0,isPlace=false},
new Guest{id=107,prenom="Vanessa",nomFamille ="Mellon",age=32,idCouple=0,isPlace=false},
new Guest{id=108,prenom="Pascal",nomFamille ="Olery",age=32,idCouple=0,isPlace=false},
new Guest{id=109,prenom="Alexandre",nomFamille ="Piel",age=32,idCouple=0,isPlace=false},
new Guest{id=110,prenom="Daniel",nomFamille ="Rusman",age=34,idCouple=0,isPlace=false},
new Guest{id=111,prenom="Stéphane",nomFamille ="Saquet",age=32,idCouple=0,isPlace=false},
new Guest{id=112,prenom="Hedi",nomFamille ="Sellami",age=32,idCouple=0,isPlace=false},
new Guest{id=113,prenom="Alexis",nomFamille ="Slimi",age=23,idCouple=0,isPlace=false},
new Guest{id=114,prenom="Fatima",nomFamille ="Slimi",age=38,idCouple=0,isPlace=false},
new Guest{id=115,prenom="Cyril",nomFamille ="Vermandé",age=32,idCouple=0,isPlace=false}

            };

            Console.Out.WriteLine("List des hôtes :");
            Console.Out.WriteLine("|--Prenom--|--Nom--|--Age--|--Coupe--|");
            foreach (Guest h in listHote)
            {
                Console.Out.WriteLine(h.prenom + " | " + h.nomFamille + " | " + h.age + " | " + h.idCouple + " |");
            }
            //Console.Out.WriteLine("List des groupe : ");
            //Console.Out.WriteLine("|--Nom--|--Hote--|");
            //foreach (Groupe g in listGroupe)
            //{
            //    foreach (Hote h in g.list)
            //    {
            //        Console.Out.WriteLine(g.nom + " | " + h.nomFamille + " " + h.prenom);
            //    }
            //}
            //Table particulier = new Table { id = 1, isParticulier = true, list = new List<Hote>(), nom = "Honneur" };
            //particulier.list.Add(listHote.First(h => h.id == 5));
            //listHoteAPlacer.Remove(listHoteAPlacer.First(h => h.id == 5));
            //particulier.list.Add(listHote.First(h => h.id == 6));
            //listHoteAPlacer.Remove(listHoteAPlacer.First(h => h.id == 6));
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
            //listTable.Add(particulier);
            Solve3(listHote, listTable);
            //listTable = Solve(listHote, listTable);
            Console.Out.WriteLine("|--------------Table-------------------|");
            foreach (Table t in listTable)
            {
                Console.Out.WriteLine(t.nom);
                Console.Out.WriteLine("|--Hôte--|");
                foreach (Guest h in t.list)
                {
                    Console.Out.WriteLine(h.nomFamille + " " + h.prenom + "|" + h.age);
                }
            }
        }
    }
}
