using BankAPI.Data;
using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class TipoTransaccionService{
    private readonly BancoContext _contexto;

    public TipoTransaccionService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<IEnumerable<TipoTransaccion>> Get(){
        return await _contexto.TipoTransaccions.ToListAsync();
    }

    public async Task<TipoTransaccion?> GetById(int id){
        return await _contexto.TipoTransaccions.FindAsync(id);
    }

}