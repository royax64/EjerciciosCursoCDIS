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
public class ClienteLoginController: ControllerBase{

    private readonly ClienteLoginService _servicio;
    private IConfiguration config;

    public ClienteLoginController(ClienteLoginService servicio, IConfiguration config){
        _servicio = servicio;
        this.config = config;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(ClienteLoginDTO loginDetails){
        var clienteDetails = await _servicio.GetClienteDetails(loginDetails);

        if (clienteDetails is null)
            return BadRequest(new {message = "Invalid username or password."});
    
        string myToken = getToken(clienteDetails);
        return Ok(new {token = myToken});
    }

    private string getToken(Cliente cliente){
        var claims = new[]{
            new Claim(ClaimTypes.Name, cliente.Nombre!),
            new Claim(ClaimTypes.Email, cliente.Correo!),
            new Claim("ClienteId", cliente.Id.ToString())
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