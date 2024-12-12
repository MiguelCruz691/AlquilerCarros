namespace AlquilerCarros.DTO
{
    public class ClienteCarroAlquilerDTO
    {
        public string? Cedula { get; set; }

        public string? Nombre { get; set; }
        public DateOnly? FechaInicio { get; set; }

        public DateOnly? FechaFin { get; set; }

        public int? Tiempo { get; set; }

        public decimal? Saldo { get; set; }
        public string? Placa { get; set; }

        public string? Marca { get; set; }
    }
}
