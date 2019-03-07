using JsonThings.Data;
using JsonThings.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace JsonThings
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<Post> posts = new List<Post>();
        public MainPage()
        {
            this.InitializeComponent();
            getPostsData();
        }

        /**
         * Metodo que recoge todos los objetos Post de la API
         * en json-server haciendo uso de la clase JsonData.cs
         */
        private async void getPostsData()
        {
            posts = await JsonData.GetPosts();
            listaDatos.ItemsSource = posts;
        }

        /**
         * Evento del boton de borrado que borra el objeto Post
         * seleccionado de la lista y, en caso de que se borre 
         * de forma satisfactorio, actualiza la lista de Post.
         * Todo esto haciendo uso dela clase JsonData.
         */
        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            Post seleccionado = (Post) listaDatos.SelectedItem;
            String res = await JsonData.DeletePost(seleccionado.id);

            if (res.ToLower().Equals("ok"))
            {
                getPostsData();
            }
        }

        /**
         * 
         */
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Post np = new Post();
            np.id = 99;
            np.author = "yo";
            np.title = "titulito";
            JsonData.PostPost(np);
        }
    }
}
