using System;
using System.Collections.Generic;
using System.Globalization;
using RevisarVeiculo.Models;

namespace RevisarVeiculo.Services
{
    public class MotorVistoria
    {

        private static readonly Dictionary<string, string> Recomendacoes = new Dictionary<string, string>
        {
            // Itens genéricos (RF02)
            { "Nível de Óleo do Motor", "Verificar nível/qualidade e trocar o óleo do motor conforme manual do fabricante." },
            { "Bateria e Sistema Elétrico", "Testar carga da bateria e revisar fiação/sistema elétrico do veículo." },
            { "Documentação Regularizada", "Regularizar documentação, IPVA e transferência do veículo." },

            // Itens de Carro (RF03.1)
            { "Estepe e Macaco", "Calibrar pneu reserva e verificar funcionamento do macaco." },
            { "Triângulo de Sinalização", "Repor equipamento obrigatório ausente/danificado." },
            { "Ar Condicionado Funcional", "Realizar higienização e checagem do gás refrigerante." },

            // Itens de Moto (RF03.2)
            { "Kit Transmissão/Corrente", "Lubrificar e ajustar a tensão do kit relação/corrente." },
            { "Manetes de Freio/Embreagem", "Regular folga e revisar cabos das manetes de freio/embreagem." },
            { "Pezinho Lateral", "Verificar mola e fixação do pezinho lateral (cavalete)." },

            // Itens de Caminhão (RF03.3)
            { "Tacógrafo", "Aferir e calibrar o tacógrafo conforme legislação vigente." },
            { "Sistema de Freios a Ar", "Revisar compressor, válvulas e mangueiras do sistema de freios a ar." },
            { "Trava e Lona da Caçamba", "Substituir lona e reparar sistema de travamento da caçamba." }
        };

        public int CalcularPontuacaoObtida(Veiculo veiculo)
        {
            int total = 0;
            foreach (var item in veiculo.VistoriaRealizada)
            {
                total += item.Pontos;
            }
            return total;
        }

        public int CalcularPontuacaoMaxima(Veiculo veiculo)
        {
            return veiculo.VistoriaRealizada.Count * 10;
        }

        public double CalcularPercentual(Veiculo veiculo)
        {
            int obtida = CalcularPontuacaoObtida(veiculo);
            int maxima = CalcularPontuacaoMaxima(veiculo);

            if (maxima == 0) return 0;


            return (double)obtida / maxima * 100;
        }

        public string ClassificarVeiculo(double percentual)
        {
            if (percentual >= 90)
            {
                return "APROVADO COM EXCELÊNCIA";
            }
            else if (percentual >= 60)
            {
                return "APROVADO COM APONTAMENTOS";
            }
            else
            {
                return "REPROVADO NA VISTORIA";
            }
        }

        public string ObterAcaoCorporativa(string classificacao)
        {
            switch (classificacao)
            {
                case "APROVADO COM EXCELÊNCIA":
                    return "Liberado para compra/revenda imediata.";
                case "APROVADO COM APONTAMENTOS":
                    return "Exige desconto na compra para reparos da oficina.";
                case "REPROVADO NA VISTORIA":
                    return "Veículo recusado pela concessionária.";
                default:
                    return string.Empty;
            }
        }

        private string ObterRecomendacao(ItemVistoria item)
        {
            if (Recomendacoes.TryGetValue(item.Nome, out var texto))
            {
                return texto;
            }
            return $"Realizar inspeção detalhada e reparo do item '{item.Nome}'.";
        }

        public void ExibirRelatorio(Veiculo veiculo, int numeroAtual, int totalVistorias)
        {
            int pontuacaoObtida = CalcularPontuacaoObtida(veiculo);
            int pontuacaoMaxima = CalcularPontuacaoMaxima(veiculo);
            double percentual = CalcularPercentual(veiculo);
            string classificacao = ClassificarVeiculo(percentual);
            string acao = ObterAcaoCorporativa(classificacao);

            Console.WriteLine($"[{numeroAtual}/{totalVistorias}] PROCESSANDO VISTORIA");
            Console.WriteLine(new string('-', 69));

            Console.WriteLine("> DADOS DO VEÍCULO:");
            Console.WriteLine($"  - Tipo: {veiculo.Tipo}");
            Console.WriteLine($"  - Modelo: {veiculo.Marca} {veiculo.Modelo}");
            Console.WriteLine($"  - Ano: {veiculo.Ano} | Quilometragem: {veiculo.Quilometragem.ToString("N0")} km");
            Console.WriteLine($"  - Atributo Específico: {veiculo.ObterAtributoEspecificoTexto()}");
            Console.WriteLine();

            Console.WriteLine($"> AVALIAÇÃO DOS ITENS INSPECIONADOS ({veiculo.VistoriaRealizada.Count} ITENS):");
            foreach (var item in veiculo.VistoriaRealizada)
            {
                string marcador = item.Status switch
                {
                    "Bom" => "[OK]",
                    "Regular" => "[ ! ]",
                    "Ruim" => "[ X ]",
                    _ => "[ ? ]"
                };
                string nomeFormatado = item.Nome.PadRight(30, '-');
                Console.WriteLine($"  {marcador} {nomeFormatado} Status: {item.Status} ({item.Pontos} pts)");
            }
            Console.WriteLine();

            Console.WriteLine("> RESUMO DA PONTUAÇÃO:");
            Console.WriteLine($"  - Pontuação Atingida: {pontuacaoObtida} de {pontuacaoMaxima} pontos possíveis");
            Console.WriteLine($"  - Percentual de Aprovação: {percentual.ToString("N1")}%");
            Console.WriteLine($"  - Classificação Final: [ {classificacao} ]");
            Console.WriteLine($"  - Ação Corporativa: {acao}");
            Console.WriteLine();

            Console.WriteLine("> RELATÓRIO DE MANUTENÇÃO E RECOMENDAÇÕES DA OFICINA:");

            var itensRuins = new List<ItemVistoria>();
            var itensRegulares = new List<ItemVistoria>();
            foreach (var item in veiculo.VistoriaRealizada)
            {
                if (item.Status == "Ruim") itensRuins.Add(item);
                else if (item.Status == "Regular") itensRegulares.Add(item);
            }

            if (itensRuins.Count == 0 && itensRegulares.Count == 0)
            {
                Console.WriteLine("Nenhuma pendência mecânica identificada. Veículo liberado para operação!");
            }
            else
            {
                if (itensRuins.Count > 0)
                {
                    Console.WriteLine("ITENS CRÍTICOS / REPROVADOS (AÇÃO IMEDIATA):");
                    foreach (var item in itensRuins)
                    {
                        Console.WriteLine($"     - {item.Nome}: {ObterRecomendacao(item)}");
                    }
                    Console.WriteLine();
                }

                if (itensRegulares.Count > 0)
                {
                    Console.WriteLine("ITENS DE ATENÇÃO (REVISÃO PREVENTIVA):");
                    foreach (var item in itensRegulares)
                    {
                        Console.WriteLine($"     - {item.Nome}: {ObterRecomendacao(item)}");
                    }
                }
            }

            Console.WriteLine();
            Console.WriteLine(new string('-', 69));
            Console.WriteLine();
        }
    }
}