using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;
using BankAPI.Data.DTOs;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace BankAPI.Services;

public class TransaccionService{
    private readonly BancoContext _contexto;

    public TransaccionService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<Cliente?> Get(int id){
        return await _contexto.Clientes.FindAsync(id);
    }

    public async Task<IEnumerable<CuentaDTOout>> GetCuentas(int id){
        return await _contexto.Cuenta.Where(a => a.IdCliente == id)
        .Select(a => new CuentaDTOout{
            Id = a.Id,
            NombreTipoCuenta = a.TipoCuentaNavigation.Nombre,
            NombreCliente = a.IdClienteNavigation.Nombre,
            Saldo = a.Saldo,
            FechaRegistro = a.FechaRegistro
        }).ToListAsync();
    }

    public async Task<Cuentum?> GetCuentaById(int idCliente, int idCuenta){
        return await _contexto.Cuenta.Where(a => a.IdCliente == idCliente && a.Id == idCuenta)
        .SingleOrDefaultAsync();
    }

    public async Task doTransaccion(TransaccionDTO transaccion){
        SqlParameter cuentaID = new SqlParameter("@cuentaID", transaccion.CuentaId);
        SqlParameter tipo = new SqlParameter("@tipo", transaccion.TipoTransaccion);
        SqlParameter cantidad = new SqlParameter("@cantidad", transaccion.Cantidad);
        SqlParameter cuentaExt = new SqlParameter("@cuentaext", transaccion.CuentaExterna);
        SqlParameter[] param = {cuentaID, tipo, cantidad, cuentaExt};
        
        _contexto.Database.ExecuteSqlRaw("exec crearTransaccion @cuentaID, @tipo, @cantidad, @cuentaext", param);
        await _contexto.SaveChangesAsync();
    }

    public async Task DeleteCuenta(Cuentum cuentaOnDB){
        _contexto.Cuenta.Remove(cuentaOnDB);
        await _contexto.SaveChangesAsync();
    }
}