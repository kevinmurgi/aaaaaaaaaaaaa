using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ProyectoXamarin.ViewModel;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProyectoXamarin
{
    public partial class App : Application
    {
        static MainDatabase database;

        public App()
        {
            InitializeComponent();

            
            Database.AddAutor(new DataModel.Autor("Brandon", "Sanderson", "19/12/1975", "Desconocido", "Masculino"));
            Database.AddAutor(new DataModel.Autor("Patrick", "Rothfuss", "06/06/1973", "Desconocido", "Masculino"));
            Database.AddAutor(new DataModel.Autor("Daniel", "Rubio", "10/10/1999", "Desconocido", "Masculino"));
            Database.AddAutor(new DataModel.Autor("Joanne", "Rowling", "31/07/1965", "Desconocido", "Femenino"));
            Database.AddAutor(new DataModel.Autor("John", "Tolkien", "03/01/1892", "Desconocido", "Masculino"));

            Database.AddLibro(new DataModel.Libro("Mistborn", "Brandon", "04/12/2016", 541, "Alta Fantasia"));
            Database.AddLibro(new DataModel.Libro("Nombre del Viento", "Patrick", "27/03/2007", 800, "Fantasia"));
            Database.AddLibro(new DataModel.Libro("Libro", "Daniel", "14/03/2014", 9999, "CCFF"));
            Database.AddLibro(new DataModel.Libro("Harry Potter", "Joanne", "26/06/1997", 255, "Fantasia"));
            Database.AddLibro(new DataModel.Libro("El Hobbit", "John", "21/09/1937", 288, "Fantasia"));
            

            MainPage = new ProyectoXamarin.MainPage();
        }

        public static MainDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new MainDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Biblioteca.db3"));
                }
                return database;
            }
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
