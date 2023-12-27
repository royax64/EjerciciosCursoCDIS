using BankAPI.Data;
using BankAPI.Data.BankModels;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Services;

public class ClientService{
    private readonly BancoContext _contexto;

    public ClientService (BancoContext contexto){
        _contexto = contexto;
    }

    public async Task<IEnumerable<Cliente>> Get(){
        return await _contexto.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetById(int id){
        return await _contexto.Clientes.FindAsync(id);
    }

    public async Task<Cliente> Create(Cliente cliente){
        _contexto.Clientes.Add(cliente);
        await _contexto.SaveChangesAsync();

        return cliente;
    }

    public async Task Update(int id, Cliente cliente){
        var clienteOnDB = await GetById(id);
        if (clienteOnDB is not null){
            
            clienteOnDB.Nombre = cliente.Nombre;
            clienteOnDB.NumeroTelefono = cliente.NumeroTelefono;
            clienteOnDB.Correo = cliente.Correo;

            await _contexto.SaveChangesAsync();
        }
    }

    //NO funciona porque el insert crea una cuenta por defecto
    //Hay que eliminar la cuenta asociada al cliente
    //Recuerda que hay un trigger que afecta el insert
    public async Task Delete(int id){
        var clienteOnDB = await GetById(id);

        if (clienteOnDB is not null){
            _contexto.Clientes.Remove(clienteOnDB);
            await _contexto.SaveChangesAsync();
        }
    }

}