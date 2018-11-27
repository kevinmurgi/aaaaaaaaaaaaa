using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ProyectoXamarin.DataModel
{
    public class ItemsMenu
    {
        private static ObservableCollection<ItemNavegacion> _items = new ObservableCollection<ItemNavegacion>()
            { new ItemNavegacion(){ Titulo = "Ver Autores", Icono = "", TargetType = typeof(ListarAutores)},
        new ItemNavegacion(){ Titulo = "Ver Libros", Icono = "", TargetType = typeof(ListarLibros)},
        new ItemNavegacion(){ Titulo = "Nuevo Autor", Icono = "", TargetType = typeof(AltaAutores)},
        new ItemNavegacion(){ Titulo = "Nuevo Libro", Icono = "", TargetType = typeof(AltaLibros)}};
        public ObservableCollection<ItemNavegacion> Items = new ObservableCollection<ItemNavegacion>();
    }
}
