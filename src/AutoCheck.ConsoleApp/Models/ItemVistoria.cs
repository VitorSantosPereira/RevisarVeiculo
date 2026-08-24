namespace RevisarVeiculo
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
                // Encapsulamento: garante que apenas os 3 status de negócio sejam aceitos.
                if (!StatusValidos.Contains(value))
                {
                    throw new ArgumentException(
                        $"Status inválido: '{value}'. Valores aceitos: Bom, Regular ou Ruim.");
                }
                _status = value;
            }
        }

    }
}