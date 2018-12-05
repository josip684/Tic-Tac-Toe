using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Projekt_Krizic_Kruzic_JG
{
    public class Prva
    {
        public string P_Text { get; private set; }
        public int Indeks;
        public Prva(string p_text, int indeks)
        {
            P_Text = p_text;
            Indeks = indeks;
        }
    }

    public class Druga
    {
        public string D_Text { get; private set; }
        public int Indeks;
        public Druga(string d_text, int indeks)
        {
            D_Text = d_text;
            Indeks = indeks;
        }
    }
}
