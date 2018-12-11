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
    public class BorrarCita : INotifyPropertyChanged
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

        // Atributos
        private Cita _citaActual;
        public Cita CitaActual
        {
            get
            {
                return this._citaActual;
            }
            set
            {
                if (this._citaActual != value)
                {
                    this._citaActual = value;
                    OnPropertyChanged("CitaActual");
                    RefreshCanExecutes();
                }
            }
        }

        // Metodos
        void RefreshCanExecutes()
        {
            (comandoBorrar as Command).ChangeCanExecute();
        }

        private Boolean canBorrar = false;
        async Task OnAlertYesNoClicked()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Confirmacion", "¿Desea borrar la cita?", "Si", "No");
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
        public ICommand comandoBorrar { private set; get; }

        // Constructores
        public BorrarCita()
        {
            comandoBorrar = new Command(
            execute: async () =>
            {
                // Pregunta si se quiere borrar antes de hacer nada
                await OnAlertYesNoClicked();
                if (canBorrar)
                {
                    App.Database.DeleteItemAsync(CitaActual);
                    // Informa de que se ha borrado la cita
                    Application.Current.MainPage.DisplayAlert("Confirmacion", "Se ha borrado la cita.", "Aceptar");
                    // Recarga la pagina
                    ((MasterDetailPage)Application.Current.MainPage).Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(VerCitas)));
                }
                canBorrar = false;
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return CitaActual != null;
            }
            );
        }
    }
}
