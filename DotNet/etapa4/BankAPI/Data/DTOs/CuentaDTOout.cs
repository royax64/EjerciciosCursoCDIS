using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.DTOs;

public class CuentaDTOout
{
    public int Id { get; set; }

    public string? NombreTipoCuenta { get; set; }

    public string? NombreCliente { get; set; }

    public decimal Saldo { get; set; }

    public DateTime FechaRegistro { get; set; }
}