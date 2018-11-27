using System;
using System.Collections.Generic;
using System.Text;
using ProyectoXamarin;

/*
 * Clase ItemNavegacion que solamente define la estructura que va a tener cada objeto del menu de la aplicación.
 */
namespace ProyectoXamarin.DataModel
{
    public class ItemNavegacion
    {
        // Constructor
        public ItemNavegacion() { }

        // Atributos de la clase
        public string Titulo { get; set; }
        public string Icono { get; set; }
        public Type TargetType { get; set; }
    }
}
