using BankAPI.Services;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers;

[Authorize]
[ApiController]
[Route("API/[controller]")]
public class CuentaController: ControllerBase{

    private readonly CuentaService _servicio;
    private readonly ClienteService _clienteServicio;
    private readonly TipoCuentaService _tipoCuentaServicio;

    public CuentaController (CuentaService servicio,
                            TipoCuentaService tipoCuentaServicio,
                            ClienteService clienteServicio){
        this._servicio = servicio;
        this._tipoCuentaServicio = tipoCuentaServicio;
        this._clienteServicio = clienteServicio;
    }

    [Authorize(Policy = "MegaBoss")]
    [HttpGet("all")]  
    public async Task<IEnumerable<CuentaDTOout>>  Get(){
        return await _servicio.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CuentaDTOout>> GetById(int id){
        var cuenta = await _servicio.GetDTObyId(id);

        if (cuenta is null)
            return CuentaNotFound(id);
        
        return cuenta;
    }

    [HttpPost("new")]
    public async Task<IActionResult> Create(CuentaDTOIn cuenta){
        var (isValid, message) = await ValidateNewCuenta(cuenta);

        if (!isValid){
            return BadRequest(message);;
        } else {
            var newCuenta = await _servicio.Create(cuenta);
            return CreatedAtAction(nameof(GetById), new {id = newCuenta.Id}, newCuenta);
        } 
    }

    [HttpPut("update/{id}")]
    public async Task<IActionResult> Update(int id, CuentaDTOIn cuenta){
        var (isValid, message) = await ValidateNewCuenta(cuenta);

        if (id != cuenta.Id)
            return BadRequest(new {message = $"Account id {cuenta.Id} from request does not match id from route {id}."});

        var cuentaOnDB = await _servicio.GetById(id);

        if (cuentaOnDB is not null && isValid){
            await _servicio.Update(id, cuenta);
            return NoContent();
        } else {
            return CuentaNotFound(id);
        } 
    }

    [Authorize(Policy = "MegaBoss")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> Delete(int id){
        var cuentaOnDB = await _servicio.GetById(id);

        if (cuentaOnDB is not null){
            await _servicio.Delete(id);
            return Ok();
        } else {
            return CuentaNotFound(id);
        } 
    }

    [NonAction]
    public NotFoundObjectResult CuentaNotFound(int id){
        return NotFound(new {message = $"An account with id #{id} does not exist."});
    }

    [NonAction]
    public async Task<(bool isValid, string message)> ValidateNewCuenta(CuentaDTOIn cuenta){
        var clientes = await _clienteServicio.Get();
        var clienteCuenta = clientes.Where(c => c.Id == cuenta.IdCliente);
        var tipoCuenta = _tipoCuentaServicio.GetById(cuenta.TipoCuenta);

        if (!clienteCuenta.Any()){
            return (false, $"Client id from request does not exist.");
        }

        if (tipoCuenta is null){
            return (false, $"Invalid Account Type {cuenta.TipoCuenta}.");
        }

        return (true, "valid");
    }
 
}