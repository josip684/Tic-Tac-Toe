using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using System.Windows.Forms;

namespace RS_Projekt_Krizic_Kruzic_JG
{
    class FormActor : ReceiveActor
    {
        private List<Button> Lista_Botuna;

        public FormActor(List<Button> lista_botuna)
        {
            Lista_Botuna = lista_botuna;
            

            Receive<Prva>(x =>
            {
                Props props = Props.Create(() => new Worker());
                IActorRef dijete = Context.ActorOf(props);
                dijete.Tell(new Prva(x.P_Text, x.Indeks));    
            });

            Receive<Druga>(x =>
            {
                Lista_Botuna[x.Indeks].Text = x.D_Text;
            });
        }
    }

    class Worker : ReceiveActor
    {
        public Worker()
        {
            Receive<Prva>(x => Provjeri(x));
        }

        public void Provjeri(Prva x)
        {
            
            if (x.P_Text == "X" )
            {
                Sender.Tell(new Druga("X", x.Indeks));
            }
            if (x.P_Text == "O" )
            {
                Sender.Tell(new Druga("O", x.Indeks));
            }
          
        }

    }
}
