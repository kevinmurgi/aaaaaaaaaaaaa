using ProyectoXamarin.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using ProyectoXamarin;
using System.Threading.Tasks;

namespace ProyectoXamarin.ViewModel
{
    public class BorrarLibro : INotifyPropertyChanged
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
        private Libro _libroActual;
        public Libro LibroActual
        {
            get
            {
                return _libroActual;
            }
            set
            {
                this._libroActual = value;
                OnPropertyChanged("LibroActual");
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

        // Muestra una aletra para preguntar si se quiere borrar, usando un atributo se comprueba en el comando
        private bool canClean = false;
        async Task OnAlertYesNoClicked()
        {
            var answer = await Application.Current.MainPage.DisplayAlert("", "¿Desea borrar el libro?", "Si", "No");
            if (answer)
            {
                canClean = true;
            }
        }

        // Constructores
        public BorrarLibro()
        {
            comandoBorrar = new Command(
            execute: async () =>
            {
                await OnAlertYesNoClicked();
                if (canClean)
                {
                    await App.Database.DeleteLibro(LibroActual);
                    var pagina = ((MasterDetailPage)Application.Current.MainPage);
                    pagina.Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListarLibros)));
                }
                canClean = false;
                RefreshCanExecutes();
            },
            canExecute: () =>
            {
                return LibroActual != null;
            }
            );
        }
    }
}
