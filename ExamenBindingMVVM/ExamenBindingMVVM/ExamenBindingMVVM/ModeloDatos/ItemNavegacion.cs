using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenBindingMVVM.ModeloDatos
{
    public class ItemNavegacion
    {
        // Atributos
        public string titulo { get; set; }
        public string icono { get; set; }
        public Type tipoPagina { get; set; }

        // Constructor
        public ItemNavegacion()
        {
        }
    }
}
