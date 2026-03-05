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
}    

