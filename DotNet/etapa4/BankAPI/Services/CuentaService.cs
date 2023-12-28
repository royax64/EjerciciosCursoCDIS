using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class CuentaService{
    private readonly BancoContext _contexto;

    public CuentaService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<IEnumerable<Cuentum>> Get(){
        return await _contexto.Cuenta.ToListAsync();
    }

    public async Task<Cuentum?> GetById(int id){
        return await _contexto.Cuenta.FindAsync(id);
    }

    public async Task<Cuentum> Create(Cuentum cuenta){
        _contexto.Cuenta.Add(cuenta);
        await _contexto.SaveChangesAsync();

        return cuenta;
    }

    public async Task Update(int id, Cuentum cuenta){
        var cuentaOnDB = await GetById(id);
        if (cuentaOnDB is not null){
            
            cuentaOnDB.TipoCuenta = cuenta.TipoCuenta;
            cuentaOnDB.IdCliente = cuenta.IdCliente;
            cuentaOnDB.Saldo = cuenta.Saldo;

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