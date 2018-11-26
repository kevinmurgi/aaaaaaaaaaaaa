using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using ProyectoXamarin.DataModel;

namespace ProyectoXamarin.ViewModel
{
    public class DatosAutores : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
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
                OnPropertyChanged("Autores");
            }
        }

        // Constructores
        public DatosAutores() { }
    }
}
