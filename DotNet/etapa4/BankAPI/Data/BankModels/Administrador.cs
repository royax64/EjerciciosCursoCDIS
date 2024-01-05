using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class Administrador
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string NumeroTelefono { get; set; } = null!;

    public string Correo { get; set; } = null!;

    public string Passwrd { get; set; } = null!;

    public string Tipo { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }
}
