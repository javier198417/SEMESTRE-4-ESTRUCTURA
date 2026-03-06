  public class Jugador
    {
        public string Nombre { get; set; } = string.Empty; // Corregido: inicializado
        public int Edad { get; set; }
        public string Posicion { get; set; } = string.Empty; // Corregido: inicializado

        public override bool Equals(object? obj) // Corregido: object? permite null
        {
            if (obj is Jugador jugador)
                return this.Nombre.ToLower() == jugador.Nombre.ToLower();
            return false;
        }

        public override int GetHashCode()
        {
            return Nombre.ToLower().GetHashCode();
        }

        public override string ToString()
        {
            return $"{Nombre}, {Edad} años - {Posicion}";
        }
    }