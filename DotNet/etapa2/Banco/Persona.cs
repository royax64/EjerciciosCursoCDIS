namespace Banco;

public abstract class Persona{
    public abstract string getNombre();

    public string getPais(){
        return "México";
    }
}

public interface Personable{
    string getNombre();
    string getPais();
}