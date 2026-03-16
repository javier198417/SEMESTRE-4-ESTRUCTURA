public class Nodo
{
    public int Valor { get; set; }
    // El '?' indica que estos campos pueden ser nulos legítimamente
    public Nodo? Izquierdo { get; set; } 
    public Nodo? Derecho { get; set; }

    public Nodo(int valor)
    {
        Valor = valor;
        Izquierdo = null;
        Derecho = null;
    }
}
