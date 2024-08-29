using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rede_Estradas
{
    public class Programa
    {
        private RedeTransporte rede;

        public Programa()
        {
            rede = new RedeTransporte();
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
                Console.WriteLine("4. Verificar se é um Dígrafo");
                Console.WriteLine("5. Sair");
                Console.Write("Escolha uma opção: ");
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Console.Write("Digite o nome da cidade: ");
                        string nomeCidade = Console.ReadLine();
                        var cidade = new Cidade(nomeCidade);
                        rede.AdicionarCidade(cidade);
                        Console.WriteLine("Cidade adicionada com sucesso!");
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
                        Console.WriteLine("Rota adicionada com sucesso!");
                        break;

                    case 3:
                        rede.MostrarRede();
                        break;

                    case 4:
                        bool ehDigrafo = rede.EhDigrafo();
                        Console.WriteLine(ehDigrafo ? "A rede é um dígrafo (rotas unidirecionais)." : "A rede é um grafo simples (rotas bidirecionais).");
                        break;

                    case 5:
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
