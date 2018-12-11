using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenBindingMVVM.ModeloDatos;
using System.Collections.ObjectModel;

namespace ExamenBindingMVVM
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerCitas : ContentPage
	{
        private List<Cita> citas;

		public VerCitas ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            citas = await App.Database.GetCitas();
            listaCitas.ItemsSource = citas;
        }
    }
}