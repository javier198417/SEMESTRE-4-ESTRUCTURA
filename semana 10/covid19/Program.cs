using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 1. Generar conjunto total de 500 ciudadanos
        HashSet<string> ciudadanos = new HashSet<string>();
        for (int i = 1; i <= 500; i++) ciudadanos.Add($"Ciudadano {i}");

        // 2. Generar conjunto de vacunados con Pfizer (75 ciudadanos)
        HashSet<string> pfizer = new HashSet<string>();
        for (int i = 1; i <= 75; i++) pfizer.Add($"Ciudadano {i}");

        // 3. Generar conjunto de vacunados con AstraZeneca (75 ciudadanos)
        // Algunos ciudadanos (del 61 al 75) tendrán ambas para el ejercicio
        HashSet<string> astrazeneca = new HashSet<string>();
        for (int i = 61; i <= 135; i++) astrazeneca.Add($"Ciudadano {i}");

        // --- OPERACIONES DE TEORÍA DE CONJUNTOS ---

        // A. Ciudadanos que han recibido ambas dosis (Intersección)
        // Formula: Pfizer ∩ AstraZeneca
        HashSet<string> ambasDosis = new HashSet<string>(pfizer);
        ambasDosis.IntersectWith(astrazeneca);

        // B. Ciudadanos que NO se han vacunado (Diferencia)
        // Formula: Ciudadanos - (Pfizer ∪ AstraZeneca)
        HashSet<string> todosVacunados = new HashSet<string>(pfizer);
        todosVacunados.UnionWith(astrazeneca);
        
        HashSet<string> noVacunados = new HashSet<string>(ciudadanos);
        noVacunados.ExceptWith(todosVacunados);

        // C. Solo Pfizer (Diferencia)
        // Formula: Pfizer - AstraZeneca
        HashSet<string> soloPfizer = new HashSet<string>(pfizer);
        soloPfizer.ExceptWith(astrazeneca);

        // D. Solo AstraZeneca (Diferencia)
        // Formula: AstraZeneca - Pfizer
        HashSet<string> soloAstrazeneca = new HashSet<string>(astrazeneca);
        soloAstrazeneca.ExceptWith(pfizer);

        // --- MOSTRAR RESULTADOS ---
        MostrarResultados("No vacunados", noVacunados);
        MostrarResultados("Ambas dosis", ambasDosis);
        MostrarResultados("Solo Pfizer", soloPfizer);
        MostrarResultados("Solo AstraZeneca", soloAstrazeneca);
    }

    static void MostrarResultados(string titulo, HashSet<string> conjunto)
    {
        Console.WriteLine($"\n--- {titulo} ({conjunto.Count}) ---");
        // Mostramos los primeros 5 para no saturar la consola
        foreach (var c in conjunto.Take(5)) Console.WriteLine(c);
        if (conjunto.Count > 5) Console.WriteLine("...");
    }
}
