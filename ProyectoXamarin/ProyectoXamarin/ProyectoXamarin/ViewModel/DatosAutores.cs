using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using ProyectoXamarin.DataModel;

/*
 * Clase DatosAutores que contiene una lista estática con los objetos Autor que hay almacenados en la BBDD
 */
namespace ProyectoXamarin.ViewModel
{
    public class DatosAutores : INotifyPropertyChanged
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
        private ObservableCollection<Autor> _autores = DataAccess.GetAutores();
        public ObservableCollection<Autor> Autores
        {
            get
            {
                return _autores;
            }
            set
            {
                _autores = value;
                OnPropertyChanged("Autores");
            }
        }

        // Constructores
        public DatosAutores() { }
    }
}
