using System;

namespace ClinicaTurnos
{
    // Clase que representa el Registro del Paciente
    public class Paciente
    {
        public string Nombre { get; set; }
        public string ID { get; set; }
        public string Especialidad { get; set; }

        public override string ToString()
        {
            return $"ID: {ID} | Paciente: {Nombre} | Especialidad: {Especialidad}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ingrese la capacidad máxima de la clínica hoy: ");
            int capacidad = int.Parse(Console.ReadLine());
            Paciente[] agenda = new Paciente[capacidad];
            int contador = 0;

            // Creamos la gestión con una capacidad de 10 turnos
            GestionTurnos miClinica = new GestionTurnos(10);

           // Ejemplo de registro
           miClinica.RegistrarPaciente("Juan Pérez", "1725443322", "Cardiología");
           miClinica.RegistrarPaciente("Ana López", "0911223344", "Pediatría");

          // Mostrar el reporte
           miClinica.MostrarReporte();

            int opcion;
            do
            {
                Console.WriteLine("\n--- SISTEMA DE TURNOS CLÍNICA ---");
                Console.WriteLine("1. Registrar Paciente");
                Console.WriteLine("2. Ver Reporte de Turnos");
                Console.WriteLine("3. Salir");
                opcion = int.Parse(Console.ReadLine());

                if (opcion == 1 && contador < capacidad)
                {
                    Paciente nuevo = new Paciente();
                    Console.Write("Nombre: "); nuevo.Nombre = Console.ReadLine();
                    Console.Write("ID: "); nuevo.ID = Console.ReadLine();
                    Console.Write("Especialidad: "); nuevo.Especialidad = Console.ReadLine();
                    agenda[contador] = nuevo;
                    contador++;
                }
                else if (opcion == 2)
                {
                    Console.WriteLine("\n--- LISTADO DE TURNOS ---");
                    for (int i = 0; i < contador; i++)
                    {
                        Console.WriteLine($"{i + 1}. {agenda[i].ToString()}");
                    }
                }
            } while (opcion != 3);

        }
    }
}
