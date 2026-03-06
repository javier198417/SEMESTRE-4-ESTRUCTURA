     public class Torneo
    {
        private Dictionary<string, Equipo> equipos;

        public Torneo()
        {
            equipos = new Dictionary<string, Equipo>(StringComparer.OrdinalIgnoreCase);
        }

        public void RegistrarEquipo(string nombre, string ciudad)
        {
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(ciudad))
            {
                Console.WriteLine("❌ El nombre y la ciudad son obligatorios.");
                return;
            }

            if (!equipos.ContainsKey(nombre))
            {
                equipos[nombre] = new Equipo { Nombre = nombre, Ciudad = ciudad };
                Console.WriteLine($"✅ Equipo {nombre} registrado exitosamente.");
            }
            else
            {
                Console.WriteLine($"⚠️ El equipo {nombre} ya existe en el torneo.");
            }
        }

        public void RegistrarJugador(string equipoNombre, Jugador jugador)
        {
            if (string.IsNullOrWhiteSpace(equipoNombre) || jugador == null || string.IsNullOrWhiteSpace(jugador.Nombre))
            {
                Console.WriteLine("❌ Datos incompletos para registrar el jugador.");
                return;
            }

            if (equipos.ContainsKey(equipoNombre))
            {
                equipos[equipoNombre].AgregarJugador(jugador);
            }
            else
            {
                Console.WriteLine($"❌ No se encontró el equipo: {equipoNombre}");
                Console.WriteLine("   Equipos disponibles:");
                foreach (var equipo in equipos.Keys)
                {
                    Console.WriteLine($"   - {equipo}");
                }
            }
        }

        public void MostrarTodo()
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("     LISTADO COMPLETO DEL TORNEO");
            Console.WriteLine("======================================");

            if (equipos.Count == 0)
            {
                Console.WriteLine("\nNo hay equipos registrados en el torneo.");
                return;
            }

            Console.WriteLine($"\nTotal de equipos: {equipos.Count}");
            
            int totalJugadores = 0;
            foreach (var equipo in equipos.Values.OrderBy(e => e.Nombre))
            {
                equipo.ListarJugadores();
                totalJugadores += equipo.Jugadores.Count;
            }
            
            Console.WriteLine($"\n📊 RESUMEN: {equipos.Count} equipos | {totalJugadores} jugadores");
        }

        public void ConsultarEquipo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("❌ Debe ingresar un nombre de equipo.");
                return;
            }

            if (equipos.ContainsKey(nombre))
            {
                equipos[nombre].ListarJugadores();
                
                // Estadísticas adicionales
                var equipo = equipos[nombre];
                Console.WriteLine($"\n📊 Estadísticas de {nombre}:");
                Console.WriteLine($"   Total jugadores: {equipo.Jugadores.Count}");
                
                // Conteo por posición
                var posiciones = equipo.Jugadores
                    .GroupBy(j => j.Posicion)
                    .Select(g => new { Posicion = g.Key, Cantidad = g.Count() });
                
                foreach (var item in posiciones)
                {
                    Console.WriteLine($"   {item.Posicion}: {item.Cantidad} jugador(es)");
                }
            }
            else
            {
                Console.WriteLine($"❌ No se encontró el equipo: {nombre}");
            }
        }
    }

