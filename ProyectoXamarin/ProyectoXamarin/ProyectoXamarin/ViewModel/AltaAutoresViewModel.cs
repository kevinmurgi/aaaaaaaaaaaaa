using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ProyectoXamarin.ViewModel;
using ProyectoXamarin.DataModel;
using System.Threading.Tasks;

namespace ProyectoXamarin.ViewModel
{
    public class AltaAutoresViewModel : INotifyPropertyChanged
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
                return this._nombre;
            }
            set
            {
                if (!value.Equals(this._nombre))
                {
                    this._nombre = value;
                    OnPropertyChanged("Nombre");
                    RefreshCanExecutes();
                }
            }
        }

        private string _apellidos = "";
        public string Apellidos
        {
            get
            {
                return this._apellidos;
            }
            set
            {
                if (!value.Equals(this._apellidos))
                {
                    this._apellidos = value;
                    OnPropertyChanged("Apellidos");
                    RefreshCanExecutes();
                }
            }
        }
        
        private string _nacimiento = "2000/01/01";
        public string Nacimiento
        {
            get
            {
                return this._nacimiento;
            }
            set
            {
                if (!value.Equals(this._nacimiento))
                {
                    this._nacimiento = value;
                    OnPropertyChanged("Nacimiento");
                }
            }
        }
        
        private string _telefono = "";
        public string Telefono
        {
            get
            {
                return this._telefono;
            }
            set
            {
                if (!value.Equals(this._telefono))
                {
                    this._telefono = value;
                    OnPropertyChanged("Telefono");
                    RefreshCanExecutes();
                }
            }
        }
        
        private string _sexo = "Seleccionar...";
        public string Sexo
        {
            get
            {
                return this._sexo;
            }
            set
            {
                if (!value.Equals(this._sexo))
                {
                    this._sexo = value;
                    OnPropertyChanged("Sexo");
                    RefreshCanExecutes();
                }
            }
        }

        // Metodos
        void limpiarCampos()
        {
            this.Nombre = "";
            this.Apellidos = "";
            this.Nacimiento = "2000/01/01";
            this.Telefono = "";
            this.Sexo = "Seleccionar...";
        }
        void RefreshCanExecutes()
        {
            ((Command)comandoAlta).ChangeCanExecute();
            ((Command)comandoBorrado).ChangeCanExecute();
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
        public AltaAutoresViewModel()
        {
            comandoAlta = new Command(
            execute: () =>
            {
                DataAccess.AddAutor(Nombre, Apellidos, Nacimiento.Substring(0, 10), Telefono, Sexo);
                limpiarCampos();
                Application.Current.MainPage.DisplayAlert("Información", "Autor registrado con éxito.", "Aceptar");
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return !Nombre.Equals("") &&
                        !Apellidos.Equals("") &&
                        !Telefono.Equals("") &&
                        !Sexo.Equals("Seleccionar...");
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
                        !Apellidos.Equals("") ||
                        !Telefono.Equals("") ||
                        !Sexo.Equals("Seleccionar...");
            }
            );
        }
    }
}
