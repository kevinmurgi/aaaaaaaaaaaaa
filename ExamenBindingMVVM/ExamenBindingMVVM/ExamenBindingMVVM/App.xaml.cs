using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExamenBindingMVVM.ModeloDatos;
using System.IO;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExamenBindingMVVM
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            App.Database.SaveCita(new Cita("Kevin", "Martinez Leiva", "28/12/2018", "15:21:00", "Revision"));
            App.Database.SaveCita(new Cita("Kevin", "Martinez Leiva", "19/12/2018", "09:00:00", "Primera Cita"));
            App.Database.SaveCita(new Cita("Daniel", "Rubio Rubia", "05/01/2019", "11:05:00", "Urgencia"));
            App.Database.SaveCita(new Cita("Jose", "GarciaPintor", "03/01/2019", "17:13:00", "Revision"));
            App.Database.SaveCita(new Cita("Cristobal", "Cano", "21/12/2018", "08:11:00", "Primera Cita"));
        }

        static BaseDatos database;
        public static BaseDatos Database
        {
            get
            {
                if (database == null)
                {
                    database = new BaseDatos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "citas.db3"));
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
