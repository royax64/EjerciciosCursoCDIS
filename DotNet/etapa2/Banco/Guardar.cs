namespace Banco;
using Newtonsoft.Json;

public static class Guardar{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\usuarios.json"; 

    public static void añadirUsuarioArchivo(Usuario usr){
        string usrInFile = "";
        var listaUsuarios = new List<Usuario>();

        if (File.Exists(filePath)) {
            usrInFile = File.ReadAllText(filePath);
            listaUsuarios = JsonConvert.DeserializeObject<List<Usuario>>(usrInFile);
        }
        
        listaUsuarios!.Add(usr);
        
        string json = JsonConvert.SerializeObject(listaUsuarios);
        File.WriteAllText(filePath, json);
    }


}
