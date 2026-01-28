using System;
using System.Collections.Generic;

public class GestionAtraccion
    {
        // Uso de la estructura lineal: COLA (Queue)
        private Queue<Persona> colaEspera = new Queue<Persona>();
        private const int TOTAL_ASIENTOS = 30; // Límite según la guía 

        public void RegistrarVisitante(string nombre)
        {
            if (colaEspera.Count < TOTAL_ASIENTOS)
            {
                int asientoAsignado = colaEspera.Count + 1;
                Persona nuevaPersona = new Persona(nombre, asientoAsignado);
                colaEspera.Enqueue(nuevaPersona);
                Console.WriteLine($"[EXITO] {nombre} registrado. Asiento: {asientoAsignado}");
            }
            else
            {
                Console.WriteLine("[ALERTA] No hay más asientos disponibles. La atracción está llena.");
            }
        }

      
        public void VerReporte()
        {
            Console.WriteLine("\n--- REPORTE DE OCUPACIÓN ---");
            if (colaEspera.Count == 0)
            {
                Console.WriteLine("La cola está vacía.");
                return;
            }

            foreach (var p in colaEspera)
            {
                Console.WriteLine($"Asiento #{p.NumeroAsiento.ToString("D2")} | Visitante: {p.Nombre}");
            }
            Console.WriteLine($"Total registrados: {colaEspera.Count} / {TOTAL_ASIENTOS}");
        }

        public void IniciarSimulacion()
        {
            if (colaEspera.Count == TOTAL_ASIENTOS)
            {
                Console.WriteLine("\n--- INICIANDO ATRACCIÓN (ORDEN DE LLEGADA) ---");
                while (colaEspera.Count > 0)
                {
                    Persona atendida = colaEspera.Dequeue();
                    Console.WriteLine($"Subiendo a la atracción: {atendida.Nombre} (Asiento {atendida.NumeroAsiento})");
                }
                Console.WriteLine("Simulación finalizada.");
            }
            else
            {
                Console.WriteLine($"\nAún faltan {TOTAL_ASIENTOS - colaEspera.Count} personas para iniciar.");
            }
        }
    }    
        