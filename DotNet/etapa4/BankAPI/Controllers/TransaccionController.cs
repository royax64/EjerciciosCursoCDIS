using BankAPI.Services;
using BankAPI.Data.BankModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BankAPI.Data.DTOs;

namespace BankAPI.Controllers;

[Authorize(Policy = "Cliente")]
[ApiController]
[Route("API/[controller]")]
public class TransaccionController: ControllerBase{

    private readonly TransaccionService _servicio;
    private readonly TipoTransaccionService _tipoTransaccionServicio;

    public TransaccionController(TransaccionService servicio, TipoTransaccionService tipoTransaccionServicio){
        _servicio = servicio;
        _tipoTransaccionServicio = tipoTransaccionServicio;
    }

    [HttpGet("me")]  
    public async Task<ActionResult<Cliente?>> Get(){
        int loggedInId = GetLoggedInId();
        if (loggedInId == -1)
            return NotFound(new {message = $"Cannot verify identity, have you logged in?"});

        return await _servicio.Get(loggedInId!);
    }

    [HttpGet("myAccounts")]  
    public async Task<IEnumerable<CuentaDTOout?>?> GetCuentas(){
        int loggedInId = GetLoggedInId();
        if (loggedInId == -1)
            return NoContent() as IEnumerable<CuentaDTOout?>;

        return await _servicio.GetCuentas(loggedInId!);
    }

    [HttpPost("withdrawal")] //Sólo tipos de transferencia 3 y 4 (cuentaext siempre será null si tipo = 3)
    public async Task<ActionResult> doTransaccionRetiro(TransaccionDTO transaccion){
        int loggedInId = GetLoggedInId();
        if (loggedInId == -1)
            return NotFound(new {message = $"Cannot verify identity, have you logged in?"});

        //validar CuentaId (que exista y pertenezca al user) y que complete el retiro.
        var cuenta = await _servicio.GetCuentaById(loggedInId, transaccion.CuentaId);
        if (cuenta is null)
            return BadRequest(new {message = "This account does not belong to you."});

        if (cuenta.Saldo - transaccion.Cantidad < 0)
            return BadRequest(new {message = "Not enough money!"});

        //Validar TipoTransaccion (que exista)
        var tipo = await _tipoTransaccionServicio.GetById(transaccion.TipoTransaccion);
        if (tipo is null || !(transaccion.TipoTransaccion == 3 || transaccion.TipoTransaccion == 4))
            return BadRequest(new {message = "Invalid or not allowed transaction type."});

        //Make CuentaExterna null if necessary
        if (transaccion.TipoTransaccion == 3)
            if (transaccion.CuentaExterna is not null)
                return BadRequest(new {message = "External Account must be null."});
            else 
                transaccion.CuentaExterna = 0; //Para que tenga un valor, el procedimiento almacenado volverá a hacerlo null

        await _servicio.doTransaccion(transaccion);
        return Ok();
    }

    [HttpPost("deposit")] //Sólo tipos de transferencia 1 (la 2 no se usa), cuentaext siempre es nulo!.
    public async Task<ActionResult> doTransaccionDeposito(TransaccionDTO transaccion){
        int loggedInId = GetLoggedInId();
        if (loggedInId == -1)
            return NotFound(new {message = $"Cannot verify identity, have you logged in?"});

        //validar CuentaId (que exista y pertenezca al user)
        var cuenta = await _servicio.GetCuentaById(loggedInId, transaccion.CuentaId);
        if (cuenta is null)
            return BadRequest(new {message = "This account does not belong to you."});

        //Validar TipoTransaccion (que exista)
        var tipo = await _tipoTransaccionServicio.GetById(transaccion.TipoTransaccion);
        if (tipo is null || transaccion.TipoTransaccion != 1 )
            return BadRequest(new {message = "Invalid or not allowed transaction type."});

        //Make CuentaExterna always null
        if (transaccion.CuentaExterna is not null)
            return BadRequest(new {message = "External Account must be null."});

        transaccion.CuentaExterna = 0; //Para que tenga un valor, el procedimiento almacenado volverá a hacerlo null
        await _servicio.doTransaccion(transaccion);
        return Ok();
    }

    [HttpDelete("deleteAccount/{id}")]
    public async Task<IActionResult> DeleteCuenta(int id){
        int loggedInId = GetLoggedInId();
        if (loggedInId == -1)
            return NotFound(new {message = $"Cannot verify identity, have you logged in?"});

        var cuentaOnDB = await _servicio.GetCuentaById(loggedInId, id);
        if (cuentaOnDB is null)
            return CuentaNotFound(id);

        if (cuentaOnDB.Saldo != 0)
            return BadRequest(new {message = "The account still has money in it."});

        await _servicio.DeleteCuenta(cuentaOnDB);
        return Ok();
    }

    [NonAction]
    public int GetLoggedInId(){
        var loggedIn = HttpContext.User.Identity as ClaimsIdentity;
        if (loggedIn != null){
            return int.Parse(loggedIn.FindFirst("ClienteID")!.Value);
        }
        return -1;
    }

    [NonAction]
    public NotFoundObjectResult CuentaNotFound(int id){
        return NotFound(new {message = $"An account with id #{id} does not exist."});
    }

    
 
}