﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using ProyectoXamarin.DataModel;

namespace ProyectoXamarin.ViewModel
{
    public class DatosLibros : INotifyPropertyChanged
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
