using System;
using System.Collections.Generic;
using System.Linq;

namespace TorneoFutbol
{
    class Program
    {
        static void Main(string[] args)
        {
            Torneo torneo = new Torneo();
            bool salir = false;

            Console.WriteLine("======================================");
            Console.WriteLine("  SISTEMA DE REGISTRO DE TORNEO DE FÚTBOL");
            Console.WriteLine("======================================\n");

            while (!salir)
            {
                MostrarMenu();
                string? opcion = Console.ReadLine(); // Corregido: string? permite null

                switch (opcion)
                {
                    case "1":
                        RegistrarEquipo(torneo);
                        break;
                    case "2":
                        RegistrarJugador(torneo);
                        break;
                    case "3":
                        torneo.MostrarTodo();
                        break;
                    case "4":
                        ConsultarEquipo(torneo);
                        break;
                    case "5":
                        salir = true;
                        Console.WriteLine("\n¡Gracias por usar el sistema! Hasta luego.");
                        break;
                    default:
                        Console.WriteLine("\n❌ Opción no válida. Intente nuevamente.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        static void MostrarMenu()
        {
            Console.WriteLine("\n--- MENÚ PRINCIPAL ---");
            Console.WriteLine("1. Registrar nuevo equipo");
            Console.WriteLine("2. Registrar jugador en equipo");
            Console.WriteLine("3. Mostrar todos los equipos y jugadores");
            Console.WriteLine("4. Consultar equipo específico");
            Console.WriteLine("5. Salir");
            Console.Write("\nSeleccione una opción: ");
        }

        static void RegistrarEquipo(Torneo torneo)
        {
            Console.WriteLine("\n--- REGISTRO DE EQUIPO ---");
            
            Console.Write("Nombre del equipo: ");
            string? nombre = Console.ReadLine(); // Corregido
            
            Console.Write("Ciudad de origen: ");
            string? ciudad = Console.ReadLine(); // Corregido

            // Validación de null
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(ciudad))
            {
                Console.WriteLine("❌ El nombre y la ciudad son obligatorios.");
                return;
            }

            torneo.RegistrarEquipo(nombre, ciudad);
        }

        static void RegistrarJugador(Torneo torneo)
        {
            Console.WriteLine("\n--- REGISTRO DE JUGADOR ---");
            
            Console.Write("Nombre del equipo: ");
            string? equipoNombre = Console.ReadLine(); // Corregido

            if (string.IsNullOrWhiteSpace(equipoNombre))
            {
                Console.WriteLine("❌ Debe ingresar un nombre de equipo.");
                return;
            }

            Console.Write("Nombre del jugador: ");
            string? nombre = Console.ReadLine(); // Corregido
            
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ Debe ingresar un nombre de jugador.");
                return;
            }
            
            Console.Write("Edad del jugador: ");
            string? edadInput = Console.ReadLine(); // Corregido
            if (!int.TryParse(edadInput, out int edad))
            {
                Console.WriteLine("❌ Edad no válida.");
                return;
            }
            
            Console.Write("Posición (Delantero/Mediocampista/Defensa/Portero): ");
            string? posicion = Console.ReadLine(); // Corregido

            if (string.IsNullOrWhiteSpace(posicion))
            {
                posicion = "No especificada"; // Valor por defecto
            }

            Jugador jugador = new Jugador
            {
                Nombre = nombre,
                Edad = edad,
                Posicion = posicion
            };

            torneo.RegistrarJugador(equipoNombre, jugador);
        }

        static void ConsultarEquipo(Torneo torneo)
        {
            Console.WriteLine("\n--- CONSULTAR EQUIPO ---");
            Console.Write("Nombre del equipo a consultar: ");
            string? nombre = Console.ReadLine(); // Corregido
            
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ Debe ingresar un nombre de equipo.");
                return;
            }
            
            torneo.ConsultarEquipo(nombre);
        }
    }
}    