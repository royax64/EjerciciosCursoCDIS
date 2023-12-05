namespace Banco;

public class Cliente: Usuario{

    private char regimenFiscal {get; set;}

    public Cliente (int myID, string myName, string myEmail, decimal mySaldo, char myRegimen) : 
        base(myID, myName, myEmail, mySaldo){
        this.regimenFiscal = myRegimen;
    }

    public override void setSaldo(decimal mySaldo){
        base.setSaldo(mySaldo);

        if (regimenFiscal.Equals('M')){
            saldo += (mySaldo * 0.02m);
        }
    }

    public override string verInfo(){
        return base.verInfo() +
        $@"Regimen Fiscal: {this.regimenFiscal}";
    }
}