using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.DTOs;

public class TransaccionDTO
{
    public int CuentaId { get; set; }

    public int TipoTransaccion { get; set; }

    public decimal Cantidad { get; set; }

    public int? CuentaExterna { get; set; }
}
