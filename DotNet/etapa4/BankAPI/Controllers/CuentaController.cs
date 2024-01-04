using BankAPI.Services;
using BankAPI.Data.BankModels;
using BankAPI.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CuentaController: ControllerBase{

    private readonly CuentaService _servicio;

    public CuentaController (CuentaService servicio){
        _servicio = servicio;
    }

    [HttpGet]  
    public async Task<IEnumerable<Cuentum>>  Get(){
        return await _servicio.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cuentum>> GetById(int id){
        var cuenta = await _servicio.GetById(id);

        if (cuenta is null)
            return CuentaNotFound(id);
        
        return cuenta;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CuentaDTO cuenta){
        var clientes = await _servicio.GetClientes();
        var clienteCuenta = clientes.Where(c => c.Id == cuenta.IdCliente);

        if (!clienteCuenta.Any()){
            return BadRequest(new {message = $"Client id {clienteCuenta.Id} from request does not exist."});;
        } else {
            var newCuenta = await _servicio.Create(cuenta);
            return CreatedAtAction(nameof(GetById), new {id = newCuenta.Id}, newCuenta);
        } 
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CuentaDTO cuenta){
        var clientes = await _servicio.GetClientes();
        if (id != cuenta.Id)
            return BadRequest(new {message = $"Account id {cliente.Id} from request does not match id from route {id}."});

        var cuentaOnDB = await _servicio.GetById(id);

        if (cuentaOnDB is not null && clientes.Where(c => c.Id == cuenta.IdCliente).Any()){
            await _servicio.Update(id, cuenta);
            return NoContent();
        } else {
            return CuentaNotFound(id);
        } 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var cuentaOnDB = await _servicio.GetById(id);

        if (cuentaOnDB is not null){
            await _servicio.Delete(id);
            return Ok();
        } else {
            return CuentaNotFound(id);
        } 
    }

    public IActionResult CuentaNotFound(int id){
        return NotFound(new {message = $"An account with id #{id} does not exist."});
    }
 
}