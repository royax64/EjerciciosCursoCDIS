using Banco;

Usuario me = new Usuario(
    1,
    "Roy", 
    "roy@gmail.com", 
    1324.23M);

me.setSaldo(-34);
Console.WriteLine(me.verInfo());
Guardar.añadirUsuarioArchivo(me);

Cliente you = new Cliente(
    2,
    "You Mama",
    "you@gmail.com",
    1000.23M,
    'M'
);

you.setSaldo(235);
Console.WriteLine(you.verInfo("Hola, como estás!"));
Guardar.añadirUsuarioArchivo(you);

Empleado dad = new Empleado(
    3,
    "Bingo",
    "bingoDad@gmail.com",
    3523570.1M,
    "IT"
);

dad.setSaldo(8674);
Console.WriteLine(dad.verInfo("Empleado del mes!"));
Guardar.añadirUsuarioArchivo(dad);
