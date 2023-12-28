using System.Text.Json.Serialization;

namespace BankAPI.Data.DTOs;

public class CuentaDTO
{
    public int Id { get; set; }

    public int TipoCuenta { get; set; }

    public int IdCliente { get; set; }

    public decimal Saldo { get; set; }

    public DateTime FechaRegistro { get; set; }
}
