namespace Banco;

public class Empleado: Usuario{

    private string departamento {get; set;}

    public Empleado (int myID, string myName, string myEmail, decimal mySaldo, string myDepartamento) : 
        base(myID, myName, myEmail, mySaldo){
        this.departamento = myDepartamento;
    }

    public override void setSaldo(decimal mySaldo){
        base.setSaldo(mySaldo);

        if(string.IsNullOrEmpty(departamento)) return;
        
        if (departamento.Equals("IT")){
            saldo += (mySaldo * 0.05m);
        }
    }

    public override string verInfo(){
        return base.verInfo() +
        $@"Departamento: {this.departamento}";
    }
}