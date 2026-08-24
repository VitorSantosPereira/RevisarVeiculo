namespace RevisarVeiculo.Models
{
    public class ItemVistoria
    {
        private static readonly string[] StatusValidos = { "Bom", "Regular", "Ruim" };

        public string Nome { get; private set; }

        private string _status = "Ruim";
        public string Status
        {
            get => _status;
            private set
            {
                if (!StatusValidos.Contains(value))
                {
                    throw new ArgumentException(
                        $"Status inválido: '{value}'. Valores aceitos: Bom, Regular ou Ruim.");
                }
                _status = value;
            }
        }

        public int Pontos
        {
            get
            {
                switch (Status)
                {
                    case "Bom":
                        return 10;
                    case "Regular":
                        return 5;
                    case "Ruim":
                        return 0;
                    default:
                        return 0;
                }
            }
        }

        public ItemVistoria(string nome, string status)
        {
            this.Nome = nome;
            this.Status = status;
        }
    }
}