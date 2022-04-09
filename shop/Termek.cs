using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shop
{
    public class Termek
    {
        public string Vonalkod { get; set; }
        public string Megnevezes { get; set; }
        public int Raktarkeszlet { get; set; }
        public double Egysegar { get; set; }

        public Termek(string s)
        {
            string[] sor = s.Split(';');

            Vonalkod = sor[0];
            Megnevezes = sor[1];
            Raktarkeszlet = int.Parse(sor[2]);
            Egysegar = double.Parse(sor[3].Replace(".",","));
        }

        public Termek(string v, string m, int r, double e)
        {
            Vonalkod = v;
            Megnevezes = m;
            Raktarkeszlet = r;
            Egysegar = e;
        }
    }
}
