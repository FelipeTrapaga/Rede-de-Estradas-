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
                Console.WriteLine("4. Verificar se é um Dígrafo (Desativado)");
                Console.WriteLine("5. Encontrar o Caminho mais Curto");
                Console.WriteLine("6. Remover Cidade");
                Console.WriteLine("7. Remover Rota");
                Console.WriteLine("8. Atualizar Cidade");
                Console.WriteLine("9. Atualizar Rota");
                Console.WriteLine("10. Listar dados do grafo");
                Console.WriteLine("11. Sair");
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
                        Cidade cidade = new Cidade(nomeCidade);
                        rede.AdicionarCidade(cidade);
                        break;

                    case 2:
                        Console.Write("Digite o nome da cidade de origem: ");
                        string origemCidade = Console.ReadLine();
                        Console.Write("Digite o nome da cidade de destino: ");
                        string destino = Console.ReadLine();
                        Console.Write("Digite a distância entre as cidades (em km): ");
                        int distancia = int.Parse(Console.ReadLine());

                        var cidadeOrigem = new Cidade(origemCidade);
                        var cidadeDestino = new Cidade(destino);

                        rede.AdicionarRota(cidadeOrigem, cidadeDestino, distancia);
                        break;

                    case 3:
                        rede.MostrarRede();
                        break;

                    case 4:
                        Console.WriteLine("Função de verificar Dígrafo ainda desativada.");
                        break;

                    case 5:
                        Console.WriteLine("Digite a cidade de origem:");
                        string origemInicio = Console.ReadLine();

                        Console.WriteLine("Digite a cidade de destino:");
                        string destinoFinal = Console.ReadLine();

                        Cidade origemCidade1 = rede.ConsultarCidade(origemInicio);
                        Cidade destinoCidade = rede.ConsultarCidade(destinoFinal);

                        if (origemCidade1 != null && destinoCidade != null)
                        {
                            List<Cidade> caminho = rede.EncontrarCaminhoMaisCurto(origemCidade1, destinoCidade);

                            if (caminho != null && caminho.Count > 0)
                            {
                                Console.WriteLine("Caminho mais curto encontrado:");
                                foreach (var cidadeFirst in caminho)
                                {
                                    Console.WriteLine(cidadeFirst.Nome);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Nenhum caminho encontrado entre as cidades.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Uma ou ambas as cidades não foram encontradas.");
                        }
                        break;

                    case 6:
                        Console.Write("Digite o nome da cidade a remover: ");
                        string cidadeRemover = Console.ReadLine();
                        rede.RemoverCidade(cidadeRemover);
                        break;

                    case 7:
                        Console.Write("Digite o nome da cidade de origem: ");
                        string origemRemover = Console.ReadLine();
                        Console.Write("Digite o nome da cidade de destino: ");
                        string destinoRemover = Console.ReadLine();
                        rede.RemoverRota(origemRemover, destinoRemover);
                        break;

                    case 8:
                        Console.Write("Digite o nome da cidade que deseja atualizar: ");
                        string cidadeAntiga = Console.ReadLine();
                        Console.Write("Digite o novo nome da cidade: ");
                        string cidadeNova = Console.ReadLine();
                        rede.AtualizarCidade(cidadeAntiga, cidadeNova);
                        break;

                    case 9:
                        Console.Write("Digite o nome da cidade de origem: ");
                        string origemAtualizar = Console.ReadLine();
                        Console.Write("Digite o nome da cidade de destino: ");
                        string destinoAtualizar = Console.ReadLine();
                        Console.Write("Digite a nova distância entre as cidades (em km): ");
                        int novaDistancia = int.Parse(Console.ReadLine());
                        rede.AtualizarRota(origemAtualizar, destinoAtualizar, novaDistancia);
                        break;

                    case 10:
                        rede.ListarDadosGrafo();
                        break;

                    case 11:
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
