using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class Rota
    {
        public Cidade Origem { get; private set; }
        public Cidade Destino { get; private set; }
        public int Distancia { get; private set; }

        public Rota(Cidade origem, Cidade destino, int distancia)
        {
            Origem = origem;
            Destino = destino;
            Distancia = distancia;
        }
    }
}
