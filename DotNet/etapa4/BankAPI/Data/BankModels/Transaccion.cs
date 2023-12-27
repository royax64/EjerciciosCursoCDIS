using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class Transaccion
{
    public int Id { get; set; }

    public int CuentaId { get; set; }

    public int TipoTransaccion { get; set; }

    public decimal Cantidad { get; set; }

    public int? CuentaExterna { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Cuentum Cuenta { get; set; } = null!;

    public virtual TipoTransaccion TipoTransaccionNavigation { get; set; } = null!;
}
