using ExamenBindingMVVM.ModeloDatos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExamenBindingMVVM.ViewModel
{
    public class NuevaCitaViewModel : INotifyPropertyChanged
    {
        // Evento PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        // Atirbutos
        private string _nombre = "";
        public string Nombre
        {
            get
            {
                return this._nombre;
            }
            set
            {
                if (this._nombre != value)
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
                if (this._apellidos != value)
                {
                    this._apellidos = value;
                    OnPropertyChanged("Apellidos");
                    RefreshCanExecutes();
                }
            }
        }

        private string _fechaCita = "2000/01/01";
        public string FechaCita
        {
            get
            {
                return this._fechaCita;
            }
            set
            {
                if (this._fechaCita != value)
                {
                    this._fechaCita = value;
                    OnPropertyChanged("FechaCita");
                    RefreshCanExecutes();
                }
            }
        }

        private TimeSpan _horaCita;
        public TimeSpan HoraCita
        {
            get
            {
                return this._horaCita;
            }
            set
            {
                if (this._horaCita != value)
                {
                    this._horaCita = value;
                    OnPropertyChanged("HoraCita");
                    RefreshCanExecutes();
                }
            }
        }

        private string _motivoCita = "Seleccionar...";
        public string MotivoCita
        {
            get
            {
                return this._motivoCita;
            }
            set
            {
                if (this._motivoCita != value)
                {
                    this._motivoCita = value;
                    OnPropertyChanged("MotivoCita");
                    RefreshCanExecutes();
                }
            }
        }

        // Metodos
        private void limpiarCampos()
        {
            this.Nombre = "";
            this.Apellidos = "";
            this.FechaCita = "2000/01/01";
            //this.HoraCita = "00:00:00";
            this.MotivoCita = "Seleccionar...";
        }
        void RefreshCanExecutes()
        {
            (comandoBorrar as Command).ChangeCanExecute();
            (comandoCrear as Command).ChangeCanExecute();
        }

        private Boolean canBorrar = false;
        async Task OnAlertYesNoClicked()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Confirmacion", "¿Desea borrar el formulario?", "Si", "No");
            if (answer)
            {
                canBorrar = true;
            }
            else
            {
                canBorrar = false;
            }
        }

        // Comandos
        public ICommand comandoCrear { private set; get; }
        public ICommand comandoBorrar { private set; get; }

        // Constructores
        public NuevaCitaViewModel()
        {
            comandoCrear = new Command(
            execute: () =>
                {
                    if (MotivoCita.Equals("Seleccionar..."))
                    {
                        MotivoCita = "";
                    }
                    App.Database.SaveCita(new Cita(Nombre, Apellidos, FechaCita.Substring(0, 10), HoraCita.ToString(), MotivoCita));
                    // Mensaje emergente que informa de que se ha insertado la nueva cita
                    Application.Current.MainPage.DisplayAlert("Confirmacion", "Se ha insertado la nueva cita.", "Aceptar");
                    limpiarCampos();
                    RefreshCanExecutes();
                },
            canExecute: () =>
                {
                    // Valida que esten los datos requeridos, al utilizar campos concretos en el XAML no hay que comprobar que sean fechas exactamente
                    return Nombre != "" &&
                        Nombre != null &&
                        FechaCita != "2000/01/01" &&
                        FechaCita != null &&
                        //HoraCita != "00:00:00" &&
                        HoraCita != null &&
                        MotivoCita != "Seleccionar..." &&
                        MotivoCita != null;
                }
            );

            comandoBorrar = new Command(
            execute: async () =>
            {
                // Pregunta si se quiere borrar antes de hacer nada
                await OnAlertYesNoClicked();
                if (canBorrar)
                {
                    limpiarCampos();
                    // Informa de que se han borrado los campos del formulario
                    Application.Current.MainPage.DisplayAlert("Confirmacion", "Se ha borrado el formulario.", "Aceptar");
                }
                canBorrar = false;
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return Nombre != "" ||
                        FechaCita != "2000/01/01" ||
                        //HoraCita != "00:00:00" ||
                        MotivoCita != "Seleccionar...";
            }
            );
        }
    }
}
