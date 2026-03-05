public class Rectangulo
    {
        // Atributos privados: base y altura del rectángulo
        private double baseRectangulo;
        private double altura;

        // Constructor: inicializa base y altura
        public Rectangulo(double baseRectangulo, double altura)
        {
            this.baseRectangulo = baseRectangulo;
            this.altura = altura;
        }

        // Método para calcular el área del rectángulo
        // Fórmula: base * altura
        public double CalcularArea()
        {
            return baseRectangulo * altura;
        }

        // Método para calcular el perímetro del rectángulo
        // Fórmula: 2 * (base + altura)
        public double CalcularPerimetro()
        {
            return 2 * (baseRectangulo + altura);
        }
    }