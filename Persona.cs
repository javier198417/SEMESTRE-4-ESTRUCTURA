using System;
using System.Collections.Generic;

public class Persona
    {
       public string Nombre { get; set; }
        public int NumeroAsiento { get; set; }

        public Persona(string nombre, int asiento)
        {
            Nombre = nombre;
            NumeroAsiento = asiento;
        }
    }