using System;
using System.Collections.Generic;

namespace BankAPI.Data.BankModels;

public partial class Cuentum
{
    public int Id { get; set; }

    public int TipoCuenta { get; set; }

    public int IdCliente { get; set; }

    public decimal Saldo { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual TipoCuentum TipoCuentaNavigation { get; set; } = null!;

    public virtual ICollection<Transaccion> Transaccions { get; set; } = new List<Transaccion>();
}
