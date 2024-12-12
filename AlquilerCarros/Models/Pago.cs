using System;
using System.Collections.Generic;

namespace AlquilerCarros.Models;

public partial class Pago
{
    public int IdPagos { get; set; }

    public DateOnly? Fecha { get; set; }

    public double? Valor { get; set; }

    public int IdAlquiler { get; set; }

    public virtual Alquiler IdAlquilerNavigation { get; set; } = null!;
}
