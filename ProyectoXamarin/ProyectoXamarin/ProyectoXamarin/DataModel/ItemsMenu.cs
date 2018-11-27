using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

/*
 * Clase ItemsMenu que crea una lista estática con los botones que van a formar el menú de la aplicación
 */
namespace ProyectoXamarin.DataModel
{
    public class ItemsMenu
    {
        public ItemsMenu()
        {
            var page1 = new ItemNavegacion() { Titulo = "Ver Autores", Icono = "Assets/persona.png", TargetType = typeof(ListarAutores) };
            var page2 = new ItemNavegacion() { Titulo = "Ver Libros", Icono = "Assets/libro.png", TargetType = typeof(ListarLibros) };
            var page3 = new ItemNavegacion() { Titulo = "Nuevo Autor", Icono = "Assets/plus.png", TargetType = typeof(AltaAutores) };
            var page4 = new ItemNavegacion() { Titulo = "Nuevo Libro", Icono = "Assets/plus.png", TargetType = typeof(AltaLibros) };
            _items.Add(page1);
            _items.Add(page2);
            _items.Add(page3);
            _items.Add(page4);
        }
        private static ObservableCollection<ItemNavegacion> _items = new ObservableCollection<ItemNavegacion>();

        public ObservableCollection<ItemNavegacion> Items
        {
            get{
                return _items;
            }
        }
    }
}
