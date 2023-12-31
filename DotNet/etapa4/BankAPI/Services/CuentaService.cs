using BankAPI.Data;
using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class CuentaService{
    private readonly BancoContext _contexto;

    public CuentaService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<IEnumerable<CuentaDTOout>> Get(){
        return await _contexto.Cuenta.Select(a => new CuentaDTOout{
            Id = a.Id,
            NombreTipoCuenta = a.TipoCuentaNavigation.Nombre,
            NombreCliente = a.IdClienteNavigation.Nombre,
            Saldo = a.Saldo,
            FechaRegistro = a.FechaRegistro
        }).ToListAsync();
    }

    public async Task<Cuentum?> GetById(int id){
        return await _contexto.Cuenta.FindAsync(id);
    }

    public async Task<CuentaDTOout?> GetDTObyId(int id){
        return await _contexto.Cuenta.Where(a => a.Id == id)
        .Select(a => new CuentaDTOout{
            Id = a.Id,
            NombreTipoCuenta = a.TipoCuentaNavigation.Nombre,
            NombreCliente = a.IdClienteNavigation.Nombre,
            Saldo = a.Saldo,
            FechaRegistro = a.FechaRegistro
        }).SingleOrDefaultAsync();
    }

    public async Task<Cuentum> Create(CuentaDTOIn cuentaBase){
        var newCuenta = new Cuentum();
        var clienteCuenta = await _contexto.Clientes.FindAsync(cuentaBase.IdCliente);
        var tipoCuenta = await _contexto.TipoCuenta.FindAsync(cuentaBase.TipoCuenta);

        newCuenta.IdCliente = cuentaBase.IdCliente;
        newCuenta.TipoCuenta = cuentaBase.TipoCuenta;
        newCuenta.Saldo = cuentaBase.Saldo;

        if (clienteCuenta != null && tipoCuenta != null) {
            newCuenta.IdClienteNavigation = clienteCuenta;
            newCuenta.TipoCuentaNavigation = tipoCuenta;
        
            _contexto.Cuenta.Add(newCuenta);
            await _contexto.SaveChangesAsync();
        }

        return newCuenta; 
    }

    public async Task Update(int id, CuentaDTOIn cuentaBase){
        var cuentaOnDB = await GetById(id);

        if (cuentaOnDB is not null){
            cuentaOnDB.TipoCuenta = cuentaBase.TipoCuenta;
            cuentaOnDB.IdCliente = cuentaBase.IdCliente;
            cuentaOnDB.Saldo = cuentaBase.Saldo;

            await _contexto.SaveChangesAsync();
        }
    }

    public async Task Delete(int id){
        var cuentaOnDB = await GetById(id);

        if (cuentaOnDB is not null){
            _contexto.Cuenta.Remove(cuentaOnDB);
            await _contexto.SaveChangesAsync();
        }
    }

}