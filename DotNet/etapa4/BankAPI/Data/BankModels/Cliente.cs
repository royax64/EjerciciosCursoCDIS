using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Data.BankModels;

public partial class Cliente
{
    public int Id { get; set; }

    [MaxLength(200, ErrorMessage = "Client name must be no more than 200 characters.")]
    public string Nombre { get; set; } = null!;

    [MaxLength(40, ErrorMessage = "Phone number must be no more than 40 characters.")]
    public string NumeroTelefono { get; set; } = null!;

    [MaxLength(50, ErrorMessage = "Email must be no more than 50 characters.")]
    [EmailAddress( ErrorMessage = "Invalid email.")]
    public string? Correo { get; set; }

    public DateTime FechaRegistro { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cuentum> Cuenta { get; set; } = new List<Cuentum>();
}
