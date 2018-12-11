using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ExamenBindingMVVM.ModeloDatos;

namespace ExamenBindingMVVM
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(VerCitas)));
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var pagina = (ItemNavegacion)e.SelectedItem;
            Type tipo = pagina.tipoPagina;
            Detail = new NavigationPage((Page)Activator.CreateInstance(tipo));
            IsPresented = false;
        }
    }
}
