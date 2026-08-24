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

    }
}