using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class Programa
    {
        private readonly RedeTransporte rede;

        public Programa()
        {
            Console.WriteLine("Digite o número máximo de cidades:");
            int maxCidades = int.Parse(Console.ReadLine());

            Console.WriteLine("Digite o número máximo de rotas:");
            int maxRotas = int.Parse(Console.ReadLine());

            rede = new RedeTransporte(maxCidades, maxRotas);
        }

        public void Executar()
        {
            bool sair = false;

            while (!sair)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Adicionar Cidade");
                Console.WriteLine("2. Adicionar Rota");
                Console.WriteLine("3. Mostrar Rede de Transporte");
                Console.WriteLine("4. Verificar se é um Dígrafo(Desativado)");
                Console.WriteLine("5. Encontrar o Caminho mais Curto");
                Console.WriteLine("6. Sair");
                Console.Write("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("Digite o nome da cidade: ");
                        string nomeCidade = Console.ReadLine();
                        while (string.IsNullOrEmpty(nomeCidade))
                        {
                            Console.Write("Digite o nome da cidade (não pode ser vazio): ");
                            nomeCidade = Console.ReadLine();
                        }
                        var cidade = new Cidade(nomeCidade);
                        rede.AdicionarCidade(cidade);

                        break;

                    case 2:
                        Console.Write("Digite o nome da cidade de origem: ");
                        string origem = Console.ReadLine();
                        Console.Write("Digite o nome da cidade de destino: ");
                        string destino = Console.ReadLine();
                        Console.Write("Digite a distância entre as cidades (em km): ");
                        int distancia = int.Parse(Console.ReadLine());

                        var cidadeOrigem = new Cidade(origem);
                        var cidadeDestino = new Cidade(destino);

                        rede.AdicionarRota(cidadeOrigem, cidadeDestino, distancia);
                        break;

                    case 3:
                        rede.MostrarRede();
                        break;

                    case 4:
                        
                        break;
                    
                    case 5:

                        Console.Write("Digite o nome da cidade de origem: ");
                        string nomeOrigem = Console.ReadLine();
                        Console.Write("Digite o nome da cidade de destino: ");
                        string nomeDestino = Console.ReadLine();

                        var cidadeOrigemBusca = rede.ConsultarCidade(nomeOrigem);
                        var cidadeDestinoBusca = rede.ConsultarCidade(nomeDestino);

                        if (cidadeOrigemBusca != null && cidadeDestinoBusca != null)
                        {
                            var caminho = rede.EncontrarCaminhoMaisCurto(cidadeOrigemBusca, cidadeDestinoBusca);
                            if (caminho != null)
                            {
                                Console.WriteLine("Caminho mais curto encontrado:");
                                foreach (var cidade1 in caminho)
                                {
                                    Console.WriteLine(cidade1.Nome);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Cidades não encontradas.");
                        }
                        break;

                    case 6:
                        sair = true;
                        Console.WriteLine("Saindo...");
                        break;

                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }
    }
}
