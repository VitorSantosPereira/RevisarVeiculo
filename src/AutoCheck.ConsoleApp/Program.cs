using System;
using System.Collections.Generic;
using System.Globalization;
using RevisarVeiculo.Services;
using RevisarVeiculo.Models;
//PtBr
namespace RevisarVeiculo
{
    internal class Program
    {
        private static readonly List<Veiculo> Vistorias = new List<Veiculo>();
        private static readonly MotorVistoria Motor = new MotorVistoria();


        private static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool continuar = true;
            do
            {
                ExibirCabecalho();
                ExibirMenu();

                string opcao = Console.ReadLine()?.Trim() ?? "";

                switch (opcao)
                {
                    case "1":
                        RealizarNovaVistoria();
                        break;
                    case "2":
                        ExibirRelatorioVistorias();
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine();
                        Console.WriteLine("Encerrando o AutoCheck .NET. Até a próxima!");
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        Pausar();
                        break;
                }
            } while (continuar);
        }

        private static void ExibirCabecalho()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 69));
            Console.WriteLine("                AUTOCHECK .NET - MOTOR DE VISTORIA                 ");
            Console.WriteLine(new string('=', 69));
            Console.WriteLine();
        }

        private static void ExibirMenu()
        {
            Console.WriteLine("MENU PRINCIPAL");
            Console.WriteLine(new string('-', 69));
            Console.WriteLine("  1 - Realizar Nova Vistoria");
            Console.WriteLine("  2 - Exibir Relatório das Vistorias");
            Console.WriteLine("  0 - Sair");
            Console.WriteLine(new string('-', 69));
            Console.Write("Escolha uma opção: ");
        }

        private static void Pausar()
        {
            Console.WriteLine();
            Console.Write("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
        private static void RealizarNovaVistoria()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 69));
            Console.WriteLine("                     NOVA VISTORIA - CADASTRO                      ");
            Console.WriteLine(new string('=', 69));
            Console.WriteLine();

            Veiculo veiculo = SolicitarTipoEDadosVeiculo();

            if (veiculo == null)
            {
                Console.WriteLine("Cadastro cancelado.");
                Pausar();
                return;
            }

            Console.WriteLine();
            Console.WriteLine($"> CHECKLIST OBRIGATÓRIO - {veiculo.Tipo.ToUpper()}");
            Console.WriteLine(new string('-', 69));

            List<string> checklist = veiculo.ObterChecklistObrigatorio();

            foreach (string itemNome in checklist)
            {
                string status = LerStatus(itemNome);
                veiculo.AdicionarItemVistoriado(itemNome, status);
            }

            Vistorias.Add(veiculo);

            Console.WriteLine();
            Console.WriteLine("Vistoria registrada com sucesso! Confira o resultado abaixo:");
            Console.WriteLine();

            Console.WriteLine(new string('=', 69));

            Motor.ExibirRelatorio(veiculo, 1, 1);

            Pausar();
        }
        private static Veiculo SolicitarTipoEDadosVeiculo()
        {
            Console.WriteLine("Tipo de veículo:");
            Console.WriteLine("  1 - Carro");
            Console.WriteLine("  2 - Moto");
            Console.WriteLine("  3 - Caminhão");
            Console.Write("Escolha o tipo: ");

            string tipoOpcao = Console.ReadLine()?.Trim() ?? "";

            if (tipoOpcao != "1" && tipoOpcao != "2" && tipoOpcao != "3")
            {
                Console.WriteLine("Tipo de veículo inválido.");
                return null;
            }

            Console.WriteLine();

            string marca = LerTexto("Marca: ");
            string modelo = LerTexto("Modelo: ");
            int ano = LerInteiro("Ano: ");
            double km = LerDouble("Quilometragem (km): ");

            switch (tipoOpcao)
            {
                case "1":
                    {
                        int portas = LerInteiro("Quantidade de portas: ");
                        return new Carro(marca, modelo, ano, km, portas);
                    }

                case "2":
                    {
                        int cilindradas = LerInteiro("Cilindradas (cc): ");
                        return new Moto(marca, modelo, ano, km, cilindradas);
                    }

                case "3":
                    {
                        int eixos = LerInteiro("Quantidade de eixos: ");
                        double capacidade = LerDouble("Capacidade de carga (toneladas): ");
                        return new Caminhao(marca, modelo, ano, km, eixos, capacidade);
                    }

                default:
                    return null;
            }
        }
        private static void ExibirRelatorioVistorias()
        {
            Console.Clear();
            Console.WriteLine(new string('=', 69));
            Console.WriteLine("                RELATÓRIO GERAL DE VISTORIAS                       ");
            Console.WriteLine(new string('=', 69));
            Console.WriteLine();

            if (Vistorias.Count == 0)
            {
                Console.WriteLine("Nenhuma vistoria realizada até o momento.");
                Pausar();
                return;
            }

            int total = Vistorias.Count;
            for (int i = 0; i < total; i++)
            {
                Motor.ExibirRelatorio(Vistorias[i], i + 1, total);
            }

            Console.WriteLine(new string('=', 69));
            Console.WriteLine("             FIM DO RELATÓRIO DE VISTORIAS                         ");
            Console.WriteLine(new string('=', 69));

            Pausar();
        }
        private static string LerTexto(string prompt)
        {
            string valor;
            do
            {
                Console.Write(prompt);
                valor = Console.ReadLine()?.Trim() ?? "";
                if (string.IsNullOrWhiteSpace(valor))
                {
                    Console.WriteLine("  Valor não pode ser vazio. Tente novamente.");
                }
            } while (string.IsNullOrWhiteSpace(valor));

            return valor;
        }

        private static int LerInteiro(string prompt)
        {
            int valor;
            bool valido;
            do
            {
                Console.Write(prompt);
                string entrada = Console.ReadLine()?.Trim() ?? "";
                valido = int.TryParse(entrada, out valor);
                if (!valido)
                {
                    Console.WriteLine("  Valor inválido. Digite um número inteiro.");
                }
            } while (!valido);

            return valor;
        }

        private static double LerDouble(string prompt)
        {
            double valor;
            bool valido;
            do
            {
                Console.Write(prompt);
                string entrada = Console.ReadLine()?.Trim() ?? "";
                entrada = entrada.Replace(",", ".");
                valido = double.TryParse(
                    entrada, NumberStyles.Any, CultureInfo.InvariantCulture, out valor);
                if (!valido)
                {
                    Console.WriteLine("  Valor inválido. Digite um número (ex: 45000 ou 30,5).");
                }
            } while (!valido);

            return valor;
        }
        private static string LerStatus(string nomeItem)
        {
            while (true)
            {
                Console.Write($"  - {nomeItem} [Bom/Regular/Ruim]: ");
                string entrada = Console.ReadLine()?.Trim() ?? "";

                if (string.Equals(entrada, "Bom", StringComparison.OrdinalIgnoreCase)) return "Bom";
                if (string.Equals(entrada, "Regular", StringComparison.OrdinalIgnoreCase)) return "Regular";
                if (string.Equals(entrada, "Ruim", StringComparison.OrdinalIgnoreCase)) return "Ruim";

                Console.WriteLine("    Status inválido. Digite exatamente: Bom, Regular ou Ruim.");
            }
        }
    }
}