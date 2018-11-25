using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ProyectoXamarin.DataModel;

namespace ProyectoXamarin.ViewModel
{
    public class DatosLibros
    {
        // Hacemos Singleton para que solo se acceda 1 vez a la base de datos
        private static DatosLibros _datos = new DatosLibros();
        public static DatosLibros Datos
        {
            get
            {
                return _datos;
            }
        }


        // Atributos
        private static ObservableCollection<Libro> _libros = DataAccess.GetLibros();
        public ObservableCollection<Libro> Libros
        {
            get
            {
                return _libros;
            }
            set
            {
                _libros = value;
            }
        }

        // Constructores
        public DatosLibros(){}
    }
}
