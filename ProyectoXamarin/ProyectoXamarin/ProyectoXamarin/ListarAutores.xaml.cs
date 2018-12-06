using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoXamarin.DataModel;
using ProyectoXamarin.ViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListarAutores : ContentPage
	{
        public ListarAutores ()
		{
            InitializeComponent ();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listaAutores.ItemsSource = await App.Database.GetAutores();
        }
    }
}