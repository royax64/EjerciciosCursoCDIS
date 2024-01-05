using BankAPI.Data;
using BankAPI.Data.DTOs;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class AdminLoginService{
    private readonly BancoContext _contexto;

    public AdminLoginService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<Administrador?> GetAdminDetails(AdminDTO loginDetails){
        return await _contexto.Administradors
        .SingleOrDefaultAsync(a => 
        a.Correo == loginDetails.Correo && a.Passwrd == loginDetails.Passwrd);
    }
}