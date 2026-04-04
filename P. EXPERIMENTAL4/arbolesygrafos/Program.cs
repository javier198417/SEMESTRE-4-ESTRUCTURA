#nullable disable

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VuelosBaratos
{
    class Vuelo
    {
        public string Destino { get; set; }
        public int Costo { get; set; }
        public Vuelo(string destino, int costo) { Destino = destino; Costo = costo; }
    }

    class GrafoVuelos
    {
        private Dictionary<string, List<Vuelo>> adyacencias = new();

        public void AgregarVuelo(string origen, string destino, int costo)
        {
            if (!adyacencias.ContainsKey(origen))
                adyacencias[origen] = new List<Vuelo>();
            adyacencias[origen].Add(new Vuelo(destino, costo));
        }

        public List<string> ObtenerAeropuertos()
        {
            var aeropuertos = new HashSet<string>();
            foreach (var origen in adyacencias.Keys)
            {
                aeropuertos.Add(origen);
                foreach (var vuelo in adyacencias[origen])
                    aeropuertos.Add(vuelo.Destino);
            }
            return aeropuertos.ToList();
        }

        public void MostrarTodosLosVuelos()
        {
            Console.WriteLine("\n--- Lista de vuelos registrados ---");
            foreach (var origen in adyacencias.Keys)
                foreach (var vuelo in adyacencias[origen])
                    Console.WriteLine($"{origen} -> {vuelo.Destino} : ${vuelo.Costo}");
        }

        public (int costo, List<string> ruta) ObtenerRutaMasBarata(string inicio, string fin)
        {
            if (!adyacencias.ContainsKey(inicio))
                return (int.MaxValue, null);

            var distancias = new Dictionary<string, int>();
            var previos = new Dictionary<string, string>();
            var pq = new PriorityQueue<string, int>();

            foreach (var aeropuerto in ObtenerAeropuertos())
            {
                distancias[aeropuerto] = int.MaxValue;
                previos[aeropuerto] = null;
            }
            distancias[inicio] = 0;
            pq.Enqueue(inicio, 0);

            while (pq.Count > 0)
            {
                string actual = pq.Dequeue();
                if (actual == fin) break;
                if (!adyacencias.ContainsKey(actual)) continue;

                foreach (var vuelo in adyacencias[actual])
                {
                    int nuevaDist = distancias[actual] + vuelo.Costo;
                    if (nuevaDist < distancias[vuelo.Destino])
                    {
                        distancias[vuelo.Destino] = nuevaDist;
                        previos[vuelo.Destino] = actual;
                        pq.Enqueue(vuelo.Destino, nuevaDist);
                    }
                }
            }

            if (distancias[fin] == int.MaxValue)
                return (int.MaxValue, null);

            List<string> ruta = new List<string>();
            string actualRuta = fin;
            while (actualRuta != null)
            {
                ruta.Insert(0, actualRuta);
                actualRuta = previos[actualRuta];
            }
            return (distancias[fin], ruta);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            GrafoVuelos grafo = new GrafoVuelos();
            CargarDatosDesdeArchivo(grafo, "vuelos.txt");

            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE BÚSQUEDA DE VUELOS BARATOS ===");
                Console.WriteLine("1. Mostrar todos los aeropuertos");
                Console.WriteLine("2. Mostrar todos los vuelos");
                Console.WriteLine("3. Buscar ruta más barata");
                Console.WriteLine("4. Salir");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        var aeropuertos = grafo.ObtenerAeropuertos();
                        Console.WriteLine("\nAeropuertos disponibles:");
                        foreach (var a in aeropuertos) Console.WriteLine($" - {a}");
                        break;
                    case "2":
                        grafo.MostrarTodosLosVuelos();
                        break;
                    case "3":
                        Console.Write("Ciudad de origen: ");
                        string origen = Console.ReadLine().Trim();
                        Console.Write("Ciudad de destino: ");
                        string destino = Console.ReadLine().Trim();
                        var (costo, ruta) = grafo.ObtenerRutaMasBarata(origen, destino);
                        if (costo == int.MaxValue)
                            Console.WriteLine($"No hay ruta disponible de {origen} a {destino}.");
                        else
                        {
                            Console.WriteLine($"\nRuta más barata: {string.Join(" -> ", ruta)}");
                            Console.WriteLine($"Costo total: ${costo}");
                        }
                        break;
                    case "4":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void CargarDatosDesdeArchivo(GrafoVuelos grafo, string archivo)
        {
            if (!File.Exists(archivo))
            {
                Console.WriteLine($"Advertencia: No se encontró {archivo}. Se usará un ejemplo predeterminado.");
                grafo.AgregarVuelo("Madrid", "Barcelona", 50);
                grafo.AgregarVuelo("Madrid", "Londres", 120);
                grafo.AgregarVuelo("Barcelona", "Roma", 80);
                grafo.AgregarVuelo("Londres", "NuevaYork", 300);
                grafo.AgregarVuelo("Roma", "Madrid", 90);
                return;
            }

            try
            {
                string[] lineas = File.ReadAllLines(archivo);
                foreach (string linea in lineas.Skip(1))
                {
                    string[] partes = linea.Split(';');
                    if (partes.Length == 3 && int.TryParse(partes[2], out int costo))
                        grafo.AgregarVuelo(partes[0], partes[1], costo);
                }
                Console.WriteLine("Datos cargados correctamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            }
        }
    }
}