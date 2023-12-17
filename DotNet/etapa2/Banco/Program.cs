using Banco;
using System.Text.RegularExpressions;

if (args.Length == 0){
    Console.Clear();
    Console.WriteLine("Enviando correo con los usuarios registrados hoy...");
    CorreoServicio.enviar();
} else {
    menu();
    Console.Clear();
    Console.Write("Gracias por usar este programa...");
}

int imprimirMenuPrincipal(){
    Console.Clear();
    string mainmenu = @"Seleccione una opción:
1) Crear un usuario nuevo
2) Eliminar un usuario existente
3) Salir
Opción -> ";
    Console.Write(mainmenu);
    int op = 0;
    string? entrada = Console.ReadLine();
    int.TryParse(entrada, out op);
    return op;
}

void menu(){
    int op = 0;
    while(op != 3){
	List<Usuario> listaUsuarios = Almacenaje.getAllUsuarios();
        op = imprimirMenuPrincipal();
        if (op > 3 || op <= 0){ //Validar
            Console.Write("Error: Ingrese opciones 1, 2, o 3...\nPresione Enter...");
            Console.ReadKey();
            continue;
        }else{
            switch(op){
                case 1:
                    crearUsuario(listaUsuarios);
                    break;
                case 2:
                    eliminarUsuario(listaUsuarios);
                    break;
            }
        }
    }
}

void crearUsuario(List<Usuario> listaUsuarios) {
    int id = 0;
    string nombre = "";
    string correo = "";
    decimal saldo = 0M;
    char tipo = 'e';
    Usuario nuevoUsuario;
    
    Console.Clear();
    Console.WriteLine("Llena el siguiente formulario: ");

    /********************VALIDANDO ID******************/
    do {
        Console.Write("ID: ");
        int.TryParse(Console.ReadLine(), out id);
        var maxID = listaUsuarios.Where(u => u.getID() == id).ToList();

        if (id <= 0) {
            Console.WriteLine("Error: ID no válido.");
            continue;
        }
        if (maxID.Any()) {
            Console.WriteLine("Error: ID ya existe.");
            continue;
        }
        break;     
    } while (true);

    /********************VALIDANDO NOMBRE******************/
    do {
        Console.Write("Nombre: ");
        nombre = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(nombre)){
            Console.WriteLine("Error: Nombre vacío.");
        }
    } while (String.IsNullOrWhiteSpace(nombre));

    /********************VALIDANDO CORREO******************/
    do {
        Console.Write("Correo: ");
        correo = Console.ReadLine();
        string regexEmail = @"^[\w\-\.]+@([\w-]+\.)+[\w-]{2,}$";
        MatchCollection coincidencias = Regex.Matches(correo, regexEmail, RegexOptions.IgnoreCase);

        if (!coincidencias.Any()){
            Console.WriteLine("Error: Correo invalido.");
            continue;
        }
        break;
    } while (true);

    /********************VALIDANDO SALDO******************/
    do {
        Console.Write("Saldo: ");
        try{
            saldo = Decimal.Parse(Console.ReadLine());
            if (saldo < 0) {
                Console.WriteLine("Error: Saldo menor a cero."); 
                continue;
            }
            break;
        }catch (Exception e){
            Console.WriteLine("Error: Saldo no válido.");
            continue;
        }
    } while (true);

    /********************VALIDANDO TIPO******************/
    do {
        Console.Write("Tipo de usuario ('e' -> Empleado o 'c' para cliente): ");
        char.TryParse(Console.ReadLine().ToLower(), out tipo);
        if (tipo != 'c' && tipo != 'e'){
            Console.WriteLine("Error: Tipo de usuario inválido.");
        }
    } while (tipo != 'c' && tipo != 'e');

    /***************VALIDANDO OTROS ATRIBUTOS*************/
    if (tipo.Equals('c')){
        char regimenFiscal;
        
        do {
            Console.Write("Regimen Fiscal: ");
            char.TryParse(Console.ReadLine().ToLower().Trim(), out regimenFiscal);
            if (regimenFiscal == '\0'){
                Console.WriteLine("Error: Regimen Fiscal vacío");
            }
        } while (regimenFiscal == '\0');

        nuevoUsuario = new Cliente(id, nombre, correo, saldo, regimenFiscal);
    
    } else {
        string departamento;
        
        do {
            Console.Write("Departamento: ");
            departamento = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(departamento)){
                Console.WriteLine("Error: Departamento vacío");
            }
        } while (String.IsNullOrWhiteSpace(departamento)); 

        nuevoUsuario = new Empleado(id, nombre, correo, saldo, departamento);
    }

    Almacenaje.añadirUsuarioArchivo(nuevoUsuario);
    Console.WriteLine("Tu nuevo usuario\n" + nuevoUsuario.verInfo());
    Console.Write("Presione cualquier botón para voler al menú");
    Console.ReadKey();
}

void eliminarUsuario(List<Usuario> listaUsuarios) {
    int id = 0;
    Console.Clear();

    do {
        Console.Write("Ingrese el ID del usuario a eliminar: ");
        int.TryParse(Console.ReadLine(), out id);
        var maxID = listaUsuarios.Where(u => u.getID() == id).ToList();

        if (id <= 0) {
            Console.WriteLine("Error: ID no válido.");
            continue;
        }
        if (!maxID.Any()) {
            Console.WriteLine("Error: ID no existe.");
            continue;
        }
        
        break;
    } while (true);

    bool res = Almacenaje.eliminarUsuarioArchivo(id);
    if (res)
        Console.WriteLine($"Se ha eliminado el usuario {id}.");
    else   
        Console.WriteLine($"No se pudo eliminar el usuario {id}. ¿Haz registrado a algún usuario?");

    Console.Write("Presione cualquier botón para voler al menú");
    Console.ReadKey();
}
