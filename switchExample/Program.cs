using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;

int totalJugador = 0;
int totalDealer = 0;
int platziCoins = 0;
string controlOtraCarta = "";
string switchControl = "menu";
System.Random r = new System.Random();
void validar_Carta()
{
    int num;
    do
    {
        num = r.Next(1, 12);
        totalJugador += num;
        Console.WriteLine($"\nToma tu carta, jugador, \nTe salio el numero: {num}");
        Console.WriteLine($"Puntaje Actual Acumulado: {totalJugador}");
        if (totalJugador < 22)
        {
            Console.WriteLine("¿Deseas otra carta?");
            controlOtraCarta = Console.ReadLine();
        }

    } while (totalJugador < 22 && (controlOtraCarta == "Si" || 
                                   controlOtraCarta == "si" || 
                                   controlOtraCarta == "Sí"));

    totalDealer = r.Next(12,23);
}

while (true) {
    Console.WriteLine("---- Welcome al P L A T Z I N O ----");
    Console.WriteLine("Cuántos PlatziCoins deseas? \n" + 
        "Ingresa un numero entero\n" +
        "Recuerda que necesitas una por ronda.");
    platziCoins = Convert.ToInt32( Console.ReadLine() );

    //Segun la cantidad de fichas se jugara una cierta cantidad de veces
    for(int i = 0; i < platziCoins; i++) { 
    //Reiniciamos los contadores en cada juego
    totalJugador = 0;
    totalDealer = 0;
    switch (switchControl)
    {
        case "menu":
            Console.WriteLine("Escriba '21' para jugar al 21");
            switchControl = Console.ReadLine();
            i--;
            break;

        case "21":
            validar_Carta();
            Console.WriteLine($"\nEl dealer tiene {totalDealer} !");
            Console.WriteLine($"Usted tiene {totalJugador} !\n");
            if (totalJugador > totalDealer && totalJugador < 22)
            {
                Console.WriteLine("Ganaste el juego, felicidades\n");
            }
            else if (totalJugador >= 22)
            {
                Console.WriteLine("Te pasaste de 21, perdiste el juego ctm\n");
            }
            else if (totalJugador <= totalDealer)
            {
                Console.WriteLine("Perdiste el juego, lo sentimos\n");
            }
            switchControl = "menu";
            break;

        default:
            Console.WriteLine("Valor ingresado no válido en el c a s i n o\n");
            switchControl = "menu";
            break;
    }
    }
}
