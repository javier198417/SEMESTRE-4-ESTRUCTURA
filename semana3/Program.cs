class Program
{
 static void Main()
 {
 Estudiante est = new Estudiante();
 Console.Write("Ingrese ID: ");
 est.ID = int.Parse(Console.ReadLine());
 Console.Write("Ingrese nombres: ");
 est.Nombres = Console.ReadLine();
 Console.Write("Ingrese apellidos: ");
 est.Apellidos = Console.ReadLine();
 Console.Write("Ingrese dirección: ");
 est.Direccion = Console.ReadLine();
 for (int i = 0; i < 3; i++)
 {
 Console.Write($"Ingrese teléfono {i+1}: ");
 est.Telefonos[i] = Console.ReadLine();
 }
 Console.WriteLine("\n--- Datos Registrados ---");
 Console.WriteLine($"ID: {est.ID}");
 Console.WriteLine($"Nombres: {est.Nombres}");
 Console.WriteLine($"Apellidos: {est.Apellidos}");
 Console.WriteLine($"Dirección: {est.Direccion}");
 Console.WriteLine("Teléfonos:");
 foreach (var tel in est.Telefonos) Console.WriteLine(tel);
 }
}