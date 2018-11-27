using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoXamarin.ViewModel;

namespace ProyectoXamarin
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AltaLibros : ContentPage
	{
        ObservableCollection<string> autores;
        public AltaLibros ()
		{
			InitializeComponent ();
            autores = DataAccess.GetNombreAutores();
            autoresPicker.ItemsSource = autores;
        }
	}
}