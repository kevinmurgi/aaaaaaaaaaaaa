using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ProyectoXamarin.DataModel;

namespace ProyectoXamarin
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            // Primera pagina que se cargara cuando se ejecute la aplicacion
            Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(ListarAutores)));
        }

        // Metodo que carga la pagina al seleccionar un item de la viewlist (proximamente sera un comando)
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = (ItemNavegacion)e.SelectedItem;
            Type page = item.TargetType;
            Detail = new NavigationPage((Page)Activator.CreateInstance(page));
            IsPresented = false;
        }
    }
}
