namespace Banco;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public static class Guardar{
    static string filePath = AppDomain.CurrentDomain.BaseDirectory + @"\usuarios.json"; 
  
    public static void añadirUsuarioArchivo(object usr){
        string usrInFile = "";
        var listaUsuarios = new List<object>();

        if (File.Exists(filePath)) {
            usrInFile = File.ReadAllText(filePath);
            listaUsuarios = JsonConvert.DeserializeObject<List<object>>(usrInFile);
        }

        listaUsuarios!.Add(usr);
        
        JsonSerializerSettings config = new JsonSerializerSettings { Formatting = Formatting.Indented };
        string json = JsonConvert.SerializeObject(listaUsuarios, config);
        Console.WriteLine(json);
        File.WriteAllText(filePath, json);
    }

    public static List<Usuario> getAllUsuarios(){
        string usrInFile = "";
        var listaUsuarios = new List<Usuario>();

        if (File.Exists(filePath)) {
            usrInFile = File.ReadAllText(filePath);
        }

        var listaObjetos = JsonConvert.DeserializeObject<List<object>>(usrInFile);

        if (listaObjetos == null){
            return listaUsuarios;
        }

        foreach (object o in listaObjetos){
            Usuario u;
            JObject usr = (JObject)o;

            if (usr.ContainsKey("regimenFiscal")) u = usr.ToObject<Cliente>();
            else u = usr.ToObject<Empleado>();

            listaUsuarios.Add(u);
        }

        return listaUsuarios;
    }

    public static List<Usuario> getNuevosUsuarios(){
        var listaUsuarios = Guardar.getAllUsuarios();
        var usuariosDeHoy = listaUsuarios.Where(
            usr => usr.getFechaRegistro().Date.Equals(DateTime.Today)
            ).ToList();
        
        return usuariosDeHoy;
    }


}
