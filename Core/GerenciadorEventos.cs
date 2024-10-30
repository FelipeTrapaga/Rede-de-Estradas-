using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class GerenciadorEventos
    {
        private List<Evento> eventos;

        public GerenciadorEventos()
        {
            eventos = new List<Evento>();
        }

        public void AdicionarEvento(Evento evento)
        {
            eventos.Add(evento);
        }

        public int AplicarImpactoAleatorio()
        {
            if (eventos.Count == 0) return 0;

            Random random = new Random();
            Evento eventoAleatorio = eventos[random.Next(eventos.Count)];
            Console.WriteLine($"Evento aplicado: {eventoAleatorio.Descricao} com impacto de {eventoAleatorio.ImpactoTempo} minutos");
            return eventoAleatorio.ImpactoTempo;
        }
    }
}
