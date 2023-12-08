namespace Banco;
using Newtonsoft.Json;

public class Usuario {
    [JsonProperty]
    protected int id {get; set;}
    
    [JsonProperty]
    protected string? nombre {get; set;}
    
    [JsonProperty]
    protected string? email {get; set;}
    
    [JsonProperty]
    protected DateTime fechaRegistro {get; set;}
    
    [JsonProperty]
    protected decimal saldo {get; set;}

    public Usuario() {}

    public Usuario(int myID, string myName, string myEmail, decimal mySaldo){
        this.id = myID;
        this.nombre = myName;
        this.email = myEmail;
        this.fechaRegistro = DateTime.Now; 
    }

    public DateTime getFechaRegistro(){
        return fechaRegistro;
    }

    public virtual string verInfo(){
        return @$"
        ID: {this.id}
        Nombre: {this.nombre}
        Email: {this.email}
        Fecha de registro: {this.fechaRegistro.ToShortDateString()}
        Saldo: {this.saldo}";
    }

    public string verInfo(string saludo){
        return @$"
        {saludo}
        ID: {this.id}
        Nombre: {this.nombre}
        Email: {this.email}
        Fecha de registro: {this.fechaRegistro}
        Saldo: {this.saldo}";
    }

    public virtual void setSaldo(decimal mySaldo){
        if (mySaldo > 0) this.saldo += mySaldo;
    }

    public int getID(){
        return id;
    }
}

