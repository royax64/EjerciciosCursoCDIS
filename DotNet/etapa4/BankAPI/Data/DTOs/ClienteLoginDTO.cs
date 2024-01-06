using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.DTOs;

public class ClienteLoginDTO
{
    public string Correo { get; set; } = null!;

    public string Passwd { get; set; } = null!;
}
