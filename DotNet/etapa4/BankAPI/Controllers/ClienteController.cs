using BankAPI.Services;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ClienteController: ControllerBase{

    private readonly ClienteService _servicio;

    public ClienteController(ClienteService servicio){
        _servicio = servicio;
    }

    [HttpGet]  
    public async Task<IEnumerable<Cliente>>  Get(){
        return await _servicio.Get();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(int id){
        var cliente = await _servicio.GetById(id);

        if (cliente is null)
            return NotFound();
        
        return cliente;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente){
        var newCliente = await _servicio.Create(cliente);

        return CreatedAtAction(nameof(GetById), new {id = newCliente.Id}, newCliente);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Cliente cliente){
        if (id != cliente.Id)
            return BadRequest();

        var clienteOnDB = await _servicio.GetById(id);

        if (clienteOnDB is not null){
            await _servicio.Update(id, cliente);
            return NoContent();
        } else {
            return NotFound();
        } 
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id){
        var clienteOnDB = await _servicio.GetById(id);

        if (clienteOnDB is not null){
            await _servicio.Delete(id);
            return Ok();
        } else {
            return NotFound();
        } 
    }
 
}