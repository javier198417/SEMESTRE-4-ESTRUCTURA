class Program
    {
        static void Main(string[] args)
        {
            // Crear un círculo con radio 5
            Circulo c = new Circulo(5);
            Console.WriteLine("Círculo:");
            Console.WriteLine("Área: " + c.CalcularArea());
            Console.WriteLine("Perímetro: " + c.CalcularPerimetro());

            // Crear un rectángulo con base 10 y altura 4
            Rectangulo r = new Rectangulo(10, 4);
            Console.WriteLine("\nRectángulo:");
            Console.WriteLine("Área: " + r.CalcularArea());
            Console.WriteLine("Perímetro: " + r.CalcularPerimetro());
        }
    }
