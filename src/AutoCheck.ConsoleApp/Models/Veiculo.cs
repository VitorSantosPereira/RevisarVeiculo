namespace RevisarVeiculo
{

    public abstract class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }

        protected Veiculo(string marca, string modelo, int ano, double quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
        }
    }
}
