namespace RevisarVeiculo.Models
{

    public abstract class Veiculo
    {
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public int Ano { get; set; }
        public double Quilometragem { get; set; }

        public List<ItemVistoria> VistoriaRealizada { get; private set; }

        protected Veiculo(string marca, string modelo, int ano, double quilometragem)
        {
            this.Marca = marca;
            this.Modelo = modelo;
            this.Ano = ano;
            this.Quilometragem = quilometragem;
            this.VistoriaRealizada = new List<ItemVistoria>();
        }

        public void AdicionarItemVistoriado(string nome, string status)
        {
            var item = new ItemVistoria(nome, status);
            this.VistoriaRealizada.Add(item);
        }

        public virtual List<string> ObterChecklistObrigatorio()
        {
            return new List<string>
            {
                "Nível de Óleo do Motor",
                "Bateria e Sistema Elétrico",
                "Documentação Regularizada"
            };
        }

        public abstract string Tipo { get; }

        public abstract string ObterAtributoEspecificoTexto();
    }
}
