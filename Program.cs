using System;
using System.Collections.Generic;
class Program
    {
        static void Main(string[] args)
        {
            GestionAtraccion parque = new GestionAtraccion();
            
            // Simulación de ingreso de personas
            Console.WriteLine("Registrando visitantes...");
            for (int i = 1; i <= 30; i++)
            {
                parque.RegistrarVisitante("Visitante_" + i);
            }

            // Mostrar Reporte
            parque.VerReporte();

            // Ejecutar orden de salida (FIFO)
            parque.IniciarSimulacion();

            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }
    }
