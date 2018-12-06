using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoXamarin.ViewModel;
using ProyectoXamarin.DataModel;

namespace ProyectoXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AltaLibros : ContentPage
	{
        private List<Autor> autores;

        public AltaLibros ()
		{
			InitializeComponent ();
            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            //autoresPicker.BindingContext = await App.Database.GetAutores();
            List<Autor> autores = await App.Database.GetAutores();
            List<string> nomAutores = new List<string>();
            foreach (Autor a in autores)
            {
                nomAutores.Add(a.Nombre);
            }
            autoresPicker.ItemsSource = nomAutores;
        }
    }
}