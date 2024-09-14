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

        public Cidade ConsultarCidade(string nomeCidade)
        {
            return mapaRotas.Keys.FirstOrDefault(cidade => cidade.Nome.Equals(nomeCidade, StringComparison.OrdinalIgnoreCase));
        }

        public Rota ConsultarRota(Cidade origem, Cidade destino)
        {
            if (mapaRotas.ContainsKey(origem))
            {
                return mapaRotas[origem].FirstOrDefault(rota => rota.Destino.Equals(destino));
            }
            return null;
        }

        public List<Cidade> EncontrarCaminhoMaisCurto(Cidade origem, Cidade destino)
        {
            var distancias = new Dictionary<Cidade, int>();
            var anteriores = new Dictionary<Cidade, Cidade>();
            var naoVisitados = new List<Cidade>();

            // Inicializar as distâncias e adicionar todas as cidades à lista de não visitados
            foreach (var cidade in mapaRotas.Keys)
            {
                distancias[cidade] = int.MaxValue; // Começar com distâncias infinitas
                anteriores[cidade] = null; // Nenhum caminho anterior inicialmente
                naoVisitados.Add(cidade);
            }

            // A distância para a cidade de origem é 0
            distancias[origem] = 0;

            while (naoVisitados.Count > 0)
            {
                // Encontrar a cidade com a menor distância ainda não visitada
                Cidade cidadeAtual = naoVisitados.OrderBy(cidade => distancias[cidade]).First();

                // Se chegamos ao destino, paramos
                if (cidadeAtual == destino)
                {
                    break;
                }

                naoVisitados.Remove(cidadeAtual);

                // Atualizar as distâncias para os vizinhos da cidade atual
                foreach (var rota in mapaRotas[cidadeAtual])
                {
                    Cidade vizinho = rota.Destino;
                    if (naoVisitados.Contains(vizinho))
                    {
                        int distanciaPossivel = distancias[cidadeAtual] + rota.Distancia;
                        if (distanciaPossivel < distancias[vizinho])
                        {
                            distancias[vizinho] = distanciaPossivel;
                            anteriores[vizinho] = cidadeAtual; // Armazena o caminho anterior
                        }
                    }
                }
            }

            // Reconstruir o caminho da cidade destino até a origem
            var caminho = new List<Cidade>();
            Cidade atual = destino;
            while (atual != null)
            {
                caminho.Insert(0, atual); // Insere no início da lista para reverter a ordem
                atual = anteriores[atual];
            }

            if (caminho.First() != origem)
            {
                Console.WriteLine("Nenhum caminho encontrado.");
                return null;
            }

            return caminho;
        }
    }
}