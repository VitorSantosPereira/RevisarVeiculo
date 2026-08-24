using System.Collections.Generic;

namespace RevisarVeiculo.Models
{
    public class Carro : Veiculo
    {
        public int QuantidadePortas { get; set; }

        public Carro(string marca, string modelo, int ano, double quilometragem, int quantidadePortas)
            : base(marca, modelo, ano, quilometragem)
        {
            this.QuantidadePortas = quantidadePortas;
        }

        public override string Tipo => "Carro";

        public override string ObterAtributoEspecificoTexto()
        {
            return $"{QuantidadePortas} Portas";
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            // Reaproveita o checklist genérico da classe base e adiciona os itens específicos de carro.
            var checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Estepe e Macaco");
            checklist.Add("Triângulo de Sinalização");
            checklist.Add("Ar Condicionado Funcional");
            return checklist;
        }
    }
}