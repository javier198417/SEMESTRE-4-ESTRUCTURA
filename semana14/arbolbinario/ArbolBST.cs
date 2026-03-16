using System;

namespace ArbolBinarioBusqueda
{
    public class ArbolBST
    {
        // Raiz puede ser nula si el árbol está vacío
        public Nodo? Raiz { get; private set; }

        public ArbolBST() => Raiz = null;

        // --- Operaciones Principales ---

        // Método público para insertar
        public void Insertar(int valor) => Raiz = InsertarRecursivo(Raiz, valor);

        // Lógica recursiva interna (soluciona error CS0161 con return actual;)
        private Nodo? InsertarRecursivo(Nodo? actual, int valor)
        {
            if (actual == null) return new Nodo(valor);

            if (valor < actual.Valor)
                actual.Izquierdo = InsertarRecursivo(actual.Izquierdo, valor);
            else if (valor > actual.Valor)
                actual.Derecho = InsertarRecursivo(actual.Derecho, valor);
            // Si el valor es igual, no hacemos nada (no duplicados en este BST básico)

            return actual; // Importante: siempre retorna el nodo (CS0161 corregido)
        }

        // Método público para buscar
        public bool Buscar(int valor) => BuscarRecursivo(Raiz, valor);

        // Lógica recursiva interna (soluciona error CS0161 con returns explícitos)
        private bool BuscarRecursivo(Nodo? actual, int valor)
        {
            if (actual == null) return false;
            if (valor == actual.Valor) return true;

            return valor < actual.Valor 
                ? BuscarRecursivo(actual.Izquierdo, valor) 
                : BuscarRecursivo(actual.Derecho, valor);
        }

        // Método público para eliminar
        public void Eliminar(int valor) => Raiz = EliminarRecursivo(Raiz, valor);

        // Lógica recursiva interna compleja
        private Nodo? EliminarRecursivo(Nodo? actual, int valor)
        {
            if (actual == null) return null;

            if (valor < actual.Valor)
                actual.Izquierdo = EliminarRecursivo(actual.Izquierdo, valor);
            else if (valor > actual.Valor)
                actual.Derecho = EliminarRecursivo(actual.Derecho, valor);
            else // Encontrado el nodo a eliminar
            {
                // Caso 1 y 2: Sin hijos o un solo hijo
                if (actual.Izquierdo == null) return actual.Derecho;
                if (actual.Derecho == null) return actual.Izquierdo;

                // Caso 3: Dos hijos. Reemplazar con el sucesor inorden (mínimo del subárbol derecho)
                actual.Valor = ObtenerMinimoValor(actual.Derecho);
                // Eliminar el sucesor inorden duplicado
                actual.Derecho = EliminarRecursivo(actual.Derecho, actual.Valor);
            }
            return actual;
        }

        // Helper para encontrar el valor mínimo
        private int ObtenerMinimoValor(Nodo actual)
        {
            int minVal = actual.Valor;
            while (actual.Izquierdo != null)
            {
                minVal = actual.Izquierdo.Valor;
                actual = actual.Izquierdo;
            }
            return minVal;
        }

        // --- Propiedades del Árbol (solucionan errores CS1061 de Program.cs) ---

        public int? VerMinimo()
        {
            if (Raiz == null) return null;
            return ObtenerMinimoValor(Raiz);
        }

        public int? VerMaximo()
        {
            if (Raiz == null) return null;
            Nodo actual = Raiz;
            while (actual.Derecho != null) actual = actual.Derecho;
            return actual.Valor;
        }

        public int ObtenerAltura() => CalcularAlturaRecursivo(Raiz);

        private int CalcularAlturaRecursivo(Nodo? actual)
        {
            if (actual == null) return 0;
            return 1 + Math.Max(CalcularAlturaRecursivo(actual.Izquierdo), CalcularAlturaRecursivo(actual.Derecho));
        }

        public void Limpiar() => Raiz = null;

        // --- Recorridos Públicos ---

        public void Inorden()
        {
            InordenRecursivo(Raiz);
            Console.WriteLine();
        }

        private void InordenRecursivo(Nodo? actual)
        {
            if (actual == null) return;
            InordenRecursivo(actual.Izquierdo);
            Console.Write($"{actual.Valor} ");
            InordenRecursivo(actual.Derecho);
        }
        // --- Recorridos Públicos Adicionales ---

public void Preorden()
{
    PreordenRecursivo(Raiz);
    Console.WriteLine();
}

private void PreordenRecursivo(Nodo? actual)
{
    if (actual == null) return;
    Console.Write($"{actual.Valor} "); // Raíz
    PreordenRecursivo(actual.Izquierdo); // Izquierda
    PreordenRecursivo(actual.Derecho);   // Derecha
}

public void Postorden()
{
    PostordenRecursivo(Raiz);
    Console.WriteLine();
}

private void PostordenRecursivo(Nodo? actual)
{
    if (actual == null) return;
    PostordenRecursivo(actual.Izquierdo); // Izquierda
    PostordenRecursivo(actual.Derecho);   // Derecha
    Console.Write($"{actual.Valor} ");    // Raíz
}
        
        // *Nota: Para completar tu trabajo, deberías agregar Preorden y Postorden siguiendo la misma lógica.*
    }
}