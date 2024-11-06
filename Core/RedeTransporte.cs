using System;
using System.Collections.Generic;
using System.Linq;

namespace Rede_Estradas
{
    public class RedeTransporte
    {
        private Dictionary<Cidade, List<Rota>> mapaRotas;
        private readonly int maxCidades;
        private readonly int maxRotas;
        private int rotasCriadas;
        private GerenciadorEventos gerenciadorEventos;

        public RedeTransporte(int maxCidades, int maxRotas)
        {
            this.maxCidades = maxCidades;
            this.maxRotas = maxRotas;
            mapaRotas = new Dictionary<Cidade, List<Rota>>();
            rotasCriadas = 0;
            gerenciadorEventos = new GerenciadorEventos();
        }

        public void AdicionarCidade(Cidade cidade)
        {
            if (mapaRotas.Count < maxCidades && !mapaRotas.ContainsKey(cidade))
            {
                mapaRotas[cidade] = new List<Rota>();
                Console.WriteLine($"Cidade {cidade.Nome} adicionada com sucesso!");
            }
            else
            {
                Console.WriteLine($"Não foi possível adicionar a cidade {cidade.Nome}.");
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

        public void AdicionarRota(Cidade origem, Cidade destino, int distancia, string tipoTransporte, int tempoViagem)
        {
            if (rotasCriadas < maxRotas && mapaRotas.ContainsKey(origem) && mapaRotas.ContainsKey(destino))
            {
                var rota = new Rota(origem, destino, distancia, tipoTransporte, tempoViagem);
                //rota.TempoViagem += gerenciadorEventos.AplicarImpactoAleatorio(); // Aplicando impacto do evento
                mapaRotas[origem].Add(rota);
                rotasCriadas++;
                Console.WriteLine($"Rota adicionada: {origem.Nome} -> {destino.Nome}, Distância: {distancia} km, Transporte: {tipoTransporte}, " +
                    $"Tempo de Viagem: {tempoViagem} min");
            }
            else
            {
                Console.WriteLine("Não foi possível adicionar a rota.");
            }
        }

        public void RemoverCidade(string nomeCidade)
        {
            var cidadeRemover = mapaRotas.Keys.FirstOrDefault(c => c.Nome.Equals(nomeCidade, StringComparison.OrdinalIgnoreCase));
            if (cidadeRemover != null)
            {
                mapaRotas.Remove(cidadeRemover);
                Console.WriteLine($"Cidade {cidadeRemover.Nome} removida com sucesso!");

                // Remover rotas que têm a cidade removida como origem ou destino
                foreach (var rotaLista in mapaRotas.Values)
                {
                    rotaLista.RemoveAll(r => r.Destino.Equals(cidadeRemover));
                }
            }
            else
            {
                Console.WriteLine($"Cidade {nomeCidade} não encontrada.");
            }
        }

        public void RemoverRota(string origemNome, string destinoNome)
        {
            var cidadeOrigem = mapaRotas.Keys.FirstOrDefault(c => c.Nome.Equals(origemNome, StringComparison.OrdinalIgnoreCase));
            var cidadeDestino = mapaRotas.Keys.FirstOrDefault(c => c.Nome.Equals(destinoNome, StringComparison.OrdinalIgnoreCase));

            if (cidadeOrigem != null && cidadeDestino != null)
            {
                var rota = mapaRotas[cidadeOrigem].FirstOrDefault(r => r.Destino.Equals(cidadeDestino));
                if (rota != null)
                {
                    mapaRotas[cidadeOrigem].Remove(rota);
                    rotasCriadas--;
                    Console.WriteLine($"Rota removida: {origemNome} -> {destinoNome}");
                }
                else
                {
                    Console.WriteLine($"Rota de {origemNome} para {destinoNome} não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("Uma ou ambas as cidades não foram encontradas.");
            }
        }

        public void AtualizarCidade(string nomeAntigo, string nomeNovo)
        {
            var cidade = mapaRotas.Keys.FirstOrDefault(c => c.Nome.Equals(nomeAntigo, StringComparison.OrdinalIgnoreCase));
            if (cidade != null)
            {
                mapaRotas.Remove(cidade);
                var novaCidade = new Cidade(nomeNovo);
                mapaRotas[novaCidade] = mapaRotas[cidade];
                Console.WriteLine($"Cidade {nomeAntigo} atualizada para {nomeNovo}.");
            }
            else
            {
                Console.WriteLine($"Cidade {nomeAntigo} não encontrada.");
            }
        }

        public void AtualizarRota(string origemNome, string destinoNome, int novaDistancia)
        {
            var cidadeOrigem = mapaRotas.Keys.FirstOrDefault(c => c.Nome.Equals(origemNome, StringComparison.OrdinalIgnoreCase));
            var cidadeDestino = mapaRotas.Keys.FirstOrDefault(c => c.Nome.Equals(destinoNome, StringComparison.OrdinalIgnoreCase));

            if (cidadeOrigem != null && cidadeDestino != null)
            {
                var rota = mapaRotas[cidadeOrigem].FirstOrDefault(r => r.Destino.Equals(cidadeDestino));
                if (rota != null)
                {
                    rota.Distancia = novaDistancia;
                    Console.WriteLine($"Rota de {origemNome} para {destinoNome} atualizada com nova distância: {novaDistancia} km.");
                }
                else
                {
                    Console.WriteLine($"Rota de {origemNome} para {destinoNome} não encontrada.");
                }
            }
            else
            {
                Console.WriteLine("Uma ou ambas as cidades não foram encontradas.");
            }
        }

        // Método principal para busca em profundidade com destino
        public void BuscaProfundidade(Cidade origem, Cidade destino)
        {
            var visitados = new HashSet<Cidade>();
            if (DFS(origem, destino, visitados))
            {
                Console.WriteLine($"Cidade destino {destino.Nome} encontrada!");
            }
            else
            {
                Console.WriteLine($"Cidade destino {destino.Nome} não encontrada a partir de {origem.Nome}.");
            }
        }

        // Método recursivo DFS com verificação de destino
        private bool DFS(Cidade cidade, Cidade destino, HashSet<Cidade> visitados)
        {
            if (visitados.Contains(cidade))
                return false;

            visitados.Add(cidade);
            Console.WriteLine($"Visitando cidade: {cidade.Nome}");

            // Verifica se a cidade atual é o destino
            if (cidade.Equals(destino))
                return true;

            // Chama DFS para cada cidade adjacente
            foreach (var rota in mapaRotas[cidade])
            {
                if (DFS(rota.Destino, destino, visitados))
                    return true; // Retorna true se o destino foi encontrado
            }

            // Retorna false se não achar o caminho até o destino
            return false; 
        }


        // Método de busca em largura (BFS)
        public void BuscaLargura(Cidade origem, Cidade destino)
        {
            var visitados = new HashSet<Cidade>();
            var fila = new Queue<Cidade>();
            fila.Enqueue(origem);

            while (fila.Count > 0)
            {
                var cidadeAtual = fila.Dequeue();
                if (!visitados.Contains(cidadeAtual))
                {
                    visitados.Add(cidadeAtual);
                    Console.WriteLine($"Visitando cidade: {cidadeAtual.Nome}");

                    // Verifica se a cidade atual é o destino
                    if (cidadeAtual.Equals(destino))
                    {
                        Console.WriteLine($"Cidade destino {destino.Nome} encontrada!");
                        return;
                    }

                    // Adiciona as cidades adjacentes na fila, se ainda não visitadas
                    foreach (var rota in mapaRotas[cidadeAtual])
                    {
                        if (!visitados.Contains(rota.Destino))
                        {
                            fila.Enqueue(rota.Destino);
                        }
                    }
                }
            }

            Console.WriteLine($"Cidade destino {destino.Nome} não encontrada a partir de {origem.Nome}.");
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