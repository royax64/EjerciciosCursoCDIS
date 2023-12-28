using BankAPI.Services;
using BankAPI.Data.BankModels;
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
            return NotFound();
        
        return cuenta;
    }

/*     [HttpGet("cliente/{id}")]
    public async Task<IEnumerable<Cuentum>> GetAccountsByClientId(int id){
        var cuentasUser = await _servicio.GetAccountsByClientId(id);

        if (cuentasUser is not null){
            return cuentasUser;
        } else {
            return (IEnumerable<Cuentum>) NotFound();
        }        
    } */

    [HttpPost]
    public async Task<IActionResult> Create(Cuentum cuenta){
        var newCuenta = await _servicio.Create(cuenta);

        return CreatedAtAction(nameof(GetById), new {id = newCuenta.Id}, newCuenta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Cuentum cuenta){
        if (id != cuenta.Id)
            return BadRequest();

        var cuentaOnDB = await _servicio.GetById(id);

        if (cuentaOnDB is not null){
            await _servicio.Update(id, cuenta);
            return NoContent();
        } else {
            return NotFound();
        } 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var cuentaOnDB = await _servicio.GetById(id);

        if (cuentaOnDB is not null){
            await _servicio.Delete(id);
            return Ok();
        } else {
            return NotFound();
        } 
    }
 
}