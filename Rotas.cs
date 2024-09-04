using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class Rota
    {
        public Cidade Origem { get; }
        public Cidade Destino { get; }
        public int Distancia { get; }

        public Rota(Cidade origem, Cidade destino, int distancia)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
        }
    }
}
