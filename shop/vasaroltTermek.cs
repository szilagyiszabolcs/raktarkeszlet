using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop
{
    public class vasaroltTermek
    {
        public string Vonalkod { get; set; }
        public string Nev { get; set; }
        public int Mennyiseg { get; set; }
        public double Ar { get; set; }

        public vasaroltTermek(Termek t, int m)
        {
            Vonalkod = t.Vonalkod;
            Nev = t.Megnevezes;
            Mennyiseg = m;
            Ar = t.Egysegar * Mennyiseg;
        }
    }
}
