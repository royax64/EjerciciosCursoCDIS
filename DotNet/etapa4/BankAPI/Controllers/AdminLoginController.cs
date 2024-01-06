using BankAPI.Services;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BankAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class AdminLoginController: ControllerBase{

    private readonly AdminLoginService _servicio;
    private IConfiguration config;

    public AdminLoginController(AdminLoginService servicio, IConfiguration config){
        _servicio = servicio;
        this.config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AdminDTO loginDetails){
        var adminDetails = await _servicio.GetAdminDetails(loginDetails);

        if (adminDetails is null)
            return BadRequest(new {message = "Invalid username or password."});
    
        string myToken = getToken(adminDetails);
        return Ok(new {token = myToken});
    }

    private string getToken(Administrador admin){
        var claims = new[]{
            new Claim(ClaimTypes.Name, admin.Nombre),
            new Claim(ClaimTypes.Email, admin.Correo)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value!));
        var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: credential
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}