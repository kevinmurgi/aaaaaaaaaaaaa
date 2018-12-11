using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExamenBindingMVVM.ModeloDatos
{
    public class ItemsMenu
    {
        // Atributos
        private static ObservableCollection<ItemNavegacion> _itemsMenuPrincipal = new ObservableCollection<ItemNavegacion>();
        public ObservableCollection<ItemNavegacion> ItemsMenuPrincipal
        {
            get
            {
                return _itemsMenuPrincipal;
            }
        }

        // Constructor
        public ItemsMenu() {
            var pagina1 = new ItemNavegacion() { titulo = "Ver Citas", icono = "Assets/citas.png", tipoPagina = typeof(VerCitas) };
            var pagina2 = new ItemNavegacion() { titulo = "Nueva Cita", icono = "Assets/nueva.png", tipoPagina = typeof(NuevaCita) };
            _itemsMenuPrincipal.Add(pagina1);
            _itemsMenuPrincipal.Add(pagina2);
        }

    }
}
