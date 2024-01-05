using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace BankAPI.Data.DTOs;

public class AdminDTO
{
    public string Correo { get; set; } = null!;

    public string Passwrd { get; set; } = null!;
}
