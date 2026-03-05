namespace PROYECTOS;

public class GestiondeTurnos

{
    // Clase que gestiona la lógica de negocio
    public class GestionTurnos
    {
        // Atributos privados (Encapsulamiento)
        private Paciente[] listaPacientes;
        private int contador;
        private int capacidad;

        // Constructor: Inicializa la estructura de datos
        public GestionTurnos(int tamaño)
        {
            this.capacidad = tamaño;
            this.listaPacientes = new Paciente[tamaño];
            this.contador = 0;
        }

        // Método para agregar un paciente al vector
        public bool RegistrarPaciente(string nombre, string id, string especialidad)
        {
            if (contador < capacidad)
            {
                Paciente nuevoPaciente = new Paciente
                {
                    Nombre = nombre,
                    ID = id,
                    Especialidad = especialidad
                };

                listaPacientes[contador] = nuevoPaciente;
                contador++;
                return true; // Registro exitoso
            }
            return false; // Clínica llena
        }

        // Método de Reportería: Retorna el vector o imprime los datos
        public void MostrarReporte()
        {
            if (contador == 0)
            {
                Console.WriteLine("No hay turnos registrados actualmente.");
                return;
            }

            Console.WriteLine("\n============================================");
            Console.WriteLine("       REPORTE DE TURNOS AGENDADOS");
            Console.WriteLine("============================================");
            Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-15}", "No.", "ID", "Nombre", "Especialidad");
            
            for (int i = 0; i < contador; i++)
            {
                Console.WriteLine("{0,-5} {1,-15} {2,-15} {3,-15}", 
                    (i + 1), 
                    listaPacientes[i].ID, 
                    listaPacientes[i].Nombre, 
                    listaPacientes[i].Especialidad);
            }
            Console.WriteLine("============================================\n");
        }

        // Método para consultar un paciente específico por ID
        public Paciente BuscarPaciente(string idBuscado)
        {
            for (int i = 0; i < contador; i++)
            {
                if (listaPacientes[i].ID == idBuscado)
                {
                    return listaPacientes[i];
                }
            }
            return null; // No encontrado
        }
    }
}  

