using System;
using System.Collections.Generic;
using System.Text;
using ProyectoXamarin;

namespace ProyectoXamarin.DataModel
{
    public class ItemNavegacion
    {
        public ItemNavegacion() { }
        public ItemNavegacion(string titulo, string icono, Type target)
        {
            this.Titulo = titulo;
            this.Icono = icono;
            this.TargetType = target;
        }
        public string Titulo { get; set; }
        public string Icono { get; set; }
        public Type TargetType { get; set; }
    }
}
