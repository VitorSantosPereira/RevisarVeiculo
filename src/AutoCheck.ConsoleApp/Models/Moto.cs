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
    }
}