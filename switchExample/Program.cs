using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

System.Random r = new System.Random();
int totalJugador = 0, totalDealer = 0, platziCoins = 0, numeroJuego = 0, opcMenu;
//Generamos aleatoriamente el dinero del jugador entre 100 - 1000 USD
//r.NextDouble() * (500 - 100 + 100) + 100;
double dineroJugador = r.Next(100,500);
string controlOtraCarta = "", nombre = "Jack", deseaSeguirJugando = "";
bool puedeJugar = true, salirJuego = false;

int valida_opcion_menu()
{
    int opc;
    do
    {
        Console.WriteLine("Ingrese una opción del menú: ");
        opc = Convert.ToInt32(Console.ReadLine());
    } while (opc <= 0 || opc > 5);
    return opc;
}

//Logica para elegir la carta del jugador
void Elegir_Carta_Jugador() {
    int num;
    do {
        num = r.Next(1, 12);
        //Se suma el valor generado aleatoriamente al total del jugador
        totalJugador += num;
        //Muestra en consola el valor al jugador y el puntaje acumulado
        Console.WriteLine($"\nToma tu carta, jugador, \nTe salio el numero: {num}");
        Console.WriteLine($"Puntaje Actual Acumulado: {totalJugador}");
        //Si la suma de los valores del jugador es menor a 22 entonces pregunta si desea otra carta
        if (totalJugador < 22) {
            Console.WriteLine("¿Deseas otra carta? (si/no)");
            controlOtraCarta = Console.ReadLine();
        }

    } while (totalJugador < 22 && (controlOtraCarta == "Si" || 
                                   controlOtraCarta == "si" || 
                                   controlOtraCarta == "Sí"));

    //Genera un numero aleatorio para el dealer
    totalDealer = r.Next(12,23);
}

int valida_platzi_coins()
{
    int opc;
    do
    {
        Console.WriteLine("---- Tarifa PlatziCoins ----");
        Console.WriteLine("1 Platzicoin: 10USD");
        Console.WriteLine("10 Platzicoin: 90USD");
        Console.WriteLine("50 Platzicoin: 400USD");
        Console.WriteLine("NOTA: Recuerda que necesitas un platzicoin por ronda");
        Console.WriteLine("Cuántos PlatziCoins deseas comprar?: ");
        opc = Convert.ToInt32(Console.ReadLine()); //1,10,50
    } while (opc != 1 && opc != 10 && opc != 50);
    return opc;
}

void tarifa_logica_PlatziCoins()
{
    int costoPlatziCoins = 0;
    platziCoins = valida_platzi_coins();
    //Generamos el costo del usuario
    switch (platziCoins)
    {
        case 1:  costoPlatziCoins = 10; break;
        case 10: costoPlatziCoins = 90; break;
        case 50: costoPlatziCoins = 400; break;
    }

    if (dineroJugador < costoPlatziCoins) {
        Console.WriteLine("Monto de dinero insuficiente");
        puedeJugar = false;
    } else
    {
        dineroJugador -= costoPlatziCoins;
    }
}

void iniciarJuego(){
        //Reiniciamos los contadores en cada juego
        totalJugador = 0;
        totalDealer = 0;
        Console.WriteLine($"\n--- JUEGO {numeroJuego} ---");
        Elegir_Carta_Jugador();
        Console.WriteLine($"\nEl dealer tiene {totalDealer} !");
        Console.WriteLine($"Usted tiene {totalJugador} !\n");
        if (totalJugador > totalDealer && totalJugador < 22)
        {
            Console.WriteLine("Ganaste el juego, felicidades\n");
        }
        else if (totalJugador >= 22)
        {
            Console.WriteLine("Te pasaste de 21, perdiste el juego ctm\n");
            platziCoins--;
        }
        else if (totalJugador <= totalDealer)
        {
            Console.WriteLine("Perdiste el juego, lo sentimos\n");
            platziCoins--;
        }
        Console.WriteLine($"PlatziCoins: {platziCoins}");

}

void vender_fichas() {
    const int tarifa = 8;
    int cantFVend;
    //Generar una validacion
    if (platziCoins < 0)
    {
        Console.WriteLine("No cuenta con platzicoins");
    } else
    {
        do
        {
            Console.WriteLine("--- ¿Cuantos platziCoins deseas vender? ---");
            Console.WriteLine("--- Tarifa PlatziCoin: 8 USD ---");
            Console.WriteLine($"PlatziCoins: {platziCoins}");
            cantFVend = Convert.ToInt32(Console.ReadLine());
        } while (cantFVend < 0 || cantFVend > platziCoins);
        platziCoins -= cantFVend;
        if (platziCoins == 0) puedeJugar = false;
        dineroJugador += cantFVend * tarifa;
    }
}

//Muestra en consola el menu del juego
void menu()
{
    Console.WriteLine("----- Welcome to P L A T Z I N O -----\n");
    Console.WriteLine("1. Jugar");
    Console.WriteLine("2. Ver dinero actual");
    Console.WriteLine("3. Ver la cantidad de fichas");
    Console.WriteLine("4. Vender tus fichas");
    Console.WriteLine("5. Salir del juego");
    opcMenu = valida_opcion_menu();
    switch (opcMenu)
    {
        case 1:
            if (puedeJugar)
            {
                Console.WriteLine($"\n\nHola {nombre}, bienvenido a BlackJack!!");
                tarifa_logica_PlatziCoins();
                for (int i = 0; i < platziCoins; i++) {
                    //validacion
                    numeroJuego = i + 1;
                    iniciarJuego();
                    do
                    {
                        Console.WriteLine("¿Desea seguir jugando? (si/no)");
                        deseaSeguirJugando = Console.ReadLine().ToLower();
                    } while (deseaSeguirJugando != "si" && deseaSeguirJugando != "no");
                    if (deseaSeguirJugando == "no") break;
                }
            }
            else
                Console.WriteLine("Su monto de dinero es insuficiente, no puede jugar\n");
            break;
        case 2:
            Console.WriteLine($"Su dinero actual es {dineroJugador} USD\n");
            break;
        case 3:
            Console.WriteLine($"Usted cuenta con {platziCoins}" + 
                (platziCoins == 1 ? " platziCoin\n" : " platziCoins\n"));
            break;
        case 4:
            vender_fichas();
            break;
        case 5:
            Console.WriteLine("Gracias por jugar!\n");
            salirJuego = true;
            break;
    }
}

//Ejecucion principal
while (true) {
    if(!salirJuego){
        menu();
    } else {
        break;
    }
    
}
