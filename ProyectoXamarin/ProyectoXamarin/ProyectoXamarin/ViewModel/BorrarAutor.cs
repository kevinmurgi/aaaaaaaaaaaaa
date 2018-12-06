using ProyectoXamarin.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ProyectoXamarin;

namespace ProyectoXamarin.ViewModel
{
    public class BorrarAutor : INotifyPropertyChanged
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

        // Atributos
        private Autor _autorActual;
        public Autor AutorActual
        {
            get
            {
                return _autorActual;
            }
            set
            {
                this._autorActual = value;
                OnPropertyChanged("AutorActual");
                RefreshCanExecutes();
            }
        }

        // Comandos
        public ICommand comandoBorrar { private set; get; }

        // Metodos
        void RefreshCanExecutes()
        {
            ((Command)comandoBorrar).ChangeCanExecute();
        }

        // Constructores
        public BorrarAutor()
        {
            comandoBorrar = new Command(
            execute: () =>
            {
                DataAccess.BorrarAutor(AutorActual.Nombre);
            },
            canExecute: () =>
            {
                return AutorActual != null;
            }
            );
        }
    }
}
