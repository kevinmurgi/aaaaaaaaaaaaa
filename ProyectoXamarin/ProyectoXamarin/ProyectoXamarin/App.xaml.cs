using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoXamarin.ViewModel;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProyectoXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DataAccess.InitializeDatabase();

            /*
            DataAccess.AddAutor("Brandon", "Sanderson", "19/12/1975", "Desconocido", "Masculino");
            DataAccess.AddAutor("Patrick", "Rothfuss", "06/06/1973", "Desconocido", "Masculino");
            DataAccess.AddAutor("Daniel", "Rubio", "10/10/1999", "Desconocido", "Masculino");
            DataAccess.AddAutor("Joanne", "Rowling", "31/07/1965", "Desconocido", "Femenino");
            DataAccess.AddAutor("John", "Tolkien", "03/01/1892", "Desconocido", "Masculino");

            DataAccess.AddLibro("Mistborn", "Brandon", "04/12/2016", 541, "Alta Fantasia");
            DataAccess.AddLibro("Nombre del Viento", "Daniel7", "27/03/2007", 800, "Fantasia");
            DataAccess.AddLibro("Libro", "Daniel", "14/03/2014", 9999, "CCFF");
            DataAccess.AddLibro("Harry Potter", "Joanne", "26/06/1997", 255, "Fantasia");
            DataAccess.AddLibro("El Hobbit", "John", "21/09/1937", 288, "Fantasia");
            */

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts 
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
