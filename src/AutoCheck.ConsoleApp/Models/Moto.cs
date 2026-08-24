namespace RevisarVeiculo.Models
{
    public class Moto : Veiculo
    {
        public int Cilindradas { get; set; }
        public Moto(string marca, string modelo, int ano, double quilometragem, int cilindradas)
           : base(marca, modelo, ano, quilometragem)
        {
            this.Cilindradas = cilindradas;
        }

        public override string Tipo => "Moto";

        public override string ObterAtributoEspecificoTexto()
        {
            return $"{Cilindradas}cc";
        }

        public override List<string> ObterChecklistObrigatorio()
        {
            var checklist = base.ObterChecklistObrigatorio();
            checklist.Add("Kit Transmissão/Corrente");
            checklist.Add("Manetes de Freio/Embreagem");
            checklist.Add("Pezinho Lateral");
            return checklist;
        }
    }
}