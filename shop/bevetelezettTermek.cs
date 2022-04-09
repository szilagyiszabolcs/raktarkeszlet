using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop
{
    public class bevetelezettTermek
    {
        public string Vonalkod { get; set; }
        public string Nev { get; set; }
        public int Mennyiseg { get; set; }

        public bevetelezettTermek(Termek t, int m)
        {
            Vonalkod = t.Vonalkod;
            Nev = t.Megnevezes;
            Mennyiseg = m;
        }
    }
}
