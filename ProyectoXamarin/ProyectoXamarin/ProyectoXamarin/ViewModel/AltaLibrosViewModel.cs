using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ProyectoXamarin.ViewModel;
using ProyectoXamarin.DataModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModel
{
    public class AltaLibrosViewModel : INotifyPropertyChanged
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

        // Atributos del ViewModel
        private string _nombre = "";
        public string Nombre
        {
            get
            {
                return _nombre;
            }
            set
            {
                if (!value.Equals(_nombre))
                {
                    _nombre = value;
                    OnPropertyChanged("Nombre");
                    RefreshCanExecutes();
                }
            }
        }

        private string _nomAutor;
        public string NomAutor
        {
            get
            {
                return _nomAutor;
            }
            set
            {
                if (!value.Equals(_nomAutor))
                {
                    _nomAutor = value;
                    OnPropertyChanged("NomAutor");
                    RefreshCanExecutes();
                }
            }
        }

        private string _lanzamiento = "2000/01/01";
        public string Lanzamiento
        {
            get
            {
                return _lanzamiento;
            }
            set
            {
                if (!value.Equals(_lanzamiento))
                {
                    _lanzamiento = value;
                    OnPropertyChanged("Lanzamiento");
                }
            }
        }

        private string _paginas = "0";
        public string Paginas
        {
            get
            {
                return _paginas;
            }
            set
            {
                if (!value.Equals(_paginas))
                {
                    _paginas = value;
                    OnPropertyChanged("Paginas");
                    RefreshCanExecutes();
                }
            }
        }

        private string _genero;
        public string Genero
        {
            get
            {
                return _genero;
            }
            set
            {
                if (!value.Equals(_genero))
                {
                    _genero = value;
                    OnPropertyChanged("Genero");
                }
            }
        }

        // Metodos
        void limpiarCampos()
        {
            this.Nombre = "";
            //this.NomAutor = "Seleccionar...";
            this.Lanzamiento = "2000/01/01";
            this.Paginas = "0";
            this.Genero = "Seleccionar...";
        }
        void RefreshCanExecutes()
        {
            ((Command)comandoAlta).ChangeCanExecute();
            ((Command)comandoBorrado).ChangeCanExecute();
        }

        // Comandos
        public ICommand comandoAlta { private set; get; }
        public ICommand comandoBorrado { private set; get; }

        // Constructores
        public AltaLibrosViewModel()
        {
            comandoAlta = new Command(
            execute: () =>
            {
                string autorAux = "Sin Autor";
                if (NomAutor != null)
                {
                    autorAux = NomAutor;
                }
                DataAccess.AddLibro(Nombre, autorAux, Lanzamiento, Int32.Parse(Paginas), "No hay genero");
                limpiarCampos();
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return !Nombre.Equals("") &&
                        Paginas != "0";
            }
            );

            comandoBorrado = new Command(
            execute: () =>
            {
                limpiarCampos();
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return !Nombre.Equals("") ||
                        Paginas != "0";
            }
            );
        }
    }
}
