using System.Collections.Generic;

namespace RevisarVeiculo.Models
{
    public class Caminhao : Veiculo
    {
        public int QuantidadeEixos { get; set; }
        public double CapacidadeCargaToneladas { get; set; }

        public Caminhao(string marca, string modelo, int ano, double quilometragem,
                         int quantidadeEixos, double capacidadeCargaToneladas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.QuantidadeEixos = quantidadeEixos;
            this.CapacidadeCargaToneladas = capacidadeCargaToneladas;
        }

        public override string Tipo => "Caminhão";

        public override string ObterAtributoEspecificoTexto()
        {
            return $"{QuantidadeEixos} Eixos | Cap. Carga: {CapacidadeCargaToneladas:N1} Toneladas";
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Tacógrafo");
            checklist.Add("Sistema de Freios a Ar");
            checklist.Add("Trava e Lona da Caçamba");
            return checklist;
        }
    }
}