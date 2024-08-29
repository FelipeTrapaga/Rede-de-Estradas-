using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    using System;
    using System.Collections.Generic;

    public class RedeTransporte
    {
        private Dictionary<Cidade, List<Rota>> mapaRotas;

        public RedeTransporte()
        {
            mapaRotas = new Dictionary<Cidade, List<Rota>>();
        }

        public void AdicionarCidade(Cidade cidade)
        {
            if (!mapaRotas.ContainsKey(cidade))
            {
                mapaRotas[cidade] = new List<Rota>();
            }
            else
            {
                Console.WriteLine("Cidade já existe na rede.");
            }
        }

        public void AdicionarRota(Cidade origem, Cidade destino, int distancia)
        {
            if (mapaRotas.ContainsKey(origem) && mapaRotas.ContainsKey(destino))
            {
                var rota = new Rota(origem, destino, distancia);
                mapaRotas[origem].Add(rota);
            }
            else
            {
                Console.WriteLine("Uma ou ambas as cidades não foram encontradas na rede.");
            }
        }

        public bool EhDigrafo()
        {
            foreach (var cidade in mapaRotas)
            {
                foreach (var rota in cidade.Value)
                {
                    var rotaInversa = mapaRotas.ContainsKey(rota.Destino) &&
                                      mapaRotas[rota.Destino].Exists(r => r.Destino == rota.Origem);

                    if (!rotaInversa)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void MostrarRede()
        {
            Console.WriteLine("Mapa da Rede de Transporte:");
            foreach (var cidade in mapaRotas)
            {
                Console.WriteLine($"Cidade: {cidade.Key.Nome}");
                foreach (var rota in cidade.Value)
                {
                    Console.WriteLine($"  Rota para {rota.Destino.Nome} - Distância: {rota.Distancia} km");
                }
            }
        }
    }
}
