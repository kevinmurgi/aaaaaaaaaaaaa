using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using ProyectoXamarin.DataModel;

/*
 * Clase DatosLibros que contiene una lista estática con los objetos Libro que hay almacenados en la BBDD
 */
namespace ProyectoXamarin.ViewModel
{
    public class DatosLibros : INotifyPropertyChanged
    {
        // Evento y metodo de PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Atributos de la clase
        private ObservableCollection<Libro> _libros = DataAccess.GetLibros();
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
