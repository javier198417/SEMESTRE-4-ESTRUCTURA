using System;

namespace ArbolBinarioBusqueda
{
    class Program
    {
        static void Main(string[] args)
        {
            ArbolBST arbol = new ArbolBST();
            int opcion = -1;

            while (opcion != 0)
            {
                Console.Clear();
                Console.WriteLine("================================================");
                Console.WriteLine("    GESTIÓN DE ÁRBOL BINARIO DE BÚSQUEDA (BST)  ");
                Console.WriteLine("================================================");
                Console.WriteLine("1. Insertar valor");
                Console.WriteLine("2. Buscar valor");
                Console.WriteLine("3. Eliminar valor");
                Console.WriteLine("4. Mostrar Recorrido Inorden");
                Console.WriteLine("5. Ver Estadísticas (Min, Max, Altura)");
                Console.WriteLine("6. Limpiar árbol");
                Console.WriteLine("0. Salir");
                Console.WriteLine("------------------------------------------------");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion)) continue;

                switch (opcion)
                {
                    case 1:
                        Console.Write("\nIngrese el número a insertar: ");
                        if (int.TryParse(Console.ReadLine(), out int valInsert))
                        {
                            arbol.Insertar(valInsert);
                            Console.WriteLine("Valor insertado con éxito.");
                        }
                        break;

                    case 2:
                        Console.Write("\nIngrese el número a buscar: ");
                        if (int.TryParse(Console.ReadLine(), out int valSearch))
                        {
                            bool encontrado = arbol.Buscar(valSearch);
                            Console.WriteLine(encontrado ? "-> El valor existe en el árbol." : "-> El valor NO se encuentra.");
                        }
                        break;

                    case 3:
                        Console.Write("\nIngrese el número a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int valDel))
                        {
                            arbol.Eliminar(valDel);
                            Console.WriteLine("Operación de eliminación realizada.");
                        }
                        break;

                    case 4:
                       
                         Console.WriteLine("\n--- RECORRIDOS DEL ÁRBOL ---");
                         if (arbol.Raiz == null) 
                        {
                            Console.WriteLine("El árbol está vacío.");
                        }
                     else 
                        {
                        Console.Write("Preorden:  "); arbol.Preorden();
                        Console.Write("Inorden:   "); arbol.Inorden();
                        Console.Write("Postorden: "); arbol.Postorden();
                    }
                    break;

                    case 5:
                        Console.WriteLine("\n--- Estadísticas del Árbol ---");
                        if (arbol.Raiz != null)
                        {
                            Console.WriteLine($"- Valor Mínimo: {arbol.VerMinimo()}");
                            Console.WriteLine($"- Valor Máximo: {arbol.VerMaximo()}");
                            Console.WriteLine($"- Altura Total: {arbol.ObtenerAltura()}");
                        }
                        else Console.WriteLine("El árbol está vacío.");
                        break;

                    case 6:
                        arbol.Limpiar();
                        Console.WriteLine("\nÁrbol vaciado completamente.");
                        break;

                    case 0:
                        Console.WriteLine("\nSaliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("\nOpción no válida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}