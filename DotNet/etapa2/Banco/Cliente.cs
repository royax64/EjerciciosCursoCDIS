namespace Banco;
using Newtonsoft.Json;

public class Cliente: Usuario, Personable{

    [JsonProperty]
    private char regimenFiscal {get; set;}

    public Cliente () {}

    public Cliente (int myID, string myName, string myEmail, decimal mySaldo, char myRegimen) : 
        base(myID, myName, myEmail, mySaldo){
        this.regimenFiscal = myRegimen;
        setSaldo(mySaldo);
    }

    public override void setSaldo(decimal mySaldo){
        base.setSaldo(mySaldo);

        if (regimenFiscal.Equals('M')){
            saldo += (mySaldo * 0.02m);
        }
    }

    public override string verInfo(){
        return base.verInfo() + @$"
        Regimen Fiscal: {this.regimenFiscal}";
    }

    public new string verInfo(string saludo){
        return base.verInfo(saludo) + @$"
        Regimen Fiscal: {this.regimenFiscal}";
    }

    public string getNombre(){
        return $"El cliente preferido {nombre}";
    }

    public string getPais(){
        return "México";
    }
}