//IMPORTS SYSTEM ========== 
using System;
using System.Collections.Generic;

namespace Rede_Estradas
{

    public class RedeTransporte
    {
        private Dictionary<Cidade, List<Rota>> mapaRotas;
        private readonly int maxCidades;
        private readonly int maxRotas;
        private int rotasCriadas;

        public RedeTransporte(int maxCidades, int maxRotas)
        {
            this.maxCidades = maxCidades;
            this.maxRotas = maxRotas;
            mapaRotas = new Dictionary<Cidade, List<Rota>>();
            rotasCriadas = 0;
        }

        public void AdicionarCidade(Cidade cidade)
        {
            if (mapaRotas.Count < maxCidades)
            {
                if (!mapaRotas.ContainsKey(cidade))
                {
                    mapaRotas[cidade] = new List<Rota>();
                    Console.WriteLine($"Cidade {cidade.Nome} adicionada com sucesso!");
                }
                else
                {
                    Console.WriteLine("Cidade já existe na rede.");
                }
            }
            else
            {
                Console.WriteLine("Número máximo de cidades atingido.");
            }
        }

        public void AdicionarRota(Cidade origem, Cidade destino, int distancia)
        {
            if (rotasCriadas < maxRotas)
            {
                if (mapaRotas.ContainsKey(origem) && mapaRotas.ContainsKey(destino))
                {
                    var rota = new Rota(origem, destino, distancia);
                    mapaRotas[origem].Add(rota);
                    rotasCriadas++;
                    Console.WriteLine($"Rota adicionada: {origem.Nome} -> {destino.Nome}, Distância: {distancia} km");
                }
                else
                {
                    Console.WriteLine("Uma ou ambas as cidades não foram encontradas na rede.");
                }
            }
            else
            {
                Console.WriteLine("Número máximo de rotas atingido.");
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

                if (cidade.Value.Count > 0)
                {
                    Console.WriteLine("  Rotas:");
                    foreach (var rota in cidade.Value)
                    {
                        Console.WriteLine($"    Destino: {rota.Destino.Nome} - Distância: {rota.Distancia} km");
                    }
                }
            }
        }
    }
}