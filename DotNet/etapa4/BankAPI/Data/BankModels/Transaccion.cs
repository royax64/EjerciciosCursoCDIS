using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BankAPI.Data.BankModels;

public partial class Transaccion
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    public int TipoTransaccion { get; set; }

    public decimal Cantidad { get; set; }

    public int? CuentaExterna { get; set; }

    public DateTime FechaRegistro { get; set; }

    [JsonIgnore]
    public virtual Cuentum Cuenta { get; set; } = null!;

    [JsonIgnore]
    public virtual TipoTransaccion TipoTransaccionNavigation { get; set; } = null!;
}
