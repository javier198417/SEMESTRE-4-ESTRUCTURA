public class Equipo
    {
        public string Nombre { get; set; } = string.Empty; // Corregido: inicializado
        public string Ciudad { get; set; } = string.Empty; // Corregido: inicializado
        public HashSet<Jugador> Jugadores { get; set; }

        public Equipo()
        {
            Jugadores = new HashSet<Jugador>();
        }

        public void AgregarJugador(Jugador jugador)
        {
            if (jugador == null) return; // Validación de null
            
            if (Jugadores.Add(jugador))
            {
                Console.WriteLine($"✅ Jugador {jugador.Nombre} registrado exitosamente en {Nombre}");
            }
            else
            {
                Console.WriteLine($"⚠️ El jugador {jugador.Nombre} ya está registrado en el equipo {Nombre}");
            }
        }

        public void ListarJugadores()
        {
            Console.WriteLine($"\n📋 Equipo: {Nombre} (Ciudad: {Ciudad})");
            Console.WriteLine($"Total de jugadores: {Jugadores.Count}");
            
            if (Jugadores.Count == 0)
            {
                Console.WriteLine("   No hay jugadores registrados en este equipo.");
            }
            else
            {
                int contador = 1;
                foreach (var jugador in Jugadores.OrderBy(j => j.Nombre))
                {
                    Console.WriteLine($"   {contador++}. {jugador}");
                }
            }
        }
    }