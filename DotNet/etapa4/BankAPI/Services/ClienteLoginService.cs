using BankAPI.Data;
using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class ClienteLoginService{
    private readonly BancoContext _contexto;

    public ClienteLoginService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<Cliente?> GetClienteDetails(ClienteLoginDTO loginDetails){
        return await _contexto.Clientes
        .SingleOrDefaultAsync(a => 
        a.Correo == loginDetails.Correo && a.Passwd == loginDetails.Passwd);
    }
}