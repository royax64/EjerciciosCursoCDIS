namespace Banco;
using Newtonsoft.Json;

public class Empleado: Usuario, Personable{

    [JsonProperty]
    private string departamento {get; set;}

    public Empleado () {}

    public Empleado (int myID, string myName, string myEmail, decimal mySaldo, string myDepartamento) : 
        base(myID, myName, myEmail, mySaldo){
        this.departamento = myDepartamento;
        setSaldo(mySaldo);
    }

    public override void setSaldo(decimal mySaldo){
        base.setSaldo(mySaldo);
        
        if (departamento.Equals("IT")){
            saldo += (mySaldo * 0.05m);
        }
    }

    public override string verInfo(){
        return base.verInfo() + @$"
        Departamento: {this.departamento}";
    }

    public new string verInfo(string saludo){
        return base.verInfo(saludo) + @$"
        Departamento: {this.departamento}";
    }

    public string getNombre(){
        return $"Empleado {nombre}";
    }

    public string getPais(){
        return $"CTRY: MX";
    }
}