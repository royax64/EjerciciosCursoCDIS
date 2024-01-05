using BankAPI.Data;
using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class TipoCuentaService{
    private readonly BancoContext _contexto;

    public TipoCuentaService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<IEnumerable<TipoCuentum>> Get(){
        return await _contexto.TipoCuenta.ToListAsync();
    }

    public async Task<TipoCuentum?> GetById(int id){
        return await _contexto.TipoCuenta.FindAsync(id);
    }

}