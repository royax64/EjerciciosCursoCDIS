using Banco;
/*

Cliente you = new Cliente(
    2,
    "You Mama",
    "you@gmail.com",
    1000.23M,
    'M'
);

//you.setSaldo(235);
Console.WriteLine(you.verInfo("Hola, como estás!"));
Guardar.añadirUsuarioArchivo(you);

Empleado dad = new Empleado(
    3,
    "Bingo",
    "bingoDad@gmail.com",
    3523570.1M,
    "IT"
);

//dad.setSaldo(8674);
Console.WriteLine(dad.verInfo("Empleado del mes!"));
Guardar.añadirUsuarioArchivo(dad);
*/

if (args.Length == 0){
   CorreoServicio.enviar();
}else {
    menu();
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
        op = imprimirMenuPrincipal();
        if (op > 3 || op <= 0){ //Validar
            Console.Write("Error: Ingrese opciones 1, 2, o 3...\nPresione Enter...");
            Console.ReadKey();
            continue;
        }else{
            switch(op){
                case 1:
                    break;
                case 2:
                    break;
            }
        }
    }
}
