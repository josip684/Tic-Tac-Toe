using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Akka.Actor;

namespace RS_Projekt_Krizic_Kruzic_JG
{
    public partial class Form1 : Form
    {
        IActorRef postar;
        public Form1()
        {
            InitializeComponent();
            List<Button> lista_botuna = new List<Button>() { A00, A01, A02, A10, A11, A12, A20, A21, A22, };

            Props prop = Props.Create(() => new FormActor(lista_botuna)).WithDispatcher("akka.actor.synchronized-dispatcher");
            postar = Program.sustav.ActorOf(prop);
        }

        public int igrac = 2;
        public int potezi = 0;
                             
        public int pobjeda_X = 0;
        public int pobjeda_O = 0;
        public int nerijeseno = 0;

        private void Form1_Load(object sender, EventArgs e)
        {
            Bodovi_X.Text = "X: " + pobjeda_X;
            Bodovi_O.Text = "O: " + pobjeda_O;
            Neodlucene.Text = "Draw: " + nerijeseno;
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int id_botuna = 0;
            
            if (button.Name == "A00") { id_botuna = 0; }
            if (button.Name == "A01") { id_botuna = 1; }
            if (button.Name == "A02") { id_botuna = 2; }
            if (button.Name == "A10") { id_botuna = 3; }
            if (button.Name == "A11") { id_botuna = 4; }
            if (button.Name == "A12") { id_botuna = 5; }
            if (button.Name == "A20") { id_botuna = 6; }
            if (button.Name == "A21") { id_botuna = 7; }
            if (button.Name == "A22") { id_botuna = 8; }

            if (button.Text == "")
            {
                if (igrac % 2 == 0)
                {
                    button.Text = "X";
                    postar.Tell(new Prva("X", id_botuna));
                    igrac++;
                    potezi++;
                }
                else
                {
                    button.Text = "O";
                    postar.Tell(new Prva("O", id_botuna));
                    igrac++;
                    potezi++;
                }

                if (ProvjeriIgru() == true)
                {
                    MessageBox.Show("Neriješeno!");
                    nerijeseno++;
                    NovaIgra();
                }

                if (ProvjeriPobjednika() == true)
                {
                    if (button.Text == "X")
                    {
                        MessageBox.Show("X je pobjednik!");
                        pobjeda_X++;
                        NovaIgra();
                    }
                    else
                    {
                        MessageBox.Show("O je pobjednik!");
                        pobjeda_O++;
                        NovaIgra();
                    }
                }

            }
        }
        void NovaIgra()
        {
            igrac = 2;
            potezi = 0;
            A00.Text = A01.Text = A02.Text = A10.Text = A11.Text = A12.Text = A20.Text = A21.Text = A22.Text = "";
            Bodovi_X.Text = "X: " + pobjeda_X;
            Bodovi_O.Text = "O: " + pobjeda_O;
            Neodlucene.Text = "Neodluceno: " + nerijeseno;
        }

        bool ProvjeriIgru()
        {
            if ((potezi == 9) && ProvjeriPobjednika() == false)
                return true;
            else
                return false;
        }

        bool ProvjeriPobjednika()
        {
            //horizontalna provjera
            if ((A00.Text == A01.Text) && (A01.Text == A02.Text) && A00.Text != "")
                return true;
            else if ((A10.Text == A11.Text) && (A11.Text == A12.Text) && A10.Text != "")
                return true;
            else if ((A20.Text == A21.Text) && (A21.Text == A22.Text) && A20.Text != "")
                return true;

            //vertikalna provjera
            if ((A00.Text == A10.Text) && (A10.Text == A20.Text) && A00.Text != "")
                return true;
            else if ((A01.Text == A11.Text) && (A11.Text == A21.Text) && A01.Text != "")
                return true;
            else if ((A02.Text == A12.Text) && (A12.Text == A22.Text) && A02.Text != "")
                return true;

            //provjera dijagonala
            if ((A00.Text == A11.Text) && (A11.Text == A22.Text) && A00.Text != "")
                return true;
            else if ((A02.Text == A11.Text) && (A11.Text == A20.Text) && A02.Text != "")
                return true;
            else
                return false;
        }

        private void btn_Nova_Igra_Click(object sender, EventArgs e)
        {
            NovaIgra();
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            pobjeda_X = pobjeda_O = nerijeseno = 0;
            NovaIgra();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
