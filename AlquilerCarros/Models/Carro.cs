using System;
using System.Collections.Generic;

namespace AlquilerCarros.Models;

public partial class Carro
{
    public int IdCarro { get; set; }

    public string? Placa { get; set; }

    public string? Marca { get; set; }

    public string? Modelo { get; set; }

    public double? Costo { get; set; }

    public bool? Disponible { get; set; }

    public virtual ICollection<Alquiler> Alquilers { get; set; } = new List<Alquiler>();
}
