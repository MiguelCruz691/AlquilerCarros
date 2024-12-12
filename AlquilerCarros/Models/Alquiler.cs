using System;
using System.Collections.Generic;

namespace AlquilerCarros.Models;

public partial class Alquiler
{
    public int IdAlquiler { get; set; }

    public DateOnly? FechaInicio { get; set; }

    public DateOnly? FechaFin { get; set; }

    public int? Tiempo { get; set; }

    public double? ValorTotal { get; set; }

    public decimal? Saldo { get; set; }

    public double? AbonoInicial { get; set; }

    public bool? Devuelto { get; set; }

    public int IdCarro { get; set; }

    public int IdCliente { get; set; }

    public virtual Carro IdCarroNavigation { get; set; } = null!;

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
}
