using System;
using System.Collections.Generic;

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
                ExibirMenu();
                int opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        AdicionarCidade();
                        break;
                    case 2:
                        AdicionarRota();
                        break;
                    case 3:
                        rede.MostrarRede();
                        break;
                    case 4:
                        EncontrarCaminhoMaisCurto();
                        break;
                    case 5:
                        RemoverCidade();
                        break;
                    case 6:
                        RemoverRota();
                        break;
                    case 7:
                        AtualizarCidade();
                        break;
                    case 8:
                        AtualizarRota();
                        break;
                    case 9:
                       
                        break;
                    case 10:
                        sair = true;
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
        }

        private void ExibirMenu()
        {
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Adicionar Cidade");
            Console.WriteLine("2. Adicionar Rota");
            Console.WriteLine("3. Mostrar Rede de Transporte");
            Console.WriteLine("4. Encontrar o Caminho mais Curto");
            Console.WriteLine("5. Remover Cidade");
            Console.WriteLine("6. Remover Rota");
            Console.WriteLine("7. Atualizar Cidade");
            Console.WriteLine("8. Atualizar Rota");
            Console.WriteLine("9. Listar dados do grafo");
            Console.WriteLine("10. Sair");
            Console.Write("Escolha uma opção: ");
        }

        private void AdicionarCidade()
        {
            Console.Write("Digite o nome da cidade: ");
            string nomeCidade = Console.ReadLine();
            while (string.IsNullOrEmpty(nomeCidade))
            {
                Console.Write("Digite o nome da cidade (não pode ser vazio): ");
                nomeCidade = Console.ReadLine();
            }
            Cidade cidade = new Cidade(nomeCidade);
            rede.AdicionarCidade(cidade);
        }

        private void AdicionarRota()
        {
            Console.Write("Digite o nome da cidade de origem: ");
            string origemCidade = Console.ReadLine();
            Console.Write("Digite o nome da cidade de destino: ");
            string destino = Console.ReadLine();
            Console.Write("Digite a distância entre as cidades (em km): ");
            int distancia = int.Parse(Console.ReadLine());
            Console.Write("Digite o tipo de transporte: ");
            string tipoTransporte = Console.ReadLine();
            Console.Write("Digite o tempo de viagem (em minutos): ");
            int tempoViagem = int.Parse(Console.ReadLine());

            var cidadeOrigem = rede.ConsultarCidade(origemCidade);
            var cidadeDestino = rede.ConsultarCidade(destino);

            if (cidadeOrigem != null && cidadeDestino != null)
            {
                rede.AdicionarRota(cidadeOrigem, cidadeDestino, distancia, tipoTransporte, tempoViagem);
            }
            else
            {
                Console.WriteLine("Uma ou ambas as cidades não foram encontradas.");
            }
        }

        private void EncontrarCaminhoMaisCurto()
        {
            Console.Write("Digite o nome da cidade de origem: ");
            string origemNome = Console.ReadLine();
            Console.Write("Digite o nome da cidade de destino: ");
            string destinoNome = Console.ReadLine();

            Cidade origem = rede.ConsultarCidade(origemNome);
            Cidade destino = rede.ConsultarCidade(destinoNome);

            if (origem != null && destino != null)
            {
                List<Cidade> caminho = rede.EncontrarCaminhoMaisCurto(origem, destino);
                if (caminho != null && caminho.Count > 0)
                {
                    Console.WriteLine("Caminho mais curto encontrado:");
                    foreach (var cidade in caminho)
                    {
                        Console.WriteLine(cidade.Nome);
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
        }

        private void RemoverCidade()
        {
            Console.Write("Digite o nome da cidade a remover: ");
            string cidadeRemover = Console.ReadLine();
            rede.RemoverCidade(cidadeRemover);
        }

        private void RemoverRota()
        {
            Console.Write("Digite o nome da cidade de origem: ");
            string origemRemover = Console.ReadLine();
            Console.Write("Digite o nome da cidade de destino: ");
            string destinoRemover = Console.ReadLine();
            rede.RemoverRota(origemRemover, destinoRemover);
        }

        private void AtualizarCidade()
        {
            Console.Write("Digite o nome da cidade que deseja atualizar: ");
            string cidadeAntiga = Console.ReadLine();
            Console.Write("Digite o novo nome da cidade: ");
            string cidadeNova = Console.ReadLine();
            rede.AtualizarCidade(cidadeAntiga, cidadeNova);
        }

        private void AtualizarRota()
        {
            Console.Write("Digite o nome da cidade de origem: ");
            string origemAtualizar = Console.ReadLine();
            Console.Write("Digite o nome da cidade de destino: ");
            string destinoAtualizar = Console.ReadLine();
            Console.Write("Digite a nova distância entre as cidades (em km): ");
            int novaDistancia = int.Parse(Console.ReadLine());
            rede.AtualizarRota(origemAtualizar, destinoAtualizar, novaDistancia);
        }
    }
}
