public class Estudiante
{
 public int ID { get; set; }
 public string Nombres { get; set; }
 public string Apellidos { get; set; }
 public string Direccion { get; set; }
 public string[] Telefonos { get; set; } = new string[3];
}

