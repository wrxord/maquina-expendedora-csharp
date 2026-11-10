
using System;

namespace MaquinaExpendedora
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1. DEFINICIÓN DE VARIABLES E INVENTARIO
            string[] nombresSodas = {
                "Limonada con chia",
                "Horchata",
                "Te de manzana",
                "Agua de jamaica",
                "Agua natural"
            };

            double[] preciosSodas = {
                20.00, 18.00, 16.50, 15.00, 12.00
            };

            int[] stockSodas = { 5, 5, 5, 3, 10 };

            double saldoCliente = 0.0;
            bool salir = false;

            Console.WriteLine("========================================");
            Console.WriteLine("   BIENVENIDO A LA MAQUINA EXPENDEDORA  ");
            Console.WriteLine("========================================");

            // 2. BUCLE PRINCIPAL
            while (!salir)
            {
                Console.WriteLine($"\nSaldo actual: ${saldoCliente:F2}");
                MostrarProductos(nombresSodas, preciosSodas, stockSodas);

                Console.WriteLine("\n[1-5] Seleccionar bebida | [6] Ingresar dinero | [0] Salir");
                Console.Write("Opcion: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                        int indice = int.Parse(opcion) - 1;

                        ComprarSoda(indice, nombresSodas, preciosSodas,
                            stockSodas, ref saldoCliente);
                        break;

                    case "6":
                        Console.Write("Ingresa la cantidad de dinero: $");

                        if (double.TryParse(Console.ReadLine(), out double ingreso)
                            && ingreso > 0)
                        {
                            saldoCliente += ingreso;
                            Console.WriteLine($"Has ingresado ${ingreso:F2}");
                        }
                        else
                        {
                            Console.WriteLine("ERROR: Monto invalido.");
                        }
                        break;

                    case "0":
                        salir = true;
                        break;

                    default:
                        Console.WriteLine("Opcion no valida. Intenta de nuevo.");
                        break;
                }
            }

            Console.WriteLine("Gracias por utilizar nuestra maquina. ¡Vuelva pronto!");
        }

        // FUNCION PARA MOSTRAR LOS PRODUCTOS
        static void MostrarProductos(string[] nombres, double[] precios, int[] stock)
        {
            Console.WriteLine("\n--- CATALOGO DE BEBIDAS ---");

            for (int i = 0; i < nombres.Length; i++)
            {
                string estado = stock[i] > 0
                    ? $"${precios[i]:F2}"
                    : "AGOTADO";

                Console.WriteLine($"[{i + 1}] {nombres[i].PadRight(20)} - {estado}");
            }
        }

        // FUNCION PARA REALIZAR UNA COMPRA
        static void ComprarSoda(int indice, string[] nombres,
            double[] precios, int[] stock, ref double saldo)
        {
            // VALIDAR QUE EL PRODUCTO EXISTA
            if (indice < 0 || indice >= nombres.Length)
                return;

            // VALIDAR EXISTENCIAS
            if (stock[indice] == 0)
            {
                Console.WriteLine($"\nLo siento, {nombres[indice]} esta agotado.");
                return;
            }

            // VALIDAR SALDO
            if (saldo < precios[indice])
            {
                Console.WriteLine($"\nSaldo insuficiente. El precio es ${precios[indice]:F2} y tienes ${saldo:F2}.");
                return;
            }

            // REALIZAR COMPRA
            saldo -= precios[indice];
            stock[indice]--;

            Console.WriteLine($"\n¡CLICK! Has comprado: {nombres[indice]}.");
            Console.WriteLine("Tu bebida esta lista. ¡Disfrutala!");
        }
    }
}
