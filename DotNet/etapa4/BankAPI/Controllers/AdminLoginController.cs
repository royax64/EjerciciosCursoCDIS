using BankAPI.Services;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("API/[controller]")]
public class AdminLoginController: ControllerBase{

    private readonly AdminLoginService _servicio;

    public AdminLoginController(AdminLoginService servicio){
        _servicio = servicio;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AdminDTO loginDetails){
        var adminDetails = await _servicio.GetAdminDetails(loginDetails);

        if (adminDetails is null)
            return BadRequest(new {message = "Invalid username or password."});
    
        //???
        return Ok(new {token = ""});
    }

}