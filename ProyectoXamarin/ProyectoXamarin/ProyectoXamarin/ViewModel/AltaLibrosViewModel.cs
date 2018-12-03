using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ProyectoXamarin.ViewModel;
using ProyectoXamarin.DataModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Diagnostics;
using System.Threading.Tasks;

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

        // Generos
        private string _generos { get; set; }
        private Boolean _aventura;
        public Boolean Aventura
        {
            get
            {
                return _aventura;
            }
            set
            {
                if (!value.Equals(_aventura))
                {
                    _aventura = value;
                    OnPropertyChanged("Aventura");
                }
            }
        }
        private Boolean _ccff;
        public Boolean CCFF
        {
            get
            {
                return _ccff;
            }
            set
            {
                if (!value.Equals(_ccff))
                {
                    _ccff = value;
                    OnPropertyChanged("CCFF");
                }
            }
        }
        private Boolean _fantasia;
        public Boolean Fantasia
        {
            get
            {
                return _fantasia;
            }
            set
            {
                if (!value.Equals(_fantasia))
                {
                    _fantasia = value;
                    OnPropertyChanged("Fantasia");
                }
            }
        }
        private Boolean _paranormal;
        public Boolean Paranormal
        {
            get
            {
                return _paranormal;
            }
            set
            {
                if (!value.Equals(_paranormal))
                {
                    _paranormal = value;
                    OnPropertyChanged("Paranormal");
                }
            }
        }
        private Boolean _romance;
        public Boolean Romance
        {
            get
            {
                return _romance;
            }
            set
            {
                if (!value.Equals(_romance))
                {
                    _romance = value;
                    OnPropertyChanged("Romance");
                }
            }
        }
        private Boolean _comedia;
        public Boolean Comedia
        {
            get
            {
                return _comedia;
            }
            set
            {
                if (!value.Equals(_comedia))
                {
                    _comedia = value;
                    OnPropertyChanged("Comedia");
                }
            }
        }

        // Metodos
        void limpiarCampos()
        {
            this.Nombre = "";
            //this.NomAutor = "";
            this.Lanzamiento = "2000/01/01";
            this.Paginas = "0";
            this.Aventura = false;
            this.CCFF = false;
            this.Fantasia = false;
            this.Paranormal = false;
            this.Romance = false;
            this.Comedia = false;
        }
        void RefreshCanExecutes()
        {
            ((Command)comandoAlta).ChangeCanExecute();
            ((Command)comandoBorrado).ChangeCanExecute();
        }
        void comprobarGeneros()
        {
            _generos = "";
            if (_aventura)
            {
                _generos += "Aventura, ";
            }
            if (_ccff)
            {
                _generos += "Ciencia Ficción, ";
            }
            if (_fantasia)
            {
                _generos += "Fantasía, ";
            }
            if (_paranormal)
            {
                _generos += "Paranormal, ";
            }
            if (_romance)
            {
                _generos += "Romance, ";
            }
            if (_comedia)
            {
                _generos += "Comedia, ";
            }
            if (_generos.Length > 2)
            {
                _generos = _generos.Substring(0, (_generos.Length - 2));
            }
        }

        // Muestra una aletra para preguntar si se quiere borrar, usando un atributo se comprueba en el comando
        private bool canClean = false;
        async Task OnAlertYesNoClicked()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("", "¿Desea borrar el formulario?", "Si", "No");
            if (answer)
            {
                canClean = true;
            }
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
                comprobarGeneros();
                DataAccess.AddLibro(Nombre, autorAux, Lanzamiento, Int32.Parse(Paginas), _generos);
                limpiarCampos();
                Application.Current.MainPage.DisplayAlert("Información", "Libro registrado con éxito.", "Aceptar");
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return !Nombre.Equals("") &&
                        Paginas != "0";
            }
            );

            comandoBorrado = new Command(
            execute: async () =>
            {
                await OnAlertYesNoClicked();
                if (canClean)
                {
                    limpiarCampos();
                }
                canClean = false;
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
