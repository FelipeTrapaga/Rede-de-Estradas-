using System;
namespace Rede_Estradas
{
    public class Evento
    {
        public string Descricao { get; set; }
        public int ImpactoTempo { get; set; } // Representa o impacto no tempo em minutos

        public Evento(string descricao, int impactoTempo)
        {
            Descricao = descricao;
            ImpactoTempo = impactoTempo;
        }
    }
}
