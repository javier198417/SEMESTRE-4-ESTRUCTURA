using System;
using System.Collections.Generic;
using System.Linq;

namespace TraductorBasico
{
    class Program
    {
        // Diccionario principal (inglés -> español)
        static Dictionary<string, string> diccionarioInglesEspanol = new Dictionary<string, string>();
        
        // Diccionario inverso (español -> inglés) para búsqueda bidireccional
        static Dictionary<string, string> diccionarioEspanolIngles = new Dictionary<string, string>();

        static void Main(string[] args)
        {
            InicializarDiccionario();
            
            bool continuar = true;  // Cambiado: ya no usamos null, inicializamos como true directamente
            
            while (continuar)
            {
                MostrarMenu();
                
                string opcion = Console.ReadLine() ?? ""; // Manejo de posible null
                
                switch (opcion)
                {
                    case "1":
                        TraducirFrase();
                        break;
                    case "2":
                        AgregarPalabras();
                        break;
                    case "0":
                        continuar = false;
                        Console.WriteLine("¡Gracias por usar el traductor! Presione cualquier tecla para salir...");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Intente nuevamente.");
                        Console.WriteLine("Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void InicializarDiccionario()
        {
            // Lista base de palabras sugeridas
            var palabrasBase = new Dictionary<string, string>
            {
                {"time", "tiempo"},
                {"person", "persona"},
                {"year", "año"},
                {"way", "camino"},
                {"day", "día"},
                {"thing", "cosa"},
                {"man", "hombre"},
                {"world", "mundo"},
                {"life", "vida"},
                {"hand", "mano"},
                {"part", "parte"},
                {"child", "niño"},
                {"eye", "ojo"},
                {"woman", "mujer"},
                {"place", "lugar"},
                {"work", "trabajo"},
                {"week", "semana"},
                {"case", "caso"},
                {"point", "punto"},
                {"government", "gobierno"},
                {"company", "compañía"}
            };
            
            foreach (var palabra in palabrasBase)
            {
                diccionarioInglesEspanol.Add(palabra.Key, palabra.Value);
                diccionarioEspanolIngles.Add(palabra.Value, palabra.Key);
            }
            
            Console.WriteLine($"Diccionario inicializado con {diccionarioInglesEspanol.Count} palabras.");
        }

        static void MostrarMenu()
        {
            Console.Clear();
            Console.WriteLine("==================== MENÚ ====================");
            Console.WriteLine();
            Console.WriteLine("1. Traducir una frase");
            Console.WriteLine("2. Agregar palabras al diccionario");
            Console.WriteLine("0. Salir");
            Console.WriteLine();
            Console.Write("Seleccione una opción: ");
        }

        static void TraducirFrase()
        {
            Console.Clear();
            Console.WriteLine("=== TRADUCTOR INGLÉS-ESPAÑOL ===");
            Console.WriteLine();
            Console.WriteLine("Ingrese la frase a traducir:");
            string? frase = Console.ReadLine();  // Usamos string? para permitir null
            
            if (string.IsNullOrWhiteSpace(frase))
            {
                Console.WriteLine("No ingresó ninguna frase.");
                Console.WriteLine("Presione cualquier tecla para continuar...");
                Console.ReadKey();
                return;
            }
            
            // Dividir la frase en palabras
            string[] palabras = frase.Split(new[] { ' ', '.', ',', ';', ':', '!', '¿', '?', '¡' }, 
                                           StringSplitOptions.RemoveEmptyEntries);
            
            string fraseTraducida = frase;
            
            // Traducir cada palabra si existe en el diccionario
            foreach (string palabra in palabras)
            {
                string palabraLimpia = palabra.ToLower().Trim();
                string? traduccion = BuscarTraduccion(palabraLimpia);
                
                if (traduccion != null)
                {
                    // Reemplazar la palabra en la frase original (respetando mayúsculas/minúsculas)
                    fraseTraducida = ReemplazarPalabra(fraseTraducida, palabra, traduccion);
                }
            }
            
            Console.WriteLine();
            Console.WriteLine("=== RESULTADO ===");
            Console.WriteLine($"Frase original: {frase}");
            Console.WriteLine($"Traducción: {fraseTraducida}");
            Console.WriteLine();
            Console.WriteLine("Nota: Solo se tradujeron las palabras que existen en el diccionario.");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }

        static string? BuscarTraduccion(string palabra)  // Cambiamos a string? para permitir null
        {
            // Buscar en diccionario inglés -> español
            if (diccionarioInglesEspanol.ContainsKey(palabra))
            {
                return diccionarioInglesEspanol[palabra];
            }
            
            // Buscar en diccionario español -> inglés
            if (diccionarioEspanolIngles.ContainsKey(palabra))
            {
                return diccionarioEspanolIngles[palabra];
            }
            
            return null;
        }

        static string ReemplazarPalabra(string fraseOriginal, string palabraOriginal, string nuevaPalabra)
        {
            // Reemplazar la palabra respetando mayúsculas/minúsculas
            int index = fraseOriginal.IndexOf(palabraOriginal, StringComparison.Ordinal);
            
            if (index >= 0)
            {
                // Mantener el formato de mayúsculas/minúsculas de la palabra original
                if (char.IsUpper(palabraOriginal[0]))
                {
                    nuevaPalabra = char.ToUpper(nuevaPalabra[0]) + nuevaPalabra.Substring(1);
                }
                
                fraseOriginal = fraseOriginal.Remove(index, palabraOriginal.Length);
                fraseOriginal = fraseOriginal.Insert(index, nuevaPalabra);
            }
            
            return fraseOriginal;
        }

        static void AgregarPalabras()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR PALABRAS AL DICCIONARIO ===");
            Console.WriteLine();
            Console.WriteLine("Ingrese una palabra en INGLÉS y su traducción en ESPAÑOL");
            Console.WriteLine("(Separe las palabras con un guión, ejemplo: computer-computadora)");
            Console.WriteLine("O ingrese 0 para volver al menú principal.");
            Console.WriteLine();
            
            while (true)
            {
                Console.Write("Ingrese la palabra (o 0 para salir): ");
                string? entrada = Console.ReadLine();  // Usamos string? para permitir null
                
                if (entrada == "0")
                {
                    break;
                }
                
                if (string.IsNullOrWhiteSpace(entrada) || !entrada.Contains("-"))
                {
                    Console.WriteLine("Formato incorrecto. Debe usar el formato: palabraIngles-palabraEspanol");
                    continue;
                }
                
                string[] partes = entrada.Split('-');
                string palabraIngles = partes[0].Trim().ToLower();
                string palabraEspanol = partes[1].Trim().ToLower();
                
                if (string.IsNullOrWhiteSpace(palabraIngles) || string.IsNullOrWhiteSpace(palabraEspanol))
                {
                    Console.WriteLine("Ambas palabras deben tener contenido.");
                    continue;
                }
                
                // Verificar si la palabra ya existe en alguno de los diccionarios
                if (diccionarioInglesEspanol.ContainsKey(palabraIngles))
                {
                    Console.WriteLine($"La palabra '{palabraIngles}' ya existe en el diccionario.");
                    Console.WriteLine($"Traducción actual: {diccionarioInglesEspanol[palabraIngles]}");
                    Console.Write("¿Desea actualizarla? (s/n): ");
                    string? respuesta = Console.ReadLine()?.ToLower();  // Manejo de null
                    
                    if (respuesta == "s")
                    {
                        // Eliminar entrada antigua de ambos diccionarios
                        string traduccionAnterior = diccionarioInglesEspanol[palabraIngles];
                        diccionarioEspanolIngles.Remove(traduccionAnterior);
                        diccionarioInglesEspanol[palabraIngles] = palabraEspanol;
                        
                        if (!diccionarioEspanolIngles.ContainsKey(palabraEspanol))
                        {
                            diccionarioEspanolIngles.Add(palabraEspanol, palabraIngles);
                        }
                        
                        Console.WriteLine("Palabra actualizada correctamente.");
                    }
                }
                else if (diccionarioEspanolIngles.ContainsKey(palabraEspanol))
                {
                    Console.WriteLine($"La palabra '{palabraEspanol}' ya existe en el diccionario.");
                    Console.WriteLine($"Traducción actual: {diccionarioEspanolIngles[palabraEspanol]}");
                    Console.Write("¿Desea actualizarla? (s/n): ");
                    string? respuesta = Console.ReadLine()?.ToLower();  // Manejo de null
                    
                    if (respuesta == "s")
                    {
                        // Eliminar entrada antigua de ambos diccionarios
                        string traduccionAnterior = diccionarioEspanolIngles[palabraEspanol];
                        diccionarioInglesEspanol.Remove(traduccionAnterior);
                        diccionarioEspanolIngles[palabraEspanol] = palabraIngles;
                        
                        if (!diccionarioInglesEspanol.ContainsKey(palabraIngles))
                        {
                            diccionarioInglesEspanol.Add(palabraIngles, palabraEspanol);
                        }
                        
                        Console.WriteLine("Palabra actualizada correctamente.");
                    }
                }
                else
                {
                    // Agregar nueva palabra a ambos diccionarios
                    diccionarioInglesEspanol.Add(palabraIngles, palabraEspanol);
                    diccionarioEspanolIngles.Add(palabraEspanol, palabraIngles);
                    Console.WriteLine("Palabra agregada correctamente.");
                }
                
                Console.WriteLine($"Total de palabras en el diccionario: {diccionarioInglesEspanol.Count}");
                Console.WriteLine();
            }
            
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
        }
    }
}