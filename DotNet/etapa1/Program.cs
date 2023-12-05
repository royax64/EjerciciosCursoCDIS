
/*
string msg = $"hola soy {name} y me apellido {surname}";
string file = @"~/Desktop"
string homedir = @$"/home/{user}/Desktop";
*/
int imprimirMenuPrincipal(){
    Console.Clear();
    string mainmenu = @"Te damos la bienvenida al Banco!
Seleccione una opción:
1) Ingresar la cantidad de retiros hechos por los usuarios
2) Revisar la cantidad entregada de billetes y monedas
3) Salir
Opción -> ";
    Console.Write(mainmenu);
    int op = 0;
    string? entrada = Console.ReadLine();
    int.TryParse(entrada, out op);
    return op;
}

#region retirosDeDinero
int[] fillArray(int numItems, int maxMoney){
    int[] arr = new int[numItems];
    for(int i = 0; i < arr.Length; i++){
        int retirado = 0;
        while(retirado <= 0 || retirado > maxMoney){
            Console.Write($"Ingrese el {i+1}° retiro. ");
            Console.Write($"Valor máximo ${maxMoney}: $");
            string? entrada = Console.ReadLine();
            int.TryParse(entrada, out retirado);
        }
        arr[i] = retirado;
    }
    return arr;
}

int[] setWithdrawals(){
    Console.Clear();
    int numRetiros = 0;
    while(numRetiros <= 0 || numRetiros > 10){
        Console.Write("Cantidad de retiros que se realizaron (máximo 10): ");
        string? entrada = Console.ReadLine();
        int.TryParse(entrada, out numRetiros);
    }
    int[] retiros = fillArray(numRetiros,50000);
    Console.Write("Ingrese Enter para continuar");
    Console.ReadKey();
    return retiros;
}
#endregion

#region verBilletesEntregados
int[] getMinBilletes(int numRetiro, int dinero){
    int[] BILLETES = {500, 200, 100, 50, 20, 10, 5, 1};
    int[] amountMoney = {0, 0};
    Console.Write($"Retiro #{numRetiro} = ${dinero} -> ");
    for (int i = 0; i < BILLETES.Length; i++ ){
        if (dinero % BILLETES[i] == dinero){
            continue;
        }else{
            int cantidadBillete = dinero / BILLETES[i]; 
            int resto = dinero - cantidadBillete*BILLETES[i];
            Console.Write($"{cantidadBillete}*${BILLETES[i]} ");
            dinero = resto;
            if (i >= 5) amountMoney[1] += cantidadBillete;
            else amountMoney[0] += cantidadBillete;
        }
    }
    return amountMoney;
}

void viewMoneyInside(int[] retiros){
    Console.Clear();
    int totalBilletes = 0;
    int totalMonedas = 0;
    for (int i = 0; i < retiros.Length ; i++){
        int[] cantidad = getMinBilletes(i+1, retiros[i]);
        Console.WriteLine($"\nBilletes entregados {cantidad[0]}");
        Console.WriteLine($"Monedas entregados {cantidad[1]}\n");
        totalBilletes += cantidad[0];
        totalMonedas += cantidad[1];
    }
    Console.WriteLine($"Se entregaron {totalBilletes} billetes y {totalMonedas} monedas.");
    Console.Write("Ingrese Enter para continuar");
    Console.ReadKey();
}
#endregion
 
#region MainMenu
int op = 0;
int[] retiros = {};
while(op != 3){
    op = imprimirMenuPrincipal();
    if (op > 3 || op <= 0){ //Validar
        Console.Write("Error: Ingrese opciones 1, 2, o 3...\nPresione Enter...");
        Console.ReadKey();
        continue;
    }else{
        switch(op){
            case 1:
                retiros = setWithdrawals();
                break;
            case 2:
                viewMoneyInside(retiros);
                break;
        }
    }
}

Console.Clear();
Console.WriteLine("Gracias por usar este programa, hasta pronto!");
#endregion
