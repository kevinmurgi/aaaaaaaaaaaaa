using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ProyectoXamarin.DataModel;

namespace ProyectoXamarin.ViewModel
{
    public class DatosAutores
    {
        // Hacemos Singleton para que solo se acceda 1 vez a la base de datos
        private static DatosAutores _datos = new DatosAutores();
        public static DatosAutores Datos
        {
            get
            {
                return _datos;
            }
        }

        // Atributos
        private static ObservableCollection<Autor> _autores = DataAccess.GetAutores();
        public ObservableCollection<Autor> Autores
        {
            get
            {
                return _autores;
            }
            set
            {
                _autores = value;
            }
        }

        // Constructores
        public DatosAutores() { }
    }
}
